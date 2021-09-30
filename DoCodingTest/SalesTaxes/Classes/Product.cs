using System;
using System.Reflection.Metadata.Ecma335;

namespace DoCodingTest.SalesTaxes
{
    public class Product
    {
        private Guid _id;
        private bool _imported;
        private string _title;
        private Decimal _price;
        private ProductType _productType;

        public string Title => _title;
        public Guid Id => _id;
        public ProductType ProductType => _productType;
        public Decimal Price => _price;
        public bool Imported => _imported;
        
        public Product(string title, bool imported, Decimal price, ProductType productType)
        {
            _id = Guid.NewGuid();
            _imported = imported;
            _title = title;
            _price = price;
            _productType = productType;
        }
    }
}