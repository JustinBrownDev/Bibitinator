using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Bibitinator.Models.BibiteTracer
{
    public class brain
    {
        public string name { get; set; }
        public Int16 index { get; set; }
        public List<neuron> Neurons { get; set; }
        public TreeNode brainTree { get; set; }
        public List<string> outPutEQs { get; set; }
    }
}
