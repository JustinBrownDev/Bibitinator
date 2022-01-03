using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models.BibiteTracer
{
    public class neuron
    {
        public Int16 index { get; set; }
        public string type { get; set; }
        public decimal value { get; set; }
        public bool isExit { get; set; }
        public bool isEntrance { get; set; }
        public Int16 inputCount { get; set; }
        public Int16 outputCount { get; set; }
        public List<synapse> input { get; set; }
        public List<synapse> output { get; set; }
    }
}
