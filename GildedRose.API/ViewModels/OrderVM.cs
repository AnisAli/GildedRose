using GildedRose.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.API.ViewModels
{
    public class OrderVM
    {
        public Guid OrderId { get; set; }

        public ProductVM Product { get; set; }

        public DateTime TimeStamp { get; set; }
    }

}
