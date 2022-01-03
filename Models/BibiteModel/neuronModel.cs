using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class neuronModel
    {
        public Int32 type { get; set; }
        public string typeName { get; set; }
        public Int32 index { get; set; }
        public Int32 inov { get; set; }
        public Int32 nIn { get; set; }
        public Int32 nOut { get; set; }
        public string desc { get; set; }
        public decimal value { get; set; }
        public Int32 lastInput { get; set; }
        public Int32 lastOutput { get; set; }
    }
}
