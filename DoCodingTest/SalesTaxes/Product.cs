using System;
using System.Reflection.Metadata.Ecma335;

namespace DoCodingTest.SalesTaxes
{
    public class Product
    {
        private Guid _id;
        private bool _imported;
        private string _title;
        private float _price;
        private ProductType _productType;

        public string Title => _title;
        public Guid Id => _id;
        public ProductType ProductType => _productType;

        public Product(string title, bool imported, float price, ProductType productType)
        {
            _id = new Guid();
            _imported = imported;
            _title = title;
            _price = price;
            _productType = productType;
        }
    }
}