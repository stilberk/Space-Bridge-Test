﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Bridge_Test
{
    class Object
    {
        private int x;
        private int y;
        private ConsoleColor color;
        private string symbol;
        private bool bridgeHitted;
        public Object(int x, int y, string symbol, ConsoleColor color = ConsoleColor.Green, bool bridgeHitted = false)
        {
            this.BridgeHitted = bridgeHitted;
            this.X = x;
            this.Y = y;
            this.Symbol = symbol;
            this.Color = color;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        public bool BridgeHitted { get; set; }
    }
}
