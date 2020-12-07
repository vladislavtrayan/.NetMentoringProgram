using System;

namespace Task1
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public bool Equals(Product obj)
        {
            if (obj == null)
                return false;
            return Name == obj.Name && Price == obj.Price;
        }
    }
}
