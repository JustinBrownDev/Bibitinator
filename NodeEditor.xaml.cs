using DynamicData;
using NodeNetwork;
using NodeNetwork.ViewModels;
using NodeNetwork.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bibitinator.Models;
using NodeNetwork.Toolkit.Layout.ForceDirected;
using System.Windows.Controls.Primitives;

namespace Bibitinator
{
    /// <summary>
    /// Interaction logic for NodeEditor.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NNViewRegistrar.RegisterSplat();
        }
    }
    public partial class NodeEditor : Window
    {
        public List<NodeViewModel> inputNodeViews = new List<NodeViewModel>();
        public List<NodeViewModel> outputNodeViews = new List<NodeViewModel>();
        public BibiteReflect.Root bibRoot = new BibiteReflect.Root();
        public List<NodeNetworkNodesDictionary> network = null;
        public NodeEditor(BibiteReflect.Root root)
        {
            bibRoot = root;
            InitializeComponent();
            //Create a new viewmodel for the NetworkView
            network = new List<NodeNetworkNodesDictionary>();
            var net = new NetworkViewModel();
            foreach (BibiteReflect.Node n in root.brain.Nodes)
            {
                var netNode = new NodeViewModel();
                var node = new NodeNetworkNodesDictionary();
                netNode.Name = n.Desc + ", " + n.TypeName;

                //if (n.TypeName == "Input")
                //{
                //    netNode.Outputs.Add(new NodeOutputViewModel());
                //}
                //else if (n.Desc.Contains("Hidden"))
                //{
                //    netNode.Outputs.Add(new NodeOutputViewModel());
                //    netNode.Inputs.Add(new NodeInputViewModel());
                //}
                //else
                //{
                //    netNode.Inputs.Add(new NodeInputViewModel());
                //}
                if (n.TypeName != "Input") netNode.Inputs.Add(new NodeInputViewModel());
                node.NetworkNode = netNode;
                node.BibiteNode = n;
                network.Add(node);

            }
            foreach (BibiteReflect.Synaps s in root.brain.Synapses)
            {
                var i = new NodeInputViewModel();
                i.Visibility = EndpointVisibility.Auto;
                var o = new NodeOutputViewModel();
                o.Visibility = EndpointVisibility.Auto;
                network.Find(x => x.BibiteNode.Index == s.NodeOut).NetworkNode.Inputs.Add(i);
                network.Find(x => x.BibiteNode.Index == s.NodeIn).NetworkNode.Outputs.Add(o);
                ConnectionViewModel connection = new ConnectionViewModel(net, i, o);
                net.Connections.Add(connection);
            }
            network.ForEach(x => net.Nodes.Add(x.NetworkNode));
            networkView.ViewModel = net;
        }


        private bool isFixed(NodeViewModel node)
        {
            return inputNodeViews.Contains(node) || outputNodeViews.Contains(node);
        }

        private double spring(ConnectionViewModel con)
        {
            return 6;
        }
        private void Arrange()
        {
            inputNodeViews.Clear();
            outputNodeViews.Clear();
            network.FindAll(x => x.BibiteNode.TypeName == "Input").ForEach(x => inputNodeViews.Add(x.NetworkNode));
            network.FindAll(x => !x.BibiteNode.Desc.Contains("Hidden") && !inputNodeViews.Contains(x.NetworkNode)).ForEach(x => outputNodeViews.Add(x.NetworkNode));

            Point pos = new Point(0, 0);
            foreach (NodeViewModel node in inputNodeViews)
            {
                NodeViewModel transNode = node;
                transNode.Position = pos;
                pos.Y += 150;
                network.FindAll(x => x.NetworkNode == node).ForEach(n => n.NetworkNode = transNode);

            }
            pos = new Point(5000, 0);

            foreach (NodeViewModel node in outputNodeViews)
            {
                NodeViewModel transNode = node;
                transNode.Position = pos;
                pos.Y += 200;
                network.FindAll(x => x.NetworkNode == node).ForEach(n => n.NetworkNode = transNode);
            }
            pos.Y = 0;
            ForceDirectedLayouter layouter = new ForceDirectedLayouter();
            var config = new Configuration
            {
                Network = networkView.ViewModel,
                IsFixedNode = isFixed,
                NodeRepulsionForce = 4,
                SpringConstant = spring,
                RowForce = spring,
            };
            layouter.Layout(config, 10000);
        }

        private void ArrangeButton_Click(object sender, RoutedEventArgs e)
        {
            
            Arrange();
        }
    }
}
