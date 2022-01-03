using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator.Models
{
    public class bibiteBrainModel
    {
        public bool isReady { get; set; }
        public bool parent { get; set; }
        public List<neuronModel> nodes { get; set; }
        public List<SynapseModel> synapses { get; set; }
    }
}
