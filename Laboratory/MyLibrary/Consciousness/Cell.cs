using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Consciousness
{
    public class Cell
    {
        public string Content { get; set; }
        public List<int> Loop { get; set; } = new List<int>();
    }
}
