using System.Collections.Generic;

namespace GildedRose.ViewModels
{
    public class ProductsPagedListVM
    {
        public int TotalProducts { get; set; } 
        public IEnumerable<ProductVM> Products { get; set; }

    }
}
