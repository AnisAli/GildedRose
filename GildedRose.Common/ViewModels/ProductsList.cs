using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.Common.ViewModels
{
    public class ProductsPagedList
    {
        public int TotalProducts { get; set; } 
        public IEnumerable<ProductVM> Products { get; set; }

    }
}
