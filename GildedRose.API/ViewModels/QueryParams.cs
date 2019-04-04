using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.API.ViewModels
{
    public class QueryParams
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SearchText { get; set; }
    }
}
