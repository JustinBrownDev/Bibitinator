using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;

namespace Bibitinator.Models
{
    public class NodeNetworkNodesDictionary
    {
        public NodeViewModel NetworkNode { get; set; }
        public BibiteReflect.Node BibiteNode { get; set; }
    }
}
