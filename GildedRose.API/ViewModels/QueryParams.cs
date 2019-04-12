﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.ViewModels
{
    public class QueryParams
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SearchText { get; set; }
    }
}