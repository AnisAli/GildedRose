﻿using System;

namespace GildedRose.API
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(int productId) : base($"Product: {productId} is out of stock.")
        {
        }

        public OutOfStockException(string message)
            : base(message)
        {
        }

        public OutOfStockException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
