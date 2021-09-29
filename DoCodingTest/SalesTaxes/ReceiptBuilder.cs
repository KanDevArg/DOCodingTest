using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
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
                        Tax = CalculateTax(p.DisplayPrice, p.Imported, p.ProductType),
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
                        Tax = RoundUpLast5Cents(p.Tax),
                        DisplayPrice = Math.Round(p.DisplayPrice + RoundUpLast5Cents(p.Tax), 2),
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
            
            
            foreach (var item in roundedPrices)
            {
                Console.Write("- \t");
                Console.Write(item.Title );
                Console.Write("\t\t\t\t");
                Console.Write(item.Count);
                Console.Write("\t");
                Console.Write(item.DisplayPrice);
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine($"Sales Taxes: {totals.First().TotalTaxes.ToString()}");
            Console.WriteLine($"Total: {totals.First().Total.ToString()}");
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
        
        private decimal RoundUpLast5Cents(decimal price)
        {
            var arrPrice = price.ToString().Split(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            if (arrPrice.Length <= 1) return price;

            var ss = arrPrice[1].Substring(1, arrPrice[1].Length - 1);
            
            var second = int.Parse(ss);

            if (second == 0 || second == 50) return price;
            
            if (second< 50 )
            {
                var numberValue = arrPrice[0];
                var decimalValues = arrPrice[1].Substring(0, 1);
                return ((decimal.Parse(numberValue + decimalValues + "5")) / 100);
            }
            else
            {
                var numberValue = arrPrice[0];
                var decimalValues = arrPrice[1].Substring(0, 1);
                return ((decimal.Parse(numberValue + (int.Parse(decimalValues)+1) + "0")) / 100);
            }
        }
    }
}