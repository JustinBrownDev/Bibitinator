using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Bibitinator.Models;
using Bibitinator.Models.BibiteTracer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Bibitinator.Models.BibiteReflect;

namespace Bibitinator
{
    public partial class BibiteEditor : Form
    {
        BibiteCollection bibCol = null;
        Node[] inNodes = null;
        Node[] outNodes = null;
        public BibiteReflect.Root obj = null;
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
            buildTextboxes();
            Trace();
            splitContainer1.Cursor = Cursors.Arrow;
            this.Text = bibCol.name;
            obj = JsonConvert.DeserializeObject<BibiteReflect.Root>(bibCol.json);
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
            foreach(PropertyInfo prop in typeof(BibiteReflect.Genes1).GetProperties())
            {
                if (prop.PropertyType == typeof(double))
                {
                    Label l = new Label();
                    TextBox t = new TextBox();
                    l.Text = prop.Name;
                    l.Tag = "label";
                    t.Text = prop.GetValue(obj.genes.genes).ToString();
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
            List<Node> nodes = obj.brain.Nodes;
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
                    inputComboBox.Items.Add(node.Desc + " :"+ node.TypeName);
                    outputComboBox.Items.Add(node.Desc + " :" + node.TypeName);
                }
            }
            inputComboBox.Text = "select";
            outputComboBox.Text = "select";
            List<Synaps> synaps = obj.brain.Synapses;
            foreach (Synaps synap in synaps)
            {
                FlowLayoutPanel p = new FlowLayoutPanel();
                p.Anchor = AnchorStyles.Top;
                TextBox cbIn = new TextBox();
                TextBox cbOut = new TextBox();
                TextBox nud = new TextBox();
                Button del = new Button();
                cbIn.ReadOnly = true;
                cbOut.ReadOnly = true;
                nud.ReadOnly = true;
                del.Text = "Remove";
                del.Click += Check_CheckedChanged;
                cbIn.Text = synap.NodeIn < 43 ? nodes[synap.NodeIn].Desc : nodes[synap.NodeIn].Desc + " :" + nodes[synap.NodeIn].TypeName;
                cbIn.Tag = nodes[synap.NodeIn].Index;
                cbOut.Text = synap.NodeOut < 43 ? nodes[synap.NodeOut].Desc : nodes[synap.NodeOut].Desc + " :" + nodes[synap.NodeOut].TypeName;
                cbOut.Tag = nodes[synap.NodeOut].Index;
                nud.Text = synap.Weight.ToString();
                p.Cursor = Cursors.Arrow;
                p.Controls.Add(cbIn);
                p.Controls.Add(cbOut);
                p.Controls.Add(nud);
                p.Controls.Add(del);
                p.Height = cbIn.Height + 10;
                cbIn.Width = 125;
                cbOut.Width = 125;
                p.Parent = BrainEditorPanel;
                p.Tag = BrainEditorPanel.Controls.Count;
                del.Tag = BrainEditorPanel.Controls.Count;
                p.Dock = DockStyle.Top; 
                p.Show();
            }
        }

        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            BrainEditorPanel.Controls.Remove(((Button)sender).Parent);
        }

        private void Trace()
        {
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
                    foreach (BibiteReflect.Synaps s in obj.brain.Synapses)
                    {
                        if (s.NodeOut == n.Index)
                        {
                            inputNeurons.Add(obj.brain.Nodes[s.NodeIn]);
                            weight.Add(s.Weight);
                        }
                    }
                    //find inputs, recurse
                    for (int i = 0; i < inputNeurons.Count; i++)
                    {
                        string nodetext;
                        if (inputNeurons[i].Desc.Contains("Hidden")) nodetext = inputNeurons[i].TypeName;
                        else nodetext = inputNeurons[i].Desc;
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode((nodetext + ", strength: " + weight[i]).ToString()))];
                        Tracer(inputNeurons[i], childNode);
                    }

                }
            }
            //start with exit neurons
            foreach (BibiteReflect.Node n in obj.brain.Nodes)
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
        private void StrenghtUpDown_Changed(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckBox[] controls = (CheckBox[])BrainEditorPanel.Controls.Find("CheckBox", true);
            List<Control> list = new List<Control>();
            foreach (CheckBox control in controls)
            {
                if (control.Checked)
                {
                    BrainEditorPanel.Controls.RemoveByKey(control.Tag.ToString());
                }
            }
        }
    }
}
