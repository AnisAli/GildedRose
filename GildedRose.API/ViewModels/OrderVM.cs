using GildedRose.ViewModels;
using System;

namespace GildedRose.API.ViewModels
{
    public class OrderVM
    {
        public Guid OrderId { get; set; }

        public ProductVM Product { get; set; }

        public DateTime TimeStamp { get; set; }
    }

}
