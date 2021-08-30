using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleExcavate.model
{
    public class UnitModel
    {
        public ulong id { get; set; }
        public string content { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public List<ulong> way { get; set; }
    }
}
