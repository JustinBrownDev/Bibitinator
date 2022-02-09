using Bibitinator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using static Bibitinator.Models.NeuralNetEditorNodes;
//using static Bibitinator.Models.BibiteReflect;

namespace Bibitinator
{
    public partial class BibiteEditor : Form
    {
        NeuralNetEditorNodes net = new NeuralNetEditorNodes();
        BibiteCollection bibCol = null;
        public List<string> middleNodeNames = new List<string> { "Sigmoid", "Linear", "TanH", "Sine", "ReLu", "Gaussian", "Latch", "Differential" };
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
            int x = 0;
            //x is the offset needed to select the correct node in new versions, if the version is new apply the offset.
            if (brainTree.Nodes[0].Nodes.Count == 8) x = 2;
            foreach (TreeNode node in brainTree.Nodes)
            {
                string desc = node.Nodes[6 - x].Nodes[0].Text + " (" + node.Nodes[1].Nodes[0].Text + ")";
                node.Text = desc;
            }
            bibCol.dynRoot = (JObject)JsonConvert.DeserializeObject(bibCol.json, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture });
            //bibCol.Root = JsonConvert.DeserializeObject<BibiteReflect.Root>(bibCol.json, new JsonSerializerSettings(){Culture = CultureInfo.InvariantCulture});
            buildTextboxes();
            Trace();
            splitContainer1.Cursor = Cursors.Arrow;
            this.Text = bibCol.name;
        }
        //recursive function for building properties explorer
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
            //build genes editor
            foreach (FieldInfo info in bibCol.dynRoot.GetType().GetFields())
            {
                string text = info.Name;
                string text2 = info.GetValue(bibCol.dynRoot).ToString();
            }
            foreach (JProperty item in bibCol.dynRoot["genes"]["genes"].ToList())
            {
                Label l = new Label();
                TextBox t = new TextBox();
                l.Text = item.Name;
                l.Tag = "label";
                t.Text = item.Value.ToString();
                t.Tag = item.Name;
                Panel p = new Panel();
                p.Width = l.Width + t.Width;
                p.Height = l.Height + t.Height;
                l.Parent = p;
                l.Dock = DockStyle.Left;
                t.Parent = p;
                t.Dock = DockStyle.Right;
                p.Parent = editorflow;
            }
            //build nodes dropdowns
            List<JToken> nodes = bibCol.dynRoot["brain"]["Nodes"].ToList();
            //List<Node> nodes = bibCol.Root.brain.Nodes;
            foreach (string node in middleNodeNames)
            {
                AddNeuronComboBox.Items.Add(node);
            }
            foreach (JToken node in nodes)
            {

                if (node.Value<string>("TypeName").Equals("Input"))
                {
                    inputComboBox.Items.Add(node.Value<string>("Desc") + " :Input");
                }
                else if (node.Value<string>("Desc").Contains("Hidden"))
                {
                    inputComboBox.Items.Add(node.Value<string>("Desc") + " :" + node.Value<string>("TypeName"));
                    outputComboBox.Items.Add(node.Value<string>("Desc") + " :" + node.Value<string>("TypeName"));
                }
                else
                {
                    outputComboBox.Items.Add(node.Value<string>("Desc") + " :Output");
                }
            }
            inputComboBox.Text = "select";
            outputComboBox.Text = "select";
            List<JToken> synaps = bibCol.dynRoot["brain"]["Synapses"].ToList();
            //List<Synaps> synaps = bibCol.Root.brain.Synapses;
            foreach (JToken synap in synaps)
            {
                AddSynapsePanel(synap);
            }
        }
        
        private void AddSynapsePanel(JToken synap)
        {
            List<JToken> relatedNodes = new List<JToken>();
            relatedNodes.Add(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(synap.Value<int>("NodeIn"))).First());
            relatedNodes.Add(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(synap.Value<int>("NodeOut"))).First());
            FlowLayoutPanel p = new FlowLayoutPanel();
            p.Anchor = AnchorStyles.Top;
            TextBox cbIn = new TextBox();
            TextBox cbOut = new TextBox();
            TextBox nud = new TextBox();
            Button del = new Button();
            cbIn.ReadOnly = true;
            cbOut.ReadOnly = true;
            nud.ReadOnly = true;
            Tuple<int, int>  tup = Tuple.Create(synap.Value<int>("NodeOut"), synap.Value<int>("NodeIn"));
            del.Tag = tup;
            del.Text = "Remove";
            del.Click += synapseDelete;
            cbIn.Text = synap.Value<int>("NodeIn") < 43 ? relatedNodes[0].Value<String>("Desc") : relatedNodes[0].Value<String>("Desc") + " :" + relatedNodes[0].Value<String>("TypeName");
            cbIn.Tag = relatedNodes[0].Value<int>("Index");
            cbOut.Text = synap.Value<int>("NodeOut") < 43 ? relatedNodes[1].Value<String>("Desc") : relatedNodes[1].Value<String>("Desc") + " :" + relatedNodes[1].Value<String>("TypeName");
            cbOut.Tag = relatedNodes[1].Value<int>("Index");
            nud.Text = synap.Value<string>("Weight");
            nud.Tag = synap.Value<bool>("en");
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
            //remove the synapse from bibCol
            Tuple<int,int> tagTup =  (Tuple<int,int>)((Button)sender).Tag;
            try
            {
                JToken syn = bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == tagTup.Item1 && x.Value<int>("NodeIn") == tagTup.Item2).First();

                //bibCol.Root.brain.Synapses.Remove(syn);
                bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Equals(syn)).First().Remove();
                void removeFromNodesAndDropDown(JToken n)
                {
                    if (n.Value<string>("Desc").Contains("Hidden"))
                    {
                        bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Equals(n)).First().Remove();
                        inputComboBox.Items.Remove(n.Value<string>("Desc") + " :" + n.Value<string>("TypeName"));
                        outputComboBox.Items.Remove(n.Value<string>("Desc") + " :" + n.Value<string>("TypeName"));
                    }

                }
                if (bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut").Equals(syn.Value<int>("NodeIn")) || x.Value<int>("NodeIn").Equals(syn.Value<int>("NodeIn"))).Count() == 0) removeFromNodesAndDropDown(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(syn.Value<int>("NodeIn"))).First());
                if (bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut").Equals(syn.Value<int>("NodeOut")) || x.Value<int>("NodeIn").Equals(syn.Value<int>("NodeOut"))).Count() == 0) removeFromNodesAndDropDown(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(syn.Value<int>("NodeOut"))).First());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //if (bibCol.Root.brain.Synapses.Find(x => x.NodeOut == syn.NodeIn || x.NodeIn == syn.NodeIn) == null) removeFromNodesAndDropDown(bibCol.Root.brain.Nodes.Find(x => x.Index == syn.NodeIn));
            //if (bibCol.Root.brain.Synapses.Find(x => x.NodeOut == syn.NodeOut || x.NodeIn == syn.NodeOut) == null) removeFromNodesAndDropDown(bibCol.Root.brain.Nodes.Find(x => x.Index == syn.NodeOut));
            //retrace
            Trace();
            //remove panel
            BrainEditorPanel.Controls.Remove(((Button)sender).Parent);
        }

        private void Trace()
        {
            ConnectionsTreeView.Nodes.Clear();
            List<string> InputList = new List<string>();
            List<int> NoLoops = new List<int>();
            //start with exit neurons
            //bibCol.Root.brain.Nodes.FindAll(x => x.TypeName != "Input" && !x.Desc.Contains("Hidden")).ForEach(TraceOutputNeurons);
            bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("TypeName") != "Input" && !x.Value<string>("Desc").Contains("Hidden")).ToList().ForEach(TraceOutputNeurons); ;
            //does tasks for before and after tracer
            void TraceOutputNeurons(JToken n)
            {
                InputList.Clear();
                string listtext = string.Empty;
                TreeNode node = new TreeNode();
                NoLoops.Clear();
                Tracer(n, node);
                //change parent node text to display input neurons.
                for (int i = 0; i < InputList.Count; i++)
                {
                    listtext += InputList[i];
                    if (i != InputList.Count - 1) listtext += ", ";

                }
                node.Text = n.Value<string>("Desc") + "  Input Neurons : " + listtext;
                //add node to view is node is not empty
                if (node.Nodes.Count > 0) ConnectionsTreeView.Nodes.Add(node);
            }
           

            //recursive trace function
            void Tracer(JToken n, TreeNode inTreeNode)
            {

                //the neuron being passed in has already been added to the tree.
                //when it reaches the end (input) add it to the list of input neruons for that output, unless its already in there
                if (n.Value<string>("TypeName").Equals("Input") && !InputList.Contains(n.Value<string>("Desc"))) InputList.Add(n.Value<string>("Desc"));
                //make sure the function isnt in an infinite loop
                else if (!NoLoops.Contains(n.Value<int>("Index")))
                {
                    NoLoops.Add(n.Value<int>("Index"));
                    //make list of nodes with synapses going into current neuron, and a parallel list of weights
                    List<JToken> inputNeurons = new List<JToken>();
                    List<double> weight = new List<double>();
                    void recordSynapseData(JToken s)
                    {
                        inputNeurons.Add(((JToken)bibCol.dynRoot)["brain"]["Nodes"].Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).First());
                        weight.Add(s.Value<double>("Weight"));
                    }
                    bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == n.Value<int>("Index")).ToList().ForEach(recordSynapseData);
                    //find inputs, recurse
                    for (int i = 0; i < inputNeurons.Count; i++)
                    {
                        string nodetext;
                        if (inputNeurons[i].Value<string>("Desc").Contains("Hidden")) nodetext = inputNeurons[i].Value<string>("TypeName");
                        else nodetext = inputNeurons[i].Value<string>("Desc");
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode((nodetext + ", strength: " + weight[i]).ToString()))];
                        JToken synapse = bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == n.Value<int>("Index") && x.Value<int>("NodeIn") == inputNeurons[i].Value<int>("Index")).First();
                        Tuple<int, int> tup = Tuple.Create(synapse.Value<int>("NodeOut"), synapse.Value<int>("NodeIn"));
                        childNode.Tag = tup;
                        Tracer(inputNeurons[i], childNode);
                    }

                }
            }
            



        }
        private string ReplaceValuesInBibiteSettings(string json)
        {
            //chars to look for to find where the value starts
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'N', 'f', 't' };
            //chars to look for to find where the value ends
            char[] stops = { ',', '}', ']' };
            foreach (Panel p in editorflow.Controls)
            {
                TextBox box = (TextBox)p.Controls[1];
                string value = Regex.Replace(box.Text, ",", ".");
                if (value.StartsWith('.')) value = 0 + value;
                if (value.Equals("NaN")) value = "NaN\"";
                int propIndex = json.IndexOf('"' + box.Tag.ToString() + '"') + box.Tag.ToString().Length + 2;
                int valIndex = json.IndexOfAny(nums, propIndex);
                int stopRemoving = json.IndexOfAny(stops, valIndex) - valIndex;
                json = json.Remove(valIndex, stopRemoving);
                json = json.Insert(valIndex, value);

            }

            json = Regex.Replace(json, @"\s", "");
            return json;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (File.Exists(bibCol.extractFolder + bibCol.name))
            {
                File.Delete(bibCol.extractFolder + bibCol.name);
            }
            string json = ReplaceValuesInBibiteSettings(bibCol.json);
            bibCol.json = json;
            File.WriteAllText(bibCol.extractFolder + bibCol.name, json);

            if (File.Exists(bibCol.extractFolder + bibCol.name))
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
            inputComboBox.Items.Clear();
            outputComboBox.Items.Clear();
            AddNeuronComboBox.Items.Clear();
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
                if (con.Controls[3].Tag.Equals(ConnectionsTreeView.SelectedNode.Tag))
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
            if (inputComboBox.SelectedIndex != -1 && outputComboBox.SelectedIndex != -1 && outputComboBox.SelectedItem != inputComboBox.SelectedItem)
            {
                JToken s = bibCol.dynRoot["brain"]["Synapses"].FirstOrDefault().DeepClone();
                string input = inputComboBox.SelectedItem.ToString();
                input = input.Remove(input.IndexOf(' '));
                string output = outputComboBox.SelectedItem.ToString();
                output = output.Remove(output.IndexOf(' '));
                s["NodeIn"] = bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("Desc") == input).First().Value<int>("Index");
                s["NodeOut"] = bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("Desc") == output).First().Value<int>("Index");
                s["en"] = true;
                s["Weight"] = double.Parse(strengthUpDown.Value.ToString(), CultureInfo.InvariantCulture);
                s["Inov"] = 0;
                bibCol.dynRoot["brain"]["Synapses"].Last().AddAfterSelf(s);
                AddSynapsePanel(s);
                Trace();
            }
                
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
            List<string> names = Directory.EnumerateFiles(bibCol.extractFolder).ToList();
            string name = "bibite_" + names.Count() + bibCol.name.Substring(bibCol.name.IndexOf('.'));
            int i = names.Count() + 1;
            while (File.Exists(bibCol.extractFolder + name))
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
            bool hasNinout = bibCol.json.Contains("Nin");
            JArray synapses = new JArray();
            NeuralNetEditorNodes nodes = new NeuralNetEditorNodes();
            
            List<JToken> nodesResult = new List<JToken>();
            //static nodes are the input and output neurons that are always present regardless of wether they have synapses going to them

            JArray staticNodes = JArray.Parse("[{\"Type\":0,\"TypeName\":\"Input\",\"Index\":0,\"Inov\":0,\"Desc\":\"Constant\",\"Value\":1.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":1,\"Inov\":0,\"Desc\":\"EnergyRatio\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":2,\"Inov\":0,\"Desc\":\"Maturity\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":3,\"Inov\":0,\"Desc\":\"LifeRatio\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":4,\"Inov\":0,\"Desc\":\"Fullness\",\"Value\":0.9222752,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":5,\"Inov\":0,\"Desc\":\"Speed\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":6,\"Inov\":0,\"Desc\":\"IsGrabbingObjects\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":7,\"Inov\":0,\"Desc\":\"AttackDamage\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":8,\"Inov\":0,\"Desc\":\"BibiteConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":9,\"Inov\":0,\"Desc\":\"BibiteConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":10,\"Inov\":0,\"Desc\":\"NVisibleBibites\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":11,\"Inov\":0,\"Desc\":\"PelletConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":12,\"Inov\":0,\"Desc\":\"PelletConcentrationAngle\",\"Value\":-0.00272769528,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":13,\"Inov\":0,\"Desc\":\"NVisiblePellets\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":14,\"Inov\":0,\"Desc\":\"MeatConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":15,\"Inov\":0,\"Desc\":\"MeatConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":16,\"Inov\":0,\"Desc\":\"NVisibleMeat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":17,\"Inov\":0,\"Desc\":\"ClosestBibiteR\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":18,\"Inov\":0,\"Desc\":\"ClosestBibiteG\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":19,\"Inov\":0,\"Desc\":\"CloestBibiteB\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":20,\"Inov\":0,\"Desc\":\"Tic\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":21,\"Inov\":0,\"Desc\":\"Minute\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":22,\"Inov\":0,\"Desc\":\"TimeAlive\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":23,\"Inov\":0,\"Desc\":\"PheroSense1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":24,\"Inov\":0,\"Desc\":\"PheroSense2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":25,\"Inov\":0,\"Desc\":\"PheroSense3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":26,\"Inov\":0,\"Desc\":\"Phero1Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":27,\"Inov\":0,\"Desc\":\"Phero2Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":28,\"Inov\":0,\"Desc\":\"Phero3Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":29,\"Inov\":0,\"Desc\":\"PheroHeading1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":30,\"Inov\":0,\"Desc\":\"PheroHeading2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":31,\"Inov\":0,\"Desc\":\"PheroHeading3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":32,\"Inov\":0,\"Desc\":\"InfectionRate\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":33,\"Inov\":0,\"Desc\":\"Accelerate\",\"Value\":0.380619019,\"LastInput\":0.40078333,\"LastOutput\":0.380619019},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":34,\"Inov\":0,\"Desc\":\"Rotate\",\"Value\":-0.00270018727,\"LastInput\":-0.00270019379,\"LastOutput\":-0.00270018727},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":35,\"Inov\":0,\"Desc\":\"Herding\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":36,\"Inov\":0,\"Desc\":\"Want2Lay\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":37,\"Inov\":0,\"Desc\":\"Want2Eat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":38,\"Inov\":0,\"Desc\":\"Digestion\",\"Value\":0.84410584,\"LastInput\":1.68910074,\"LastOutput\":0.84410584},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":39,\"Inov\":0,\"Desc\":\"Grab\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":40,\"Inov\":0,\"Desc\":\"ClkReset\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":41,\"Inov\":0,\"Desc\":\"Phere1Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":42,\"Inov\":0,\"Desc\":\"Phere2Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":43,\"Inov\":0,\"Desc\":\"Phere3Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":44,\"Inov\":0,\"Desc\":\"Want2Grow\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":45,\"Inov\":0,\"Desc\":\"Want2Heal\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":46,\"Inov\":0,\"Desc\":\"Want2Attack\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":47,\"Inov\":0,\"Desc\":\"ImmuneSystem\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0}]");
            int staticNodeCount = staticNodes.Count;
            foreach (Control con in BrainEditorPanel.Controls)
            {
                //the nodes associated with each synapse are stored as a tag when generating the panels
                List<JToken> relatedNodes = (List<JToken>)con.Tag;
                Tuple<int, int> tagTup = (Tuple<int, int>)con.Controls[3].Tag;
                JToken s = bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == tagTup.Item1 && x.Value<int>("NodeIn") == tagTup.Item2).First();
                synapses.Add(s);
                relatedNodes[0]["NOut"] = 0;
                relatedNodes[1]["NOut"] = 0;
                relatedNodes[0]["NIn"] = 0;
                relatedNodes[1]["NIn"] = 0;
                if (nodesResult.Find(x => x.Value<int>("Index") == relatedNodes[0].Value<int>("Index")) == null)
                {
                    nodesResult.Add(relatedNodes[0]);
                }
                if (nodesResult.Find(x => x.Value<int>("Index") == relatedNodes[1].Value<int>("Index")) == null)
                {
                    nodesResult.Add(relatedNodes[1]);
                }
            }
            bool complete = false;
            //trim the synapses for only those that serve a function, this doesnt work entirely but its enough for the game to load
            while (!complete)
            {
                int count = synapses.Count;
                List<JToken> remove = new List<JToken>();
                foreach (JToken s in synapses)
                {
                    if (s.Value<int>("NodeIn") >= staticNodeCount && synapses.Where(x => x.Value<int>("NodeOut") == s.Value<int>("NodeIn")).Count() == 0) remove.Add(s);
                    else if (s.Value<int>("NodeOut") >= staticNodeCount && synapses.Where(x => x.Value<int>("NodeIn") == s.Value<int>("NodeOut")).Count() == 0) remove.Add(s);
                }
                remove.ForEach(x => synapses.Remove(x));
                if (synapses.Count == count) complete = true;
            }
            JArray validatedNodes = new JArray();
            //nodesResult.OrderBy(x => x.Index);
            //create a list of nodes that are associated with the trimmed list of synapses
            foreach (JToken s in synapses)
            {
                if (validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).Count() == 0) validatedNodes.Add(nodesResult.Find(x => x.Value<int>("Index") == s.Value<int>("NodeIn")));
                if (validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeOut")).Count() == 0) validatedNodes.Add(nodesResult.Find(x => x.Value<int>("Index") == s.Value<int>("NodeOut")));
            }
            validatedNodes = new JArray(validatedNodes.Children().OrderBy(x => x.Value<int>("Index")));
            //add any missing static nodes
            for (int i = 0; i < staticNodeCount; i++)
            {
                if (validatedNodes.Children().Where(x => x.Value<int>("Index") == i).Count() == 0)
                {
                    validatedNodes.Add(staticNodes[i]);
                }
            }
            validatedNodes = new JArray(validatedNodes.Children().OrderBy(x => x.Value<int>("Index")));
            //first assign the Nodein and Nodeout synapse properties to the List index of their associated node, then assign the index property of the nodes to their list index
            //this ensures theres no gaps in the indecies of the nodes from deleted nodes
            //fix the HiddenX numbers of middle neurons so they are in order
            foreach (JToken s in synapses)
            {
                s["NodeIn"] = validatedNodes.Children().ToList().IndexOf(validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).First());
                s["NodeOut"] = validatedNodes.Children().ToList().IndexOf(validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeOut")).First());

            }
            List<Tuple<JToken, JToken>> tups = new List<Tuple<JToken, JToken>>();
            foreach(JToken tok in validatedNodes.Children())
            {
                tok["Value"] = Regex.Replace(tok.Value<string>("Value"), ",", ".");
                validatedNodes.Where(x => x == tok).First()["Index"] = validatedNodes.ToList<JToken>().IndexOf(tok);
                if(hasNinout && tok["NIn"] == null)
                {
                    tok.Last().AddAfterSelf("\"NIn\" : 0");
                    tok.Last().AddAfterSelf("\"NOut\" : 0");
                }
                if (!hasNinout && tok["NIn"] != null)
                {
                    JToken replacement = new JObject(new JProperty("Type", tok.Value<int>("Type")), new JProperty("TypeName", tok.Value<string>("TypeName")), new JProperty("Index", tok.Value<int>("Index")), new JProperty("Inov", tok.Value<int>("Inov")), new JProperty("Desc", tok.Value<string>("Desc")), new JProperty("Value", tok.Value<double>("Value")), new JProperty("LastInput", tok.Value<double>("LastInput")), new JProperty("LastOutput", tok.Value<double>("LastOutput")));
                    tups.Add(Tuple.Create(tok, replacement));
                }
            }
            foreach(Tuple<JToken,JToken> tup in tups)
            {
                validatedNodes.Where(x => x == tup.Item1).First().Replace(tup.Item2);
            }
            foreach (JToken tok in validatedNodes.Children().Where(x => x.Value<string>("Desc").Contains("Hidden")))
            {
                validatedNodes.Children().Where(x => x == tok).First()["Desc"] = "Hidden" + (tok.Value<int>("Index") - staticNodeCount).ToString();
            }
            if (hasNinout)
            {
                foreach(JToken synapse in synapses)
                {
                    JToken inNode = validatedNodes.Where(x => x.Value<int>("Index") == synapse.Value<int>("NodeIn")).First();
                    JToken outNode = validatedNodes.Where(x => x.Value<int>("Index") == synapse.Value<int>("NodeOut")).First();
                    validatedNodes.Where(x => x == inNode).First()["NIn"] = inNode.Value<int>("NIn") + 1;
                    validatedNodes.Where(x => x == outNode).First()["NOut"] = inNode.Value<int>("NIn") + 1;
                }
            }
            bibCol.dynRoot["brain"]["Nodes"].Replace(validatedNodes);
            bibCol.dynRoot["brain"]["Synapses"].Replace(synapses);
        }
        
        void serializeModelData(string name)
        {
            //begin serializing the data to a json file
            //the version selection is janky, needs to use the version property in settings.json, currently theres only two versions supported and it differentiates via the file typ
            string json = string.Empty;
            json = JsonConvert.SerializeObject(bibCol.dynRoot, new JsonSerializerSettings(){Culture = CultureInfo.InvariantCulture });

            json = ReplaceValuesInBibiteSettings(json);
            bibCol.json = json;
            //confirm file was created
            if (File.Exists(bibCol.extractFolder + name))
            {
                File.Delete(bibCol.extractFolder + name);
            }
            File.WriteAllText(bibCol.extractFolder + name, json);
            if (File.Exists(bibCol.extractFolder + name))
            {
                MessageBox.Show("Saved Sucessfully");
            }
        }
        private void AddNeuronButton_Click(object sender, EventArgs e)
        {
            if (AddNeuronComboBox.SelectedIndex > -1)
            {
                JToken n = bibCol.dynRoot["brain"]["Nodes"].FirstOrDefault().DeepClone();
                n["Type"] = AddNeuronComboBox.SelectedIndex - 1;
                n["TypeName"] = AddNeuronComboBox.SelectedItem.ToString();
                n["Index"] = bibCol.dynRoot["brain"]["Nodes"].Children().Count();
                while (bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(n.Value<int>("Index"))).Count() > 0) n["Index"] = n.Value<int>("Index") + 1;
                int i = n.Value<int>("Index") - bibCol.dynRoot["brain"]["Nodes"].Where(x => !x.Value<string>("Desc").Contains("Hidden")).Count();
                while (bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("Desc") == "Hidden" + i).Count() > 0) i++;
                n["Desc"] = "Hidden" + i;
                bibCol.dynRoot["brain"]["Nodes"].Last().AddAfterSelf(n);
                inputComboBox.Items.Add(n.Value<string>("Desc") + " :" + n.Value<string>("TypeName"));
                outputComboBox.Items.Add(n.Value<string>("Desc") + " :" + n.Value<string>("TypeName"));
            }
        }
    }
}
