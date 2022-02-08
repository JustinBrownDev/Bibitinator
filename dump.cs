using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator
{
    class dump
    {
        /*
         *  private void applyBrainChangesToModel()
        {
            bool hasNinout = bibCol.json.Contains("Nin");
            JArray synapses = new JArray();
            NeuralNetEditorNodes nodes = new NeuralNetEditorNodes();
            //Node[] inputNodes;
            //Node[] outputNodes;
            //if (bibCol.name.Contains(".bb8"))
            //{
            //    inputNodes = nodes.inputNodesNew;
            //    outputNodes = nodes.outputNodesNew;
            //}
            //else
            //{
            //    inputNodes = nodes.inputNodesOld;
            //    outputNodes = nodes.outputNodesOld;
            //}
            List<JToken> nodesResult = new List<JToken>();
            //static nodes are the input and output neurons that are always present regardless of wether they have synapses going to them
            //List<Node> staticNodes = new List<Node>();
            //staticNodes.AddRange(inputNodes);
            //staticNodes.AddRange(outputNodes);
            JArray staticNodes = JArray.Parse("[{\"Type\":0,\"TypeName\":\"Input\",\"Index\":0,\"Inov\":0,\"Desc\":\"Constant\",\"Value\":1.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":1,\"Inov\":0,\"Desc\":\"EnergyRatio\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":2,\"Inov\":0,\"Desc\":\"Maturity\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":3,\"Inov\":0,\"Desc\":\"LifeRatio\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":4,\"Inov\":0,\"Desc\":\"Fullness\",\"Value\":0.9222752,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":5,\"Inov\":0,\"Desc\":\"Speed\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":6,\"Inov\":0,\"Desc\":\"IsGrabbingObjects\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":7,\"Inov\":0,\"Desc\":\"AttackDamage\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":8,\"Inov\":0,\"Desc\":\"BibiteConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":9,\"Inov\":0,\"Desc\":\"BibiteConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":10,\"Inov\":0,\"Desc\":\"NVisibleBibites\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":11,\"Inov\":0,\"Desc\":\"PelletConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":12,\"Inov\":0,\"Desc\":\"PelletConcentrationAngle\",\"Value\":-0.00272769528,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":13,\"Inov\":0,\"Desc\":\"NVisiblePellets\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":14,\"Inov\":0,\"Desc\":\"MeatConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":15,\"Inov\":0,\"Desc\":\"MeatConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":16,\"Inov\":0,\"Desc\":\"NVisibleMeat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":17,\"Inov\":0,\"Desc\":\"ClosestBibiteR\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":18,\"Inov\":0,\"Desc\":\"ClosestBibiteG\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":19,\"Inov\":0,\"Desc\":\"CloestBibiteB\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":20,\"Inov\":0,\"Desc\":\"Tic\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":21,\"Inov\":0,\"Desc\":\"Minute\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":22,\"Inov\":0,\"Desc\":\"TimeAlive\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":23,\"Inov\":0,\"Desc\":\"PheroSense1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":24,\"Inov\":0,\"Desc\":\"PheroSense2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":25,\"Inov\":0,\"Desc\":\"PheroSense3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":26,\"Inov\":0,\"Desc\":\"Phero1Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":27,\"Inov\":0,\"Desc\":\"Phero2Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":28,\"Inov\":0,\"Desc\":\"Phero3Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":29,\"Inov\":0,\"Desc\":\"PheroHeading1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":30,\"Inov\":0,\"Desc\":\"PheroHeading2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":31,\"Inov\":0,\"Desc\":\"PheroHeading3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":32,\"Inov\":0,\"Desc\":\"InfectionRate\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":33,\"Inov\":0,\"Desc\":\"Accelerate\",\"Value\":0.380619019,\"LastInput\":0.40078333,\"LastOutput\":0.380619019},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":34,\"Inov\":0,\"Desc\":\"Rotate\",\"Value\":-0.00270018727,\"LastInput\":-0.00270019379,\"LastOutput\":-0.00270018727},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":35,\"Inov\":0,\"Desc\":\"Herding\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":36,\"Inov\":0,\"Desc\":\"Want2Lay\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":37,\"Inov\":0,\"Desc\":\"Want2Eat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":38,\"Inov\":0,\"Desc\":\"Digestion\",\"Value\":0.84410584,\"LastInput\":1.68910074,\"LastOutput\":0.84410584},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":39,\"Inov\":0,\"Desc\":\"Grab\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":40,\"Inov\":0,\"Desc\":\"ClkReset\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":41,\"Inov\":0,\"Desc\":\"Phere1Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":42,\"Inov\":0,\"Desc\":\"Phere2Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":43,\"Inov\":0,\"Desc\":\"Phere3Out\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":44,\"Inov\":0,\"Desc\":\"Want2Grow\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":45,\"Inov\":0,\"Desc\":\"Want2Heal\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":46,\"Inov\":0,\"Desc\":\"Want2Attack\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":47,\"Inov\":0,\"Desc\":\"ImmuneSystem\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0}]");
            int staticNodeCount = staticNodes.Count;
            foreach (Control con in BrainEditorPanel.Controls)
            {
                //the nodes associated with each synapse are stored as a tag when generating the panels
                List<JToken> relatedNodes = (List<JToken>)con.Tag;
                JToken s = (JToken)con.Controls[3].Tag;
                //Synaps s = new Synaps();
                //s.NodeIn = Convert.ToInt32(con.Controls[0].Tag);
                //s.NodeOut = Convert.ToInt32(con.Controls[1].Tag);
                //s.Weight = Double.Parse(con.Controls[2].Text, CultureInfo.InvariantCulture);
                //s.Inov = 0;
                //s.En = true;
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
                s["NodeIn"] = validatedNodes.Children().ToList().IndexOf(validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).Single());
                s["NodeOut"] = validatedNodes.Children().ToList().IndexOf(validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeOut")).Single());

            }
            List<Tuple<JToken, JToken>> tups = new List<Tuple<JToken, JToken>>();
            foreach(JToken tok in validatedNodes.Children())
            { 
                validatedNodes.Where(x => x == tok).Single()["Index"] = validatedNodes.ToList<JToken>().IndexOf(tok);
                if(hasNinout && tok["NIn"] == null)
                {
                    tok.Append("\"NIn\" : 0");
                    tok.Append("\"NOut\" : 0");
                }
                if (!hasNinout && tok["NIn"] != null)
                {
                    JToken replacement = new JObject(new JProperty("Type", tok.Value<int>("Type")), new JProperty("TypeName", tok.Value<string>("TypeName")), new JProperty("Index", tok.Value<int>("Index")), new JProperty("Inov", tok.Value<int>("Inov")), new JProperty("Desc", tok.Value<string>("Desc")), new JProperty("Value", tok.Value<double>("Value")), new JProperty("LastInput", tok.Value<double>("LastInput")), new JProperty("LastOutput", tok.Value<double>("LastOutput")));
                    tups.Add(Tuple.Create(tok, replacement));
                }
            }
            foreach(Tuple<JToken,JToken> tup in tups)
            {
                validatedNodes.Where(x => x == tup.Item1).Single().Replace(tup.Item2);
            }
            foreach (JToken tok in validatedNodes.Children().Where(x => x.Value<string>("Desc").Contains("Hidden")))
            {
                validatedNodes.Children().Where(x => x == tok).Single()["Desc"] = "Hidden" + (tok.Value<int>("Index") - staticNodeCount).ToString();
            }
            //validatedNodes.Children().Where(x => x.Value<string>("Desc").Contains("Hidden")).ForEach(x => x["Desc"] = "Hidden" + (x.Value<int>("Index") - staticNodeCount).ToString());
            //assign validated data to the root model
            //JToken nodeToken = null;
            //JToken synToken = null;
            //foreach(JToken n in validatedNodes)
            //{
            //    nodeToken = (JToken)JsonConvert.DeserializeObject(validatedNodes.ToString());
            //    //nodeToken.Append<JToken>(n);
            //}
            //foreach(JToken s in synapses)
            //{
            //    synToken.Append<JToken>(s);
            //}
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
            //if (bibCol.name.Contains(".bb8"))
            //{
            //    json = JsonConvert.SerializeObject(bibCol.Root, new JsonSerializerSettings()
            //    { ContractResolver = new IgnorePropertiesResolver(new[] { "NIn", "NOut", "maxHealth", "MaxEnergy", "d1Size", "isWow" }), Culture = CultureInfo.InvariantCulture });
            //}
            //else if (bibCol.name.Contains(".json"))
            //{
        //    json = JsonConvert.SerializeObject(bibCol.Root, new JsonSerializerSettings()
        //    { ContractResolver = new IgnorePropertiesResolver(new[] { "Fullness", "Phero1Heading", "Phero2Heading", "Phero3Heading", "GrowthScale", "GrowthMaturityFactor", "GrowthMaturityExponent", "biteProgress", "stomach" }), Culture = CultureInfo.InvariantCulture });
        //}
            json = JsonConvert.SerializeObject(bibCol.dynRoot, new JsonSerializerSettings(){Culture = CultureInfo.InvariantCulture });

            //if (bibCol.json.Contains("version"))
            //{
            //    int versionLength = bibCol.json.LastIndexOf('"') - (bibCol.json.IndexOf("version") - 1);
            //    string version = bibCol.json.Substring(bibCol.json.IndexOf("version") - 1, versionLength + 1);
            //    json = json.Insert(json.LastIndexOf('}'), "," + version);
            //}
            //copy postition to new json file
            //int positionLocation = bibCol.json.IndexOf("position") - 1;
            //int positionLength = bibCol.json.IndexOf(']', positionLocation) - positionLocation;
            //string position = bibCol.json.Substring(positionLocation, positionLength + 1);
            //position = Regex.Replace(position, @"\s", "");
            //json = json.Replace("\"_position\":null", position);
            //set genes in new json file

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
         */
        //if (inputComboBox.SelectedIndex != -1 && outputComboBox.SelectedIndex != -1 && outputComboBox.SelectedItem != inputComboBox.SelectedItem)
        //{
        //    Synaps s = new Synaps();
        //    string input = inputComboBox.SelectedItem.ToString();
        //    input = input.Remove(input.IndexOf(' '));
        //    string output = outputComboBox.SelectedItem.ToString();
        //    output = output.Remove(output.IndexOf(' '));
        //    s.NodeIn = bibCol.Root.brain.Nodes.Find(x => x.Desc == input).Index;
        //    s.NodeOut = bibCol.Root.brain.Nodes.Find(x => x.Desc == output).Index;
        //    s.En = true;
        //    s.Weight = (double)strengthUpDown.Value;
        //    s.Inov = 0;
        //    bibCol.Root.brain.Synapses.Add(s);
        //    AddSynapsePanel(JsonConvert.SerializeObject(s));
        //}   
        /*public void buildTextboxesOld()
        {
            //build genes editor
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
            //build nodes dropdowns
            List<Node> nodes = bibCol.Root.brain.Nodes;
            foreach(Node node in net.middleNodes)
            {
                AddNeuronComboBox.Items.Add(node.TypeName);
            }
            foreach (Node node in nodes)
            {
                if (node.TypeName == "Input")
                {
                    inputComboBox.Items.Add(node.Desc + " :Input");
                }
                else if (node.Desc.Contains("Hidden"))
                {
                    inputComboBox.Items.Add(node.Desc + " :" + node.TypeName);
                    outputComboBox.Items.Add(node.Desc + " :" + node.TypeName);
                }
                else
                {
                    outputComboBox.Items.Add(node.Desc + " :Output");       
                }
            }
            inputComboBox.Text = "select";
            outputComboBox.Text = "select";
            List<Synaps> synaps = bibCol.Root.brain.Synapses;
            foreach (Synaps synap in synaps)
            {
                AddSynapsePanel(synap);
            }
        }*/
        /*private void AddSynapsePanelOld(BibiteReflect.Synaps synap)
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
            del.Tag = synap;
            del.Text = "Remove";
            del.Click += synapseDelete;
            cbIn.Text = synap.NodeIn < 43 ? relatedNodes[0].Desc : relatedNodes[0].Desc + " :" + relatedNodes[0].TypeName;
            cbIn.Tag = relatedNodes[0].Index;
            cbOut.Text = synap.NodeOut < 43 ? relatedNodes[1].Desc : relatedNodes[1].Desc + " :" + relatedNodes[1].TypeName;
            cbOut.Tag = relatedNodes[1].Index;
            nud.Text = synap.Weight.ToString();
            nud.Tag = synap.En;
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
        } */
        /*void TraceOutputNeuronsOld(BibiteReflect.Node n)
           {
               InputList.Clear();
               string listtext = string.Empty;
               TreeNode node = new TreeNode();
               NoLoops.Clear();
               TracerOld(n, node);
               //change parent node text to display input neurons.
               for (int i = 0; i < InputList.Count; i++)
               {
                   listtext += InputList[i];
                   if (i != InputList.Count - 1) listtext += ", ";

               }
               node.Text = n.Desc + "  Input Neurons : " + listtext;
               //add node to view is node is not empty
               if (node.Nodes.Count > 0) ConnectionsTreeView.Nodes.Add(node);
           }*/
        /*void TracerOld(BibiteReflect.Node n, TreeNode inTreeNode)
            {

                //the neuron being passed in has already been added to the tree.
                //when it reaches the end (input) add it to the list of input neruons for that output, unless its already in there
                if (n.TypeName.Equals("Input") && !InputList.Contains(n.Desc)) InputList.Add(n.Desc);
                //make sure the function isnt in an infinite loop
                else if (!NoLoops.Contains(n.Index))
                {
                    NoLoops.Add(n.Index);
                    //make list of nodes with synapses going into current neuron, and a parallel list of weights
                    List<BibiteReflect.Node> inputNeurons = new List<BibiteReflect.Node>();
                    List<double> weight = new List<double>();
                    void recordSynapseData(Synaps s)
                    {
                        inputNeurons.Add(bibCol.Root.brain.Nodes.Find(x => x.Index == s.NodeIn));
                        weight.Add(s.Weight);
                    }
                    bibCol.Root.brain.Synapses.FindAll(x => x.NodeOut == n.Index).ForEach(recordSynapseData);
                    //find inputs, recurse
                    for (int i = 0; i < inputNeurons.Count; i++)
                    {
                        string nodetext;
                        if (inputNeurons[i].Desc.Contains("Hidden")) nodetext = inputNeurons[i].TypeName;
                        else nodetext = inputNeurons[i].Desc;
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode((nodetext + ", strength: " + weight[i]).ToString()))];
                        childNode.Tag = bibCol.Root.brain.Synapses.Find(x => x.NodeOut == n.Index && x.NodeIn == inputNeurons[i].Index);
                        TracerOld(inputNeurons[i], childNode);
                    }

                }
            }*/
        //private void applyBrainChangesToModelOld()
        //{
        //    List<Synaps> synapses = new List<Synaps>();
        //    NeuralNetEditorNodes nodes = new NeuralNetEditorNodes();
        //    Node[] inputNodes;
        //    Node[] outputNodes;
        //    if (bibCol.name.Contains(".bb8"))
        //    {
        //        inputNodes = nodes.inputNodesNew;
        //        outputNodes = nodes.outputNodesNew;
        //    }
        //    else
        //    {
        //        inputNodes = nodes.inputNodesOld;
        //        outputNodes = nodes.outputNodesOld;
        //    }
        //    List<Node> nodesResult = new List<Node>();
        //    //static nodes are the input and output neurons that are always present regardless of wether they have synapses going to them
        //    List<Node> staticNodes = new List<Node>();
        //    staticNodes.AddRange(inputNodes);
        //    staticNodes.AddRange(outputNodes);

        //    int staticNodeCount = staticNodes.Count;
        //    foreach (Control con in BrainEditorPanel.Controls)
        //    {
        //        //the nodes associated with each synapse are stored as a tag when generating the panels
        //        List<Node> relatedNodes = (List<Node>)con.Tag;
        //        Synaps s = (Synaps)con.Controls[3].Tag;
        //        //Synaps s = new Synaps();
        //        //s.NodeIn = Convert.ToInt32(con.Controls[0].Tag);
        //        //s.NodeOut = Convert.ToInt32(con.Controls[1].Tag);
        //        //s.Weight = Double.Parse(con.Controls[2].Text, CultureInfo.InvariantCulture);
        //        //s.Inov = 0;
        //        //s.En = true;
        //        synapses.Add(s);
        //        relatedNodes[0].NOut = 0;
        //        relatedNodes[1].NOut = 0;
        //        relatedNodes[0].NIn = 0;
        //        relatedNodes[1].NIn = 0;
        //        if (nodesResult.Find(x => x.Index == relatedNodes[0].Index) == null)
        //        {
        //            nodesResult.Add(relatedNodes[0]);
        //        }
        //        if (nodesResult.Find(x => x.Index == relatedNodes[1].Index) == null)
        //        {
        //            nodesResult.Add(relatedNodes[1]);
        //        }
        //    }
        //    bool complete = false;
        //    //trim the synapses for only those that serve a function, this doesnt work entirely but its enough for the game to load
        //    while (!complete)
        //    {
        //        int count = synapses.Count;
        //        foreach (Synaps s in synapses)
        //        {
        //            if (s.NodeIn >= staticNodeCount && synapses.FindAll(x => x.NodeOut == s.NodeIn) == null) synapses.Remove(s);
        //            else if (s.NodeOut >= staticNodeCount && synapses.FindAll(x => x.NodeIn == s.NodeOut) == null) synapses.Remove(s);
        //        }
        //        if (synapses.Count == count) complete = true;
        //    }
        //    List<Node> validatedNodes = new List<Node>();
        //    //nodesResult.OrderBy(x => x.Index);
        //    //create a list of nodes that are associated with the trimmed list of synapses
        //    foreach (Synaps s in synapses)
        //    {
        //        if (validatedNodes.Find(x => x.Index == s.NodeIn) == null) validatedNodes.Add(nodesResult.Find(x => x.Index == s.NodeIn));
        //        if (validatedNodes.Find(x => x.Index == s.NodeOut) == null) validatedNodes.Add(nodesResult.Find(x => x.Index == s.NodeOut));
        //    }
        //    validatedNodes = validatedNodes.OrderBy(x => x.Index).ToList();
        //    //add any missing static nodes
        //    for (int i = 0; i < staticNodeCount; i++)
        //    {
        //        if (validatedNodes.Find(x => x.Index == i) == null)
        //        {
        //            validatedNodes.Add(staticNodes[i]);
        //        }
        //    }
        //    validatedNodes = validatedNodes.OrderBy(x => x.Index).ToList();
        //    //first assign the Nodein and Nodeout synapse properties to the List index of their associated node, then assign the index property of the nodes to their list index
        //    //this ensures theres no gaps in the indecies of the nodes from deleted nodes
        //    //fix the HiddenX numbers of middle neurons so they are in order
        //    foreach (Synaps s in synapses)
        //    {
        //        s.NodeIn = validatedNodes.IndexOf(validatedNodes.Find(x => x.Index == s.NodeIn));
        //        s.NodeOut = validatedNodes.IndexOf(validatedNodes.Find(x => x.Index == s.NodeOut));
        //    }
        //    validatedNodes.ForEach(x => x.Index = validatedNodes.IndexOf(x));
        //    validatedNodes.FindAll(x => x.Desc.Contains("Hidden")).ForEach(x => x.Desc = "Hidden" + (x.Index - staticNodeCount).ToString());
        //    //assign validated data to the root model
        //    bibCol.Root.brain.Nodes = validatedNodes;
        //    bibCol.Root.brain.Synapses = synapses;
        //}
        //private void populateSettingsOld()
        //{
        //    string settingsFilePath = extractTo + "\\settings" + settingsExtension;
        //    if (File.Exists(settingsFilePath))
        //    {
        //        JObject jobj = new JObject();
        //        using (StreamReader r = new StreamReader(settingsFilePath))
        //        {
        //            var json = r.ReadToEnd();
        //            jobj = JObject.Parse(json);
        //            worldsettingsJson = json.ToString();
        //        }
        //        foreach (var property in jobj.Properties())
        //        {
        //            if (property.Name != "materials" && property.Name != "MeatSettings")
        //            {
        //                Label l = new Label();
        //                TextBox t = new TextBox();
        //                l.Text = property.Name;
        //                int first = property.Value.ToString().IndexOf("\"Value\":") + 9;
        //                int length = property.Value.ToString().IndexOf('}') - first;
        //                t.Text = property.Value.ToString().Substring(first, length);
        //                t.Tag = property.Name;
        //                Panel p = new Panel();
        //                l.Width = 200;
        //                t.Width = 150;
        //                p.Width = l.Width + t.Width;
        //                p.Height = l.Height + t.Height;
        //                l.Parent = p;
        //                l.Dock = DockStyle.Left;
        //                t.Parent = p;
        //                t.Dock = DockStyle.Right;
        //                p.Parent = worldSettingsFlow;
        //            }
        //        }
        //    }
        //}
        //private void TTrace()
        //{

        //public void buildTextboxes()
        //{
        //    JsonSerializerSettings settings = new JsonSerializerSettings();
        //    settings.TypeNameHandling = TypeNameHandling.All;
        //    settings.FloatFormatHandling = FloatFormatHandling.String;
        //    Root obj = JsonConvert.DeserializeObject<Root>(bibCol.json);
        //    foreach (PropertyInfo prop in typeof(Genes1).GetProperties())
        //    {
        //        if (prop.PropertyType == typeof(double))
        //        {
        //            Label l = new Label();
        //            TextBox t = new TextBox();
        //            l.Text = prop.Name;
        //            t.Text = prop.GetValue(obj.genes.genes).ToString();
        //            t.Tag = prop.Name;
        //            Panel p = new Panel();
        //            p.Width = l.Width + t.Width;
        //            p.Height = l.Height + t.Height;
        //            l.Parent = p;
        //            l.Dock = DockStyle.Left;
        //            t.Parent = p;
        //            t.Dock = DockStyle.Right;
        //            p.Parent = editorflow;
        //        }
        //    }
        //    for (int i = 0; i < propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes.Count; i++)
        //    {
        //        Label l = new Label();
        //        TextBox t = new TextBox();
        //        l.Text = propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes[i].Text.Substring(0, propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes[i].Text.IndexOf(':'));
        //        t.Text = propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes[i].Text.Substring(propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes[i].Text.IndexOf(':') + 3);
        //        t.Tag = propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes[i].Text.Substring(0, propertiesTree.Nodes[0].Nodes[2].Nodes[0].Nodes[i].Text.IndexOf(':'));
        //        Panel p = new Panel();
        //        p.Width = l.Width + t.Width;
        //        p.Height = l.Height + t.Height;
        //        l.Parent = p;
        //        l.Dock = DockStyle.Left;
        //        t.Parent = p;
        //        t.Dock = DockStyle.Right;
        //        p.Parent = editorflow;
        //    }
        //    for (int i = 1; i < 7; i++)
        //    {
        //        for (int n = 0; n < propertiesTree.Nodes[0].Nodes[i].Nodes.Count; n++)
        //        {
        //            if (propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes.Count < 3)
        //            {
        //                if (i != 3 || n != 1 || propertiesTree.Nodes[0].Nodes[1].Nodes.Count != 5)
        //                {
        //                    for (int k = 0; k < propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes.Count; k++)
        //                    {
        //                        Label l = new Label();
        //                        TextBox t = new TextBox();
        //                        if (propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Nodes.Count > 0)
        //                        {
        //                            l.Text = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.Substring(0, propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.IndexOf(':'));
        //                            t.Tag = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.Substring(0, propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.IndexOf(':'));
        //                            t.Text = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Nodes[0].Text;
        //                        }
        //                        else
        //                        {
        //                            if (propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text.Contains(':'))
        //                            {
        //                                l.Text = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text.Substring(0, propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text.IndexOf(':'));
        //                                t.Tag = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text.Substring(0, propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text.IndexOf(':'));
        //                            }
        //                            else
        //                            {
        //                                l.Text = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text + k;
        //                                t.Tag = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Text;
        //                            }
        //                            if (propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.Contains(':'))
        //                            {
        //                                t.Text = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.Substring(propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text.IndexOf(':') + 3);
        //                            }
        //                            else
        //                            {
        //                                t.Text = propertiesTree.Nodes[0].Nodes[i].Nodes[n].Nodes[k].Text;
        //                            }
        //                        }
        //                        Panel p = new Panel();
        //                        p.Width = l.Width + t.Width;
        //                        p.Height = l.Height + t.Height;
        //                        l.Parent = p;
        //                        l.Dock = DockStyle.Left;
        //                        t.Parent = p;
        //                        t.Dock = DockStyle.Right;
        //                        p.Parent = editorflow;
        //                        if (!t.Text.Equals("NaN"))
        //                        {
        //                            t.Text = t.Text.ToLower();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //    List<synapse> synList = new List<synapse>();
        //    //old vs new version fix
        //    Int16 neuronModifier = 0;
        //    Int16 lastInputIndex = 28;
        //    Int16 LastOutputIndex = 43;
        //    if (propertiesTree.Nodes[0].Nodes[6].Nodes[2].Nodes[0].Nodes.Count == 8)
        //    {
        //        neuronModifier = 2;
        //        lastInputIndex = 32;
        //        LastOutputIndex = 47;
        //    }
        //    //Create list of neurons
        //    List<neuron> neurons = new List<neuron>();
        //    for (int i = 0; i < propertiesTree.Nodes[0].Nodes[6].Nodes[2].Nodes.Count; i++)
        //    {
        //        neuron n = new neuron();

        //        n.index = Convert.ToInt16(propertiesTree.Nodes[0].Nodes[6].Nodes[2].Nodes[i].Nodes[2].Nodes[0].Text);
        //        n.type = propertiesTree.Nodes[0].Nodes[6].Nodes[2].Nodes[i].Nodes[(6 - neuronModifier)].Nodes[0].Text;
        //        n.value = Decimal.Parse(propertiesTree.Nodes[0].Nodes[6].Nodes[2].Nodes[i].Nodes[(7 - neuronModifier)].Nodes[0].Text, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
        //        if (n.index <= lastInputIndex)
        //        {
        //            n.isEntrance = true;
        //            n.isExit = false;
        //        }
        //        else if (n.index <= LastOutputIndex)
        //        {
        //            n.isExit = true;
        //            n.isEntrance = false;
        //        }
        //        else
        //        {
        //            n.type = propertiesTree.Nodes[0].Nodes[6].Nodes[2].Nodes[i].Nodes[1].Nodes[0].Text;
        //            n.isEntrance = false;
        //            n.isExit = false;
        //        }
        //        neurons.Add(n);
        //    }

        //    //Create list of synapses
        //    for (int i = 0; i < propertiesTree.Nodes[0].Nodes[6].Nodes[3].Nodes.Count; i++)
        //    {
        //        synapse s = new synapse();
        //        s.inNode = Convert.ToInt16(propertiesTree.Nodes[0].Nodes[6].Nodes[3].Nodes[i].Nodes[1].Nodes[0].Text);
        //        s.outNode = Convert.ToInt16(propertiesTree.Nodes[0].Nodes[6].Nodes[3].Nodes[i].Nodes[2].Nodes[0].Text);
        //        if (propertiesTree.Nodes[0].Nodes[6].Nodes[3].Nodes[i].Nodes[3].Nodes[0].Text.Contains("E"))
        //        {
        //            s.weight = Decimal.Parse(propertiesTree.Nodes[0].Nodes[6].Nodes[3].Nodes[i].Nodes[3].Nodes[0].Text, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
        //        }
        //        else
        //        {
        //            s.weight = Convert.ToDecimal(propertiesTree.Nodes[0].Nodes[6].Nodes[3].Nodes[i].Nodes[3].Nodes[0].Text);
        //        }
        //        //Add Synapses to applicable neurons
        //        if (neurons[s.inNode].output == null) neurons[s.inNode].output = new List<synapse>();
        //        neurons[s.inNode].output.Add(s);
        //        if (neurons[s.outNode].input == null) neurons[s.outNode].input = new List<synapse>();
        //        neurons[s.outNode].input.Add(s);
        //        synList.Add(s);
        //    }
        //    List<string> InputList = new List<string>();
        //    void Tracer(neuron n, TreeNode inTreeNode)
        //    {
        //        //the neuron being passed in has already been added to the dataset.
        //        if (n.isEntrance && !InputList.Contains(n.type)) InputList.Add(n.type);
        //        else
        //        {
        //            //make list nodes with synapses going into current neuron
        //            List<neuron> inputNeurons = new List<neuron>();
        //            List<decimal> weight = new List<decimal>();
        //            foreach (synapse s in synList)
        //            {
        //                if (s.outNode == n.index)
        //                {
        //                    inputNeurons.Add(neurons[s.inNode]);
        //                    weight.Add(s.weight);
        //                }
        //            }
        //            //find inputs, recurse
        //            for (int i = 0; i < inputNeurons.Count; i++)
        //            {
        //                var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode((inputNeurons[i].type + ", strength: " + weight[i]).ToString()))];
        //                Tracer(inputNeurons[i], childNode);
        //            }

        //        }
        //    }
        //    //start with exit neurons
        //    foreach (neuron n in neurons)
        //    {
        //        if (n.input != null && n.isExit && n.input.Count > 0)
        //        {
        //            InputList.Clear();
        //            string listtext = string.Empty;
        //            TreeNode node = new TreeNode();
        //            Tracer(n, node);
        //            for (int i = 0; i < InputList.Count; i++)
        //            {
        //                listtext += InputList[i];
        //                if (i != InputList.Count - 1) listtext += ", ";

        //            }
        //            node.Text = n.type + "  Input Neurons : " + listtext;
        //            ConnectionsTreeView.Nodes.Add(node);

        //        }
        //    }
        //}
        /*   private void DisposeWorkers()
           {
               backgroundWorker1.Dispose();
               backgroundWorker2.Dispose();
           }
           //Initiate Json to Tree infrastructure 
           private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
           {
               BackgroundWorker worker = sender as BackgroundWorker;
               e.Result = populateWithJson((string)e.Argument, worker, e);
           }
           //Initiate Synapse Tracer
           private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
           {
               BackgroundWorker worker = sender as BackgroundWorker;
               e.Result = Trace((TracerInputModel)e.Argument, worker, e);
           }
           //Completed Json, when Done with all bibites, start tracing synapses
           private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
           {
               // First, handle the case where an exception was thrown.
               if (e.Error != null)
               {
                   MessageBox.Show(e.Error.Message);
               }
               else if (e.Result != null)
               {
                   //Raw Json & Tree to page
                   JsonModel j = (JsonModel)e.Result;
                   jsonTextTabControl.TabPages.Add(j.tc);
                   bibiteTree.Nodes.Add(j.tv);
                   //Run Tracer on that tree
                   TracerInputModel tim = new TracerInputModel();
                   tim.name = bibiteTree.Nodes[treeIndex].Text;
                   tim.bibiteBrainTree = bibiteTree.Nodes[treeIndex].Nodes[6];
                   backgroundWorker2.RunWorkerAsync(tim);
               }
               else
               {
                   browseButton.Enabled = true;
                   tabControl1.Enabled = true;
               }
           }
           //completed tracing synapses
           private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
           {
               // First, handle the case where an exception was thrown.
               if (e.Error != null)
               {
                   MessageBox.Show(e.Error.Message);
               }
               else if (e.Result != null)
               {
                   brain b = (brain)e.Result;
                   TabPage tab = new TabPage();
                   tab.Text = b.name;
                   TreeView tv = new TreeView();
                   tv.Nodes.Add(b.brainTree);
                   tv.Parent = tab;
                   tv.Dock = DockStyle.Fill;
                   tv.Nodes[0].Expand();
                   ConnectionsTabControl.TabPages.Add(tab);
                   if (treeIndex == (bibiteListView.Items.Count - 1))
                   {
                       treeIndex = 0;
                       Cursor.Current = Cursors.Default;
                       browseButton.Enabled = true;
                       tabControl1.Enabled = true;
                       DisposeWorkers();
                   }
                   else
                   {

                       treeIndex++;
                       backgroundWorker1.RunWorkerAsync(bibiteListView.Items[treeIndex].Text);
                   }
               }
               else
               {
                   browseButton.Enabled = true;
                   tabControl1.Enabled = true;
               }
           }
           //Json Deconstructor

           //Synapse Tracer V2.1
           private brain Trace(TracerInputModel data, BackgroundWorker worker, DoWorkEventArgs e)
           {
               brain model = new brain();
               model.brainTree = new TreeNode();
               model.name = data.name;
               List<synapse> synList = new List<synapse>();
               try
               {
                   //old vs new version fix
                   Int16 neuronModifier = 0;
                   Int16 lastInputIndex = 28;
                   Int16 LastOutputIndex = 43;
                   if (data.bibiteBrainTree.Nodes[2].Nodes[0].Nodes.Count == 8)
                   {
                       neuronModifier = 2;
                       lastInputIndex = 32;
                       LastOutputIndex = 47;
                   }
                   //Create list of neurons
                   for (int i = 0; i < data.bibiteBrainTree.Nodes[2].Nodes.Count; i++)
                   {
                       neuron n = new neuron();

                       n.index = Convert.ToInt16(data.bibiteBrainTree.Nodes[2].Nodes[i].Nodes[2].Nodes[0].Text);
                       n.type = data.bibiteBrainTree.Nodes[2].Nodes[i].Nodes[(6 - neuronModifier)].Nodes[0].Text;
                       n.value = Decimal.Parse(data.bibiteBrainTree.Nodes[2].Nodes[i].Nodes[(7 - neuronModifier)].Nodes[0].Text, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                       if (n.index <= lastInputIndex)
                       {
                           n.isEntrance = true;
                           n.isExit = false;
                       }
                       else if (n.index <= LastOutputIndex)
                       {
                           n.isExit = true;
                           n.isEntrance = false;
                       }
                       else
                       {
                           n.type = data.bibiteBrainTree.Nodes[2].Nodes[i].Nodes[1].Nodes[0].Text;
                           n.isEntrance = false;
                           n.isExit = false;
                       }
                       if (model.Neurons == null) model.Neurons = new List<neuron>();
                       model.Neurons.Add(n);
                   }

                   //Create list of synapses
                   for (int i = 0; i < data.bibiteBrainTree.Nodes[3].Nodes.Count; i++)
                   {
                       synapse s = new synapse();
                       s.inNode = Convert.ToInt16(data.bibiteBrainTree.Nodes[3].Nodes[i].Nodes[1].Nodes[0].Text);
                       s.outNode = Convert.ToInt16(data.bibiteBrainTree.Nodes[3].Nodes[i].Nodes[2].Nodes[0].Text);
                       if (data.bibiteBrainTree.Nodes[3].Nodes[i].Nodes[3].Nodes[0].Text.Contains("E"))
                       {
                           s.weight = Decimal.Parse(data.bibiteBrainTree.Nodes[3].Nodes[i].Nodes[3].Nodes[0].Text, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                       }
                       else
                       {
                           s.weight = Convert.ToDecimal(data.bibiteBrainTree.Nodes[3].Nodes[i].Nodes[3].Nodes[0].Text);
                       }
                       //Add Synapses to applicable neurons
                       if (model.Neurons[s.inNode].output == null) model.Neurons[s.inNode].output = new List<synapse>();
                       model.Neurons[s.inNode].output.Add(s);
                       if (model.Neurons[s.outNode].input == null) model.Neurons[s.outNode].input = new List<synapse>();
                       model.Neurons[s.outNode].input.Add(s);
                       synList.Add(s);
                   }
                   List<string> InputList = new List<string>();
                   void AddNode(neuron n, TreeNode inTreeNode)
                   {
                       //the neuron being passed in has already been added to the dataset.
                       if (n.isEntrance && !InputList.Contains(n.type)) InputList.Add(n.type);
                       else
                       {
                           //make list nodes with synapses going into current neuron
                           List<neuron> inputNeurons = new List<neuron>();
                           List<decimal> weight = new List<decimal>();
                           foreach (synapse s in synList)
                           {
                               if (s.outNode == n.index)
                               {
                                   inputNeurons.Add(model.Neurons[s.inNode]);
                                   weight.Add(s.weight);
                               }
                           }
                           //find inputs, recurse
                           for (int i = 0; i < inputNeurons.Count; i++)
                           {
                               var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode((inputNeurons[i].type + ", strength: " + weight[i]).ToString()))];
                               AddNode(inputNeurons[i], childNode);
                           }

                       }
                   }
                   //start with exit neurons
                   foreach (neuron n in model.Neurons)
                   {
                       if (n.input != null && n.isExit && n.input.Count > 0)
                       {
                           InputList.Clear();
                           string listtext = string.Empty;
                           TreeNode node = new TreeNode();
                           AddNode(n, node);
                           for (int i = 0; i < InputList.Count; i++)
                           {
                               listtext += InputList[i];
                               if (i != InputList.Count - 1) listtext += ", ";

                           }
                           node.Text = n.type + "  Input Neurons : " + listtext;
                           model.brainTree.Nodes.Add(node);
                           model.brainTree.Text = data.name;
                       }
                   }
               }
               catch
               {
                   return null;
               }
               return model;
           }
        */
    }
}
