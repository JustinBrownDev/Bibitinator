using Bibitinator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Bibitinator.Models.BibiteReflect;

namespace Bibitinator
{
    public partial class BibiteEditor : Form
    {

        NeuralNetEditorNodes net = new NeuralNetEditorNodes();
        BibiteCollection bibCol = null;
        public BibiteEditor(BibiteCollection col)
        {
            InitializeComponent();
            bibCol = col;
        }
        private void BibiteEditor_Load(object sender, EventArgs e)
        {
            propertiesTree.Nodes.Add(createNode(bibCol));
            propertiesTree.Nodes[0].Expand();
            //name the nodes
            TreeNode brainTree = propertiesTree.Nodes[0].Nodes[6].Nodes[2];
            List<string> dic = new List<string>();
            int x = 0;
            if (brainTree.Nodes[0].Nodes.Count == 8) x = 2;
            foreach (TreeNode node in brainTree.Nodes)
            {
                string desc = node.Nodes[6 - x].Nodes[0].Text + " (" + node.Nodes[1].Nodes[0].Text + ")";
                dic.Add(desc);
                node.Text = desc;
            }
            bibCol.Root = JsonConvert.DeserializeObject<BibiteReflect.Root>(bibCol.json);
            buildTextboxes();
            Trace();
            splitContainer1.Cursor = Cursors.Arrow;
            this.Text = bibCol.name;
        }
        public TreeNode createNode(BibiteCollection col)
        {
            var jobj = JObject.Parse(col.json);
            TreeNode tn = new TreeNode();
            void AddNode(JToken token, TreeNode inTreeNode)
            {
                if (token == null)
                    return;
                if (token is JValue)
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                    inTreeNode.Text += (":  " + token.ToString());
                    childNode.Tag = token;
                }
                else if (token is JObject)
                {
                    var obj = (JObject)token;
                    foreach (var property in obj.Properties())
                    {
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name))];
                        childNode.Tag = property;
                        AddNode(property.Value, childNode);
                    }
                }
                else if (token is JArray)
                {
                    var array = (JArray)token;
                    for (int i = 0; i < array.Count; i++)
                    {
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                        childNode.Tag = array[i];
                        AddNode(array[i], childNode);
                    }
                }
            }
            AddNode(jobj, tn);
            tn.Text = col.name;
            return tn;
        }
        public void buildTextboxes()
        {
            //BibiteReflect.Root obj = JsonConvert.DeserializeObject<BibiteReflect.Root>(bibCol.json);
            foreach (PropertyInfo prop in typeof(BibiteReflect.Genes1).GetProperties())
            {
                if (prop.PropertyType == typeof(double) && bibCol.json.Contains(prop.Name))
                {
                    Label l = new Label();
                    TextBox t = new TextBox();
                    l.Text = prop.Name;
                    l.Tag = "label";
                    t.Text = prop.GetValue(bibCol.Root.genes.genes).ToString();
                    t.Tag = prop.Name;
                    Panel p = new Panel();
                    p.Width = l.Width + t.Width;
                    p.Height = l.Height + t.Height;
                    l.Parent = p;
                    l.Dock = DockStyle.Left;
                    t.Parent = p;
                    t.Dock = DockStyle.Right;
                    p.Parent = editorflow;
                }
            }
            List<Node> nodes = bibCol.Root.brain.Nodes;
            foreach(Node node in net.middleNodes)
            {
                AddNeuronComboBox.Items.Add(node.TypeName);
            }
            foreach (Node node in nodes)
            {
                if (node.Index <= 28)
                {
                    inputComboBox.Items.Add(node.Desc + " :Input");
                }
                else if (node.Index <= 43)
                {
                    outputComboBox.Items.Add(node.Desc + " :Output");
                }
                else
                {
                    inputComboBox.Items.Add(node.Desc + " :" + node.TypeName);
                    outputComboBox.Items.Add(node.Desc + " :" + node.TypeName);
                }
            }
            inputComboBox.Text = "select";
            outputComboBox.Text = "select";
            List<Synaps> synaps = bibCol.Root.brain.Synapses;
            foreach (Synaps synap in synaps)
            {
                AddSynapsePanel(synap);
            }
        }
        private void AddSynapsePanel(BibiteReflect.Synaps synap)
        {
            List<Node> relatedNodes = new List<Node>();
            relatedNodes.Add(bibCol.Root.brain.Nodes.Find(x => x.Index == synap.NodeIn));
            relatedNodes.Add(bibCol.Root.brain.Nodes.Find(x => x.Index == synap.NodeOut));
            FlowLayoutPanel p = new FlowLayoutPanel();
            p.Anchor = AnchorStyles.Top;
            TextBox cbIn = new TextBox();
            TextBox cbOut = new TextBox();
            TextBox nud = new TextBox();
            Button del = new Button();
            cbIn.ReadOnly = true;
            cbOut.ReadOnly = true;
            nud.ReadOnly = true;
            nud.Tag = synap;
            del.Text = "Remove";
            del.Click += synapseDelete;
            cbIn.Text = synap.NodeIn < 43 ? relatedNodes[0].Desc : relatedNodes[0].Desc + " :" + relatedNodes[0].TypeName;
            cbIn.Tag = bibCol.Root.brain.Nodes[synap.NodeIn].Index;
            cbOut.Text = synap.NodeOut < 43 ? relatedNodes[1].Desc : relatedNodes[1].Desc + " :" + relatedNodes[1].TypeName;
            cbOut.Tag = relatedNodes[1].Index;
            nud.Text = synap.Weight.ToString();
            p.Cursor = Cursors.Arrow;
            p.Controls.Add(cbIn);
            p.Controls.Add(cbOut);
            p.Controls.Add(nud);
            p.Controls.Add(del);
            p.Height = cbIn.Height + 10;
            cbIn.Width = 125;
            cbOut.Width = 125;
            p.Tag = relatedNodes;
            p.Parent = BrainEditorPanel;
            p.Dock = DockStyle.Top;
            p.Show();
        }
        private void synapseDelete(object sender, EventArgs e)
        {
            Panel parent = (Panel)((Button)sender).Parent;

            int synIn = Convert.ToInt32(parent.Controls[0].Tag);
            int synOut = Convert.ToInt32(parent.Controls[1].Tag);
            decimal strength = Convert.ToDecimal(parent.Controls[2].Text);

            var syn = bibCol.Root.brain.Synapses[Convert.ToInt32(BrainEditorPanel.Controls.IndexOf(parent))];
            bibCol.Root.brain.Synapses.Remove(syn);
            Trace();
            BrainEditorPanel.Controls.Remove(((Button)sender).Parent);
        }

        private void Trace()
        {
            //BibiteReflect.Root obj = JsonConvert.DeserializeObject<BibiteReflect.Root>(bibCol.json);
            ConnectionsTreeView.Nodes.Clear();
            List<string> InputList = new List<string>();
            List<int> NoLoops = new List<int>();
            void Tracer(BibiteReflect.Node n, TreeNode inTreeNode)
            {

                //the neuron being passed in has already been added to the dataset.

                if (n.TypeName.Equals("Input") && !InputList.Contains(n.Desc)) InputList.Add(n.Desc);
                else if (!NoLoops.Contains(n.Index))
                {
                    NoLoops.Add(n.Index);
                    //make list nodes with synapses going into current neuron
                    List<BibiteReflect.Node> inputNeurons = new List<BibiteReflect.Node>();
                    List<double> weight = new List<double>();

                    foreach (BibiteReflect.Synaps s in bibCol.Root.brain.Synapses)
                    {
                        if (s.NodeOut == n.Index)
                        {
                            inputNeurons.Add(bibCol.Root.brain.Nodes[s.NodeIn]);
                            weight.Add(s.Weight);
                        }
                    }
                    //find inputs, recurse
                    for (int i = 0; i < inputNeurons.Count; i++)
                    {
                        string nodetext;
                        if (inputNeurons[i].Desc.Contains("Hidden")) nodetext = inputNeurons[i].TypeName;
                        else nodetext = inputNeurons[i].Desc;
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(Text = (nodetext + ", strength: " + weight[i]).ToString()))];
                        childNode.Tag = bibCol.Root.brain.Synapses.Find(x => x.NodeOut == n.Index && x.NodeIn == inputNeurons[i].Index);
                        Tracer(inputNeurons[i], childNode);
                    }

                }
            }
            //start with exit neurons
            foreach (BibiteReflect.Node n in bibCol.Root.brain.Nodes)
            {
                if (n.TypeName != "Input" && !n.Desc.Contains("Hidden"))
                {
                    InputList.Clear();
                    string listtext = string.Empty;
                    TreeNode node = new TreeNode();
                    NoLoops.Clear();
                    Tracer(n, node);
                    for (int i = 0; i < InputList.Count; i++)
                    {
                        listtext += InputList[i];
                        if (i != InputList.Count - 1) listtext += ", ";

                    }
                    node.Text = n.Desc + "  Input Neurons : " + listtext;
                    if (node.Nodes.Count > 0) ConnectionsTreeView.Nodes.Add(node);
                }
            }
        }
        private string ReplaceValuesInBibite(string json)
        {
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'N', 'f', 't' };
            char[] stops = { ',', '}', ']' };
            if (File.Exists(bibCol.extractFolder + "\\bibites\\" + bibCol.name))
            {
                File.Delete(bibCol.extractFolder + "\\bibites\\" + bibCol.name);
            }
            foreach (Panel p in editorflow.Controls)
            {
                foreach (Control c in p.Controls)
                {
                    if (!c.Tag.Equals("label"))
                    {
                        TextBox box = (TextBox)c;
                        string value = box.Text;
                        if (value.Equals("NaN")) value = "NaN\"";
                        int propIndex = json.IndexOf('"' + box.Tag.ToString() + '"') + box.Tag.ToString().Length + 2;
                        int valIndex = json.IndexOfAny(nums, propIndex);
                        int stopRemoving = json.IndexOfAny(stops, valIndex) - valIndex;
                        json = json.Remove(valIndex, stopRemoving);
                        json = json.Insert(valIndex, value);
                    }
                }

            }

            json = Regex.Replace(json, @"\s", "");
            return json;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string json = ReplaceValuesInBibite(bibCol.json);
            File.WriteAllText(bibCol.extractFolder + "\\bibites\\" + bibCol.name, json);

            if (File.Exists(bibCol.extractFolder + "\\bibites\\" + bibCol.name))
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Failure!");
            }
            Cursor = Cursors.Default;
        }

        private void GenesResetButton_Click(object sender, EventArgs e)
        {
            editorflow.Controls.Clear();
            buildTextboxes();
        }
        private void ConnectionsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (MovingLabel.Text == "")
            {
                MovingLabel.Text = "Selected";
            }
            foreach (Control con in BrainEditorPanel.Controls)
            {
                if (con.Controls[2].Tag == ConnectionsTreeView.SelectedNode.Tag)
                {
                    //MovingLabel.Parent.Controls.Remove(MovingLabel);
                    con.Controls.Add(MovingLabel);
                    BrainEditorPanel.ScrollControlIntoView(con);
                    break;
                }
            }
        }
        private void addSynapse_Click(object sender, EventArgs e)
        {
            Synaps s = new Synaps();
            string input = inputComboBox.SelectedItem.ToString();
            input = input.Remove(input.IndexOf(' '));
            string output = outputComboBox.SelectedItem.ToString();
            output = output.Remove(output.IndexOf(' '));
            s.NodeIn = bibCol.Root.brain.Nodes.Find(x => x.Desc == input).Index;
            s.NodeOut = bibCol.Root.brain.Nodes.Find(x => x.Desc == output).Index;
            s.En = true;
            s.Weight = (double)strengthUpDown.Value;
            s.Inov = 0;
            bibCol.Root.brain.Synapses.Add(s);
            AddSynapsePanel(s);
        }

        private void BrainResetButton_Click(object sender, EventArgs e)
        {

        }
        private void BrainSaveButton_Click(object sender, EventArgs e)
        {
            applyBrainChangesToModel();
            serializeModelData(bibCol.name);
        }
        private void BrainSaveCopyButton_Click(object sender, EventArgs e)
        {
            applyBrainChangesToModel();
            List<string> names = Directory.EnumerateFiles(bibCol.extractFolder + "\\bibites").ToList();
            string name = "bibite_" + names.Count() + bibCol.name.Substring(bibCol.name.IndexOf('.'));
            int i = names.Count() + 1;
            while (File.Exists(bibCol.extractFolder + "\\bibites\\" + name))
            {
                name = "bibite_" + i + bibCol.name.Substring(bibCol.name.IndexOf('.'));
            }
            serializeModelData(name);
        }
        public class IgnorePropertiesResolver : DefaultContractResolver
        {
            //used to specify which properties to not include when serializing json files
            //allows the files to be saved with the correct properties for both versions, using the same model
            private readonly HashSet<string> ignoreProps;
            public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
            {
                this.ignoreProps = new HashSet<string>(propNamesToIgnore);
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);
                if (this.ignoreProps.Contains(property.PropertyName))
                {
                    property.ShouldSerialize = _ => false;
                }
                return property;
            }
        }
        private void applyBrainChangesToModel()
        {
            List<Synaps> synapses = new List<Synaps>();
            NeuralNetEditorNodes nodes = new NeuralNetEditorNodes();
            Node[] inputNodes;
            Node[] outputNodes;
            if (bibCol.name.Contains(".bb8"))
            {
                inputNodes = nodes.inputNodesNew;
                outputNodes = nodes.outputNodesNew;
            }
            else
            {
                inputNodes = nodes.inputNodesOld;
                outputNodes = nodes.outputNodesOld;
            }
            List<Node> nodesResult = new List<Node>();
            //static nodes are the input and output neurons that are always present regardless of wether they have synapses going to them
            List<Node> staticNodes = new List<Node>();
            staticNodes.AddRange(inputNodes);
            staticNodes.AddRange(outputNodes);

            int staticNodeCount = staticNodes.Count;
            foreach (Control con in BrainEditorPanel.Controls)
            {
                //the nodes associated with each synapse are stored as a tag when generating the panels
                List<Node> relatedNodes = (List<Node>)con.Tag;
                Synaps s = new Synaps();
                s.NodeIn = Convert.ToInt32(con.Controls[0].Tag);
                s.NodeOut = Convert.ToInt32(con.Controls[1].Tag);
                s.Weight = Convert.ToDouble(con.Controls[2].Text);
                s.Inov = 0;
                s.En = true;
                synapses.Add(s);
                relatedNodes[0].NOut = 0;
                relatedNodes[1].NOut = 0;
                relatedNodes[0].NIn = 0;
                relatedNodes[1].NIn = 0;
                if (nodesResult.Find(x => x.Index == relatedNodes[0].Index) == null)
                {
                    nodesResult.Add(relatedNodes[0]);
                }
                if (nodesResult.Find(x => x.Index == relatedNodes[1].Index) == null)
                {
                    nodesResult.Add(relatedNodes[1]);
                }
            }
            bool complete = false;
            //trim the synapses for only those that serve a function, this doesnt work entirely but its enough for the game to load
            while (!complete)
            {
                int count = synapses.Count;
                foreach (Synaps s in synapses)
                {
                    if (s.NodeIn >= staticNodeCount && synapses.FindAll(x => x.NodeOut == s.NodeIn) == null) synapses.Remove(s);
                    else if (s.NodeOut >= staticNodeCount && synapses.FindAll(x => x.NodeIn == s.NodeOut) == null) synapses.Remove(s);
                }
                if (synapses.Count == count) complete = true;
            }
            List<Node> validatedNodes = new List<Node>();
            nodesResult.OrderBy(x => x.Index);
            //create a list of nodes that are associated with the trimmed list of synapses
            foreach (Synaps s in synapses)
            {
                if (validatedNodes.Find(x => x.Index == s.NodeIn) == null) validatedNodes.Add(nodesResult.Find(x => x.Index == s.NodeIn));
                if (validatedNodes.Find(x => x.Index == s.NodeOut) == null) validatedNodes.Add(nodesResult.Find(x => x.Index == s.NodeOut));
            }
            validatedNodes.OrderBy(x => x.Index).ToList();
            //add any missing static nodes
            for (int i = 0; i < staticNodeCount; i++)
            {
                if (validatedNodes.Find(x => x.Index == i) == null)
                {
                    validatedNodes.Add(staticNodes[i]);
                }
            }
            validatedNodes = validatedNodes.OrderBy(x => x.Index).ToList();
            //first assign the Nodein and Nodeout synapse properties to the List index of their associated node, then assign the index property of the nodes to their list index
            //this ensures theres no gaps in the indecies of the nodes from deleted nodes
            //fix the HiddenX numbers of middle neurons so they are in order
            foreach (Synaps s in synapses)
            {
                s.NodeIn = validatedNodes.IndexOf(validatedNodes.Find(x => x.Index == s.NodeIn));
                s.NodeOut = validatedNodes.IndexOf(validatedNodes.Find(x => x.Index == s.NodeOut));
            }
            validatedNodes.ForEach(x => x.Index = validatedNodes.IndexOf(x));
            validatedNodes.FindAll(x => x.Desc.Contains("Hidden")).ForEach(x => x.Desc = "Hidden" + (x.Index - staticNodeCount).ToString());
            //assign validated data to the root model
            bibCol.Root.brain.Nodes = validatedNodes;
            bibCol.Root.brain.Synapses = synapses;
        }
        void serializeModelData(string name)
        {
            //begin serializing the data to a json file
            //the version selection is janky, needs to use the version property in settings.json, currently theres only two versions supported and it differentiates via the file type
            string json = null;
            if (bibCol.name.Contains(".bb8"))
            {
                json = JsonConvert.SerializeObject(bibCol.Root, new JsonSerializerSettings()
                { ContractResolver = new IgnorePropertiesResolver(new[] { "NIn", "NOut", "maxHealth", "MaxEnergy", "d1Size", "isWow" }) });
            }
            else if (bibCol.name.Contains(".json"))
            {
                json = JsonConvert.SerializeObject(bibCol.Root, new JsonSerializerSettings()
                { ContractResolver = new IgnorePropertiesResolver(new[] { "Fullness", "Phero1Heading", "Phero2Heading", "Phero3Heading", "GrowthScale", "GrowthMaturityFactor", "GrowthMaturityExponent", "biteProgress", "stomach" }) });
            }
            if (File.Exists(bibCol.extractFolder + "\\bibites\\" + name))
            {
                File.Delete(bibCol.extractFolder + "\\bibites\\" + name);
            }
            File.WriteAllText(bibCol.extractFolder + "\\bibites\\" + name, json);
            if (File.Exists(bibCol.extractFolder + "\\bibites\\" + name))
            {
                MessageBox.Show("Saved Sucessfully");
            }
        }

        private void NodeEditorButton_Click(object sender, EventArgs e)
        {
            App n = new App();
            n.Run(new NodeEditor(bibCol.Root));
        }

        private void AddNeuronButton_Click(object sender, EventArgs e)
        {
            Node n = new Node();
            n = net.middleNodes.ToList().Find(x => x.TypeName == AddNeuronComboBox.SelectedItem.ToString());
            n.Index = bibCol.Root.brain.Nodes.Count();
            int i = bibCol.Root.brain.Nodes.FindAll(x => x.Desc.Contains("Hidden")).Count();           
            while (bibCol.Root.brain.Nodes.Find(x => x.Desc == "Hidden" + i) != null) i++;
            n.Desc = "Hidden" + i;
            bibCol.Root.brain.Nodes.Add(n);
            inputComboBox.Items.Add(n.Desc + " :" + n.TypeName);
            outputComboBox.Items.Add(n.Desc + " :" + n.TypeName);
        }
    }
}
