using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.ViewModels
{
    public class ProductsPagedListVM
    {
        public int TotalProducts { get; set; } 
        public IEnumerable<ProductVM> Products { get; set; }

    }
}
