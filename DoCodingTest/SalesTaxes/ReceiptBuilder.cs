using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DoCodingTest.SalesTaxes
{
    public class ReceiptBuilder
    {
        public void PrintReceipt(IEnumerable<Product> products)
        {
            var (roundingAppliedToTaxValues, totals) = PrepareReceiptData(products);


            ShowReceipt(roundingAppliedToTaxValues, totals);
        }

        private (List<ReceiptDisplayItem> roundingAppliedToTaxValues, List<ReceiptBottomTotals> totals) PrepareReceiptData(IEnumerable<Product> products)
        {
            //Get items grouped by id and get the total price for the groups
            var itemsGroupedById = products
                .GroupBy(p => p.Id)
                .Select(p => new ReceiptDisplayItem()
                    {
                        Who = "Display Item",
                        Title = p.First().Title,
                        Count = p.Count().ToString(),
                        DisplayPrice = p.Sum(c => c.Price),
                        UnitPrice = p.First().Price,
                        Imported = p.First().Imported,
                        ProductType = p.First().ProductType
                    }
                ).ToList();


            //Calculate Taxes
            var itemsWithTaxCalculation = itemsGroupedById
                .Select(p => new ReceiptDisplayItem()
                    {
                        Who = "Display Item",
                        Title = p.Title,
                        Count = p.Count,
                        Tax = RoundToNearest5Cents(CalculateTax(p.DisplayPrice, p.Imported, p.ProductType)),
                        DisplayPrice = p.DisplayPrice,
                        UnitPrice = p.UnitPrice,
                        Imported = p.Imported,
                        ProductType = p.ProductType
                    }
                ).ToList();

            //Rounding of taxes values
            var roundingAppliedToTaxValues = itemsWithTaxCalculation
                .Select(p => new ReceiptDisplayItem()
                    {
                        Who = "Display Item",
                        Title = p.Title,
                        Count = p.Count,
                        Tax = p.Tax,
                        DisplayPrice = p.DisplayPrice + p.Tax,
                        UnitPrice = p.UnitPrice,
                        Imported = p.Imported,
                        ProductType = p.ProductType
                    }
                ).ToList();


            //Calculate Total (total taxes and total)
            var totals = roundingAppliedToTaxValues
                .GroupBy(t => t.Who)
                .Select(t => new ReceiptBottomTotals()
                {
                    Total = t.Sum(t => t.DisplayPrice),
                    TotalTaxes = t.Sum(t => t.Tax)
                }).ToList();
            return (roundingAppliedToTaxValues, totals);
        }

        private static void ShowReceipt(List<ReceiptDisplayItem> roundedPrices, List<ReceiptBottomTotals> totals)
        {
            Console.WriteLine("\n");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("\tReceipt");
            Console.WriteLine("--------------------------------------------------------------------------------");
            
            
            foreach (var item in roundedPrices) {
                Console.Write("- \t");
                Console.Write(item.Title );
                Console.Write("\t\t\t\t");
                Console.Write(item.Count);
                Console.Write("\t");
                Console.Write(int.Parse(item.Count) > 1
                    ? $"{Math.Round(item.DisplayPrice, 2)} ( {item.Count} @ {Math.Round(item.DisplayPrice / int.Parse(item.Count), 2)})"
                    : $"{Math.Round(item.DisplayPrice, 2)}");
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine($"Sales Taxes: {Math.Round(totals.First().TotalTaxes, 2).ToString()}");
            Console.WriteLine($"Total: {Math.Round(totals.First().Total, 2).ToString()}");
            Console.WriteLine("\n");
        }

        private Decimal CalculateTax(Decimal price, bool imported, ProductType productType)
        {
            return imported switch
            {
                true when productType == ProductType.basicTaxed => price * (decimal)0.15,
                true when productType == ProductType.noTaxed => price * (decimal)0.05,
                false when productType == ProductType.basicTaxed =>  price * (decimal)0.10,
                _ => 0
            };
        }
        
        private decimal RoundToNearest5Cents(decimal price)
        {
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var arrPrice = price.ToString().Split(decimalSeparator);

            var hundredth = "";
            var thousandth = "";
            
            if (arrPrice.Length <= 1) return price;
            if (arrPrice[1].Length == 0) return price;
            
            var decimalChunk = arrPrice[1];
            if (arrPrice[1].Length > 3) {
               decimalChunk = arrPrice[1][..3];
            }
            
            var tenth = "" + decimalChunk[0];
            if (decimalChunk.Length > 1)
            {
                hundredth = "" + decimalChunk[1];
            }
            if (decimalChunk.Length > 2)
            {
                thousandth = "" + decimalChunk[2];
            }
            
            var value = int.Parse(tenth);
            
            if (thousandth != "") {
                value = int.Parse(thousandth);
            }
            else {
                if (hundredth != "") { 
                    value = int.Parse(hundredth);
                }    
            }

            if (value >= 0 && value <= 2) {
                hundredth = "0";
                thousandth = "0";
            }

            if (value >= 3 && value <= 7) {
                hundredth = "5";
                thousandth = "0";
            }
            
            if (value == 8 || value == 9) {
                hundredth = "0";
                thousandth = "0";
                tenth = (int.Parse(tenth) +1).ToString();
            }

            return ((decimal.Parse(arrPrice[0] + decimalSeparator + tenth + hundredth + thousandth)));
        }
    }
}