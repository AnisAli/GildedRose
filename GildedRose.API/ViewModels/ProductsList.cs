using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.API.ViewModels
{
    public class ProductsList
    {
        public int TotalProducts { get; set; } 
        public IList<Product> Products { get; set; }

    }
}
