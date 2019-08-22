using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Project
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public Product()
        {

        }
        public Product(int id, string name, string category, double price, string size, string description, string picture )
        {
            ID = id;
            Name = name;
            Category = category;
            Price = price;
            Size = size;
            Description = description;
            Picture = picture;
        }
       
    }
}
