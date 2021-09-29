using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace DoCodingTest.SalesTaxes
{
    public class ReceiptBuilder
    {
        public void PrintReceipt(List<Product> products)
        {
            var productsToBeTaxedWithBasicRatio = products.Where(p => p.ProductType == ProductType.other).ToList();

            //group by id...  aggregate, sum prices
        }
    }
}