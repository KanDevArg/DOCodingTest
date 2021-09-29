using System;

namespace DoCodingTest.SalesTaxes
{
    public class ReceiptDisplayItem
    {
        public string Who { get; set; }
        public string Title { get; set; }
        public string Count { get; set; }
        public decimal DisplayPrice { get; set; }
        
        public Decimal RoundedDisplayPrice { get; set; }
        public Decimal UnitPrice { get; set; }
        
        public Decimal Tax { get; set; }
        public bool Imported { get; set; }
        public ProductType ProductType { get; set; }
    }
    
    public class ReceiptBottomTotals
    {
        public Decimal TotalTaxes { get; set; }
        public Decimal Total { get; set; }
    }
}