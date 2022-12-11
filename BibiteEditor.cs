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

    // Hey! thanks for checking out the source code! If you're seeing this it means my effort to write comments was not in vain :D <3
    // in case its confusing I use the <,V,^,> characters as arrows to point to which code I'm talking about
namespace Bibitinator
{
    public partial class BibiteEditor : Form
    {
        BibiteCollection bibCol = null;         //----------------------------- < Declare Bibite Collection & V List of middle neuron names
        public List<string> middleNodeNames = new List<string> { "Sigmoid", "Linear", "TanH", "Sine", "ReLu", "Gaussian", "Latch", "Differential" };
        public BibiteEditor(BibiteCollection col)
        {
            InitializeComponent();
            bibCol = col;           //----------------------------------------- set local BibiteCollection Instance to the Instance passed in as an arg
        }
        private void BibiteEditor_Load(object sender, EventArgs e)
        {
            propertiesTree.Nodes.Add(RecursiveJPropTreeNodeBuilder(bibCol)); //------------------- this starts func for populating json properties tree
            propertiesTree.Nodes[0].Expand(); //------------------------------- expands the main parent node
            TreeNode brainTree = propertiesTree.Nodes[0].Nodes[6].Nodes[2]; //- subset of the json properties tree starting at the "Nodes" node
                                                                            //- the fact I have to say "Nodes" node bothers me >:^I
            int x = 0;                                                      //- OPPORTUNITY FOR BETTER CODE
                                                                            //- x is the offset needed to select the correct node in new versions,
            if (brainTree.Nodes[0].Nodes.Count == 8) x = 2;        //---------- if (detect version change) then apply offset
            foreach (TreeNode node in brainTree.Nodes)              //--------- < V name each neuron for json properties tree by: 'Description (TypeName)'
            {
                string desc = node.Nodes[6 - x].Nodes[0].Text + " (" + node.Nodes[1].Nodes[0].Text + ")";
                node.Text = desc;
            }

                                                                             // V deserialize json string to JObject
            bibCol.dynRoot = (JObject)JsonConvert.DeserializeObject(bibCol.json, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture });
            buildGenesEditor();                                   //------------- start function to setup genes editor
            BrainTrace();                                            //------------- start function to trace inputs and outputs
            splitContainer1.Cursor = Cursors.Arrow;    //---------------------- for some reason this wont stay in the designer, needs to be set programattically
            this.Text = bibCol.name;                   //---------------------- set window text to bibite name
        }
        private void applyBrainChangesToModel() //APPLIES BRAIN CHAGES TO DYNROOT
        {   //This is certainly the weirdest function in this whole program so heres a brief description of the important steps it takes:

            // 1) for each panel in the brain editor add its respective neurons & the synapse between them to the lists nodesResult & synapses
            // 2) remove synapses that dont (directly or indirectly) connect an input to an output
            // 3) create a list of neurons that are associated with the trimmed list of synapses
            // 4) Add missing static neurons (inputs and outputs), they must be present regardless of wether a synapse connects to them.
            // 5) Assign the synapse Nodein/Nodeout index properties to the list index of their targeted nodes, then assign the
            // nodes' index properties to reflect their list index, removing any gaps or repeats in the node's index properties
            // 6) Assign verified nodes & synapses to the DynRoot variable

            // This function also recalculates the nodes' Nin/Nout properties if the bibite version requires it, rewrites the hidden nodes' descriptions
            // so they are in order (Hidden0, Hidden1, Hidden2, ...), and verifies decimal points are denoted using the . symbol. These are somewhat scattered
            // throughout and thus arn't numbered 
            bool hasNinout = bibCol.json.Contains("Nin"); //Detect whether Nin/Nout needs to be Added
            JArray synapses = new JArray();
            List<JToken> nodesResult = new List<JToken>();
            //static nodes are the input and output neurons that are always present regardless of wether they have synapses going to them
            // V Static Neuron Template
            JArray staticNodes = JArray.Parse("[{\"Type\":0,\"TypeName\":\"Input\",\"Index\":0,\"Inov\":0,\"Desc\":\"Constant\",\"Value\":1.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":1,\"Inov\":0,\"Desc\":\"EnergyRatio\",\"Value\":0.9680333,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":2,\"Inov\":0,\"Desc\":\"Maturity\",\"Value\":0.2516982,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":3,\"Inov\":0,\"Desc\":\"LifeRatio\",\"Value\":1.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":4,\"Inov\":0,\"Desc\":\"Fullness\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":5,\"Inov\":0,\"Desc\":\"Speed\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":6,\"Inov\":0,\"Desc\":\"IsGrabbingObjects\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":7,\"Inov\":0,\"Desc\":\"AttackedDamage\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":8,\"Inov\":0,\"Desc\":\"BibiteConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":9,\"Inov\":0,\"Desc\":\"BibiteConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":10,\"Inov\":0,\"Desc\":\"NVisibleBibites\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":11,\"Inov\":0,\"Desc\":\"PelletConcentrationWeight\",\"Value\":0.124345839,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":12,\"Inov\":0,\"Desc\":\"PelletConcentrationAngle\",\"Value\":-0.9555383,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":13,\"Inov\":0,\"Desc\":\"NVisiblePellets\",\"Value\":0.25,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":14,\"Inov\":0,\"Desc\":\"MeatConcentrationWeight\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":15,\"Inov\":0,\"Desc\":\"MeatConcentrationAngle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":16,\"Inov\":0,\"Desc\":\"NVisibleMeat\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":17,\"Inov\":0,\"Desc\":\"ClosestBibiteR\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":18,\"Inov\":0,\"Desc\":\"ClosestBibiteG\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":19,\"Inov\":0,\"Desc\":\"ClosestBibiteB\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":20,\"Inov\":0,\"Desc\":\"Tic\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":21,\"Inov\":0,\"Desc\":\"Minute\",\"Value\":0.219387144,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":22,\"Inov\":0,\"Desc\":\"TimeAlive\",\"Value\":0.219387144,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":23,\"Inov\":0,\"Desc\":\"PheroSense1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":24,\"Inov\":0,\"Desc\":\"PheroSense2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":25,\"Inov\":0,\"Desc\":\"PheroSense3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":26,\"Inov\":0,\"Desc\":\"Phero1Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":27,\"Inov\":0,\"Desc\":\"Phero2Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":28,\"Inov\":0,\"Desc\":\"Phero3Angle\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":29,\"Inov\":0,\"Desc\":\"Phero1Heading\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":30,\"Inov\":0,\"Desc\":\"Phero2Heading\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":31,\"Inov\":0,\"Desc\":\"Phero3Heading\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":0,\"TypeName\":\"Input\",\"Index\":32,\"Inov\":0,\"Desc\":\"InfectionRate\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":33,\"Inov\":0,\"Desc\":\"Accelerate\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":34,\"Inov\":0,\"Desc\":\"Rotate\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":35,\"Inov\":0,\"Desc\":\"Herding\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":36,\"Inov\":0,\"Desc\":\"Want2Lay\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":37,\"Inov\":0,\"Desc\":\"Want2Eat\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":38,\"Inov\":0,\"Desc\":\"Digestion\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":39,\"Inov\":0,\"Desc\":\"Grab\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":40,\"Inov\":0,\"Desc\":\"ClkReset\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":41,\"Inov\":0,\"Desc\":\"PhereOut1\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":42,\"Inov\":0,\"Desc\":\"PhereOut2\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":5,\"TypeName\":\"ReLu\",\"Index\":43,\"Inov\":0,\"Desc\":\"PhereOut3\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":44,\"Inov\":0,\"Desc\":\"Want2Grow\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":45,\"Inov\":0,\"Desc\":\"Want2Heal\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":1,\"TypeName\":\"Sigmoid\",\"Index\":46,\"Inov\":0,\"Desc\":\"Want2Attack\",\"Value\":0.5,\"LastInput\":0.0,\"LastOutput\":0.5},{\"Type\":3,\"TypeName\":\"TanH\",\"Index\":47,\"Inov\":0,\"Desc\":\"ImmuneSystem\",\"Value\":0.0,\"LastInput\":0.0,\"LastOutput\":0.0}]");
            int staticNodeCount = staticNodes.Count;

            foreach (Control con in BrainEditorPanel.Controls) //------------- STEP #1) Only Adds synapses and neurons referenced in brain editor
            {                                                              //- the nodes associated with each synapse are stored as a tag when generating the panels
                List<JToken> relatedNodes = (List<JToken>)con.Tag;
                Tuple<int, int> tagTup = (Tuple<int, int>)con.Controls[3].Tag;
                //- V Find and add this panel's synapse
                JToken s = bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == tagTup.Item1 && x.Value<int>("NodeIn") == tagTup.Item2).First();
                synapses.Add(s);
                relatedNodes[0]["NOut"] = 0;                               //- Set NOut & NIn to 0 so they can be recalculated
                relatedNodes[1]["NOut"] = 0;
                relatedNodes[0]["NIn"] = 0;
                relatedNodes[1]["NIn"] = 0;
                if (nodesResult.Find(x => x.Value<int>("Index") == relatedNodes[0].Value<int>("Index")) == null)
                {                                                          //- If nodes result doesnt contain node add it
                    nodesResult.Add(relatedNodes[0]);
                }
                if (nodesResult.Find(x => x.Value<int>("Index") == relatedNodes[1].Value<int>("Index")) == null)
                {                                                          //- If nodes result doesnt contain node add it pt.2 Electric Bibitagloo
                    nodesResult.Add(relatedNodes[1]);
                }
            }
            bool complete = false;
            while (!complete)    //----------------------------------------- STEP #2) trim the synapses for only those that serve a function
            {                                                              //this doesnt work entirely but its enough for the game to load
                int count = synapses.Count;                                //Count of synapses before loop
                List<JToken> remove = new List<JToken>();                  //Synapses to Remove
                foreach (JToken s in synapses)
                {   //If the node referenced if a hidden node AND that node doesnt have a synapse on the other side, remove it
                    if (s.Value<int>("NodeIn") >= staticNodeCount && synapses.Where(x => x.Value<int>("NodeOut") == s.Value<int>("NodeIn")).Count() == 0) remove.Add(s);
                    else if (s.Value<int>("NodeOut") >= staticNodeCount && synapses.Where(x => x.Value<int>("NodeIn") == s.Value<int>("NodeOut")).Count() == 0) remove.Add(s);
                }
                remove.ForEach(x => synapses.Remove(x));
                if (synapses.Count == count) complete = true;              //if no synapses were removed this round, move on
            }

            JArray validatedNodes = new JArray(); //-------------------------- STEP #3) create a list of nodes that are associated with the trimmed list of synapses
            foreach (JToken s in synapses)  //-------------------------------- adds nodes that are referebced by the synapses to validatedNodes
            {
                if (validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).Count() == 0) validatedNodes.Add(nodesResult.Find(x => x.Value<int>("Index") == s.Value<int>("NodeIn")));
                if (validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeOut")).Count() == 0) validatedNodes.Add(nodesResult.Find(x => x.Value<int>("Index") == s.Value<int>("NodeOut")));
            }
            validatedNodes = new JArray(validatedNodes.Children().OrderBy(x => x.Value<int>("Index")));
            for (int i = 0; i < staticNodeCount; i++)                      //- STEP #4) add any missing static nodes
            {
                if (validatedNodes.Children().Where(x => x.Value<int>("Index") == i).Count() == 0)
                {
                    validatedNodes.Add(staticNodes[i]);
                }
            }
            validatedNodes = new JArray(validatedNodes.Children().OrderBy(x => x.Value<int>("Index")));
            foreach (JToken s in synapses) //--------------------------------- STEP #5) Assign NodeIn/NodeOut properties to the list index of their targeted Node
            {                              //--------------------------------- Node Index properties will later be changed to reflect this
                s["NodeIn"] = validatedNodes.Children().ToList().IndexOf(validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).First());
                s["NodeOut"] = validatedNodes.Children().ToList().IndexOf(validatedNodes.Children().Where(x => x.Value<int>("Index") == s.Value<int>("NodeOut")).First());
            }
            // tups tuple list holds the original token & and replacement token in each
            // tuple, I'm not sure why I didn't do the replacing inline but there must
            // have been a reason.
            List<Tuple<JToken, JToken>> tups = new List<Tuple<JToken, JToken>>();
            foreach (JToken tok in validatedNodes.Children())
            {                                               //---------------- Make sure decimal points are using invariant culture notation
                tok["Value"] = Regex.Replace(tok.Value<string>("Value"), ",", ".");
                //--------- V Assign Index propertes to their actual index in the list, there should now be no gaps in the node indexes 
                validatedNodes.Where(x => x == tok).First()["Index"] = validatedNodes.ToList<JToken>().IndexOf(tok);
                if (hasNinout && tok["NIn"] == null)                //--------- If Nin/Nout is to be included, and those properties arnt present
                {
                    tok.Last().AddAfterSelf("\"NIn\" : 0");
                    tok.Last().AddAfterSelf("\"NOut\" : 0");
                }
                if (!hasNinout && tok["NIn"] != null)           //------------ If Nin/Nout is not to be included and those properties are present
                {
                    JToken replacement = new JObject(new JProperty("Type", tok.Value<int>("Type")), new JProperty("TypeName", tok.Value<string>("TypeName")), new JProperty("Index", tok.Value<int>("Index")), new JProperty("Inov", tok.Value<int>("Inov")), new JProperty("Desc", tok.Value<string>("Desc")), new JProperty("Value", tok.Value<double>("Value")), new JProperty("LastInput", tok.Value<double>("LastInput")), new JProperty("LastOutput", tok.Value<double>("LastOutput")));
                    tups.Add(Tuple.Create(tok, replacement));
                }
            }
            foreach (Tuple<JToken, JToken> tup in tups) //---------------------- Do the replacing for the Tups list
            {
                validatedNodes.Where(x => x == tup.Item1).First().Replace(tup.Item2);
            }
            foreach (JToken tok in validatedNodes.Children().Where(x => x.Value<string>("Desc").Contains("Hidden")))
            {                                          //--------------------- Rewrite the hidden nodes descriptions' to reflect the new set of nodes
                validatedNodes.Children().Where(x => x == tok).First()["Desc"] = "Hidden" + (tok.Value<int>("Index") - staticNodeCount).ToString();
            }
            if (hasNinout)
            {           //---------------------------------------------------- If Nin/Nout is to be assigned, run through the synapse list and increment
                foreach (JToken synapse in synapses)                         // the respective values each time a node is referenced
                {
                    JToken inNode = validatedNodes.Where(x => x.Value<int>("Index") == synapse.Value<int>("NodeIn")).First();
                    JToken outNode = validatedNodes.Where(x => x.Value<int>("Index") == synapse.Value<int>("NodeOut")).First();
                    validatedNodes.Where(x => x == inNode).First()["NIn"] = inNode.Value<int>("NIn") + 1;
                    validatedNodes.Where(x => x == outNode).First()["NOut"] = inNode.Value<int>("NIn") + 1;
                }
            }
            bibCol.dynRoot["brain"]["Nodes"].Replace(validatedNodes); //------ STEP #6) Finally, apply these changes to DynRoot
            bibCol.dynRoot["brain"]["Synapses"].Replace(synapses);
        }
        private string ReplaceValuesInBibiteSettings(string json) //SAVES GENES DATA TO JSON
        {
            //- chars to look for to find where the value starts
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'N', 'f', 't' };
            char[] stops = { ',', '}', ']' };                              //- chars to look for to find where the value ends
            foreach (Panel p in editorflow.Controls)
            {
                TextBox box = (TextBox)p.Controls[1];                      //- get textbox 
                string value = Regex.Replace(box.Text, ",", ".");          //- ensure data is using periods to denote the decimal point
                if (value.StartsWith('.')) value = 0 + value;              //- prepend 0 to values with a leading decimal point
                if (value.Equals("NaN")) value = "NaN\"";                  //- Hacky fix, adding " to stops caused other issues
                                                                           //- V get the index of the property, adds 2 to make sure its inside the property name for the next step
                int propIndex = json.IndexOf('"' + box.Tag.ToString() + '"') + box.Tag.ToString().Length + 2;
                int valIndex = json.IndexOfAny(nums, propIndex);           //- Get the index of the value by finding a num char after the property index
                                                                           //- V get the length of the value by finding the index of the next stop char and subtracting the valIndex
                int stopRemoving = json.IndexOfAny(stops, valIndex) - valIndex;
                json = json.Remove(valIndex, stopRemoving);                //- Remove old value and
                json = json.Insert(valIndex, value);                       //- Insert new

            }

            json = Regex.Replace(json, @"\s", "");                         //- remove whitespace
            return json;
        }
        void serializeModelData(string name) //SERIALIZES DYNROOT TO JSON, SAVES TO FILE
        {
            //begin serializing the data to a json file
            //the version selection is janky, needs to use the version property in settings.json, currently theres only two versions supported and it differentiates via the file typ
            string json = string.Empty;
            json = JsonConvert.SerializeObject(bibCol.dynRoot, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture });

            json = ReplaceValuesInBibiteSettings(json);
            bibCol.json = json;
            //confirm file was created
            if (File.Exists(bibCol.saveTo + name))
            {
                File.Delete(bibCol.saveTo + name);
            }
            File.WriteAllText(bibCol.saveTo + name, json);
            if (File.Exists(bibCol.saveTo + name))
            {
                MessageBox.Show("Saved Sucessfully");
            }
        }
        public TreeNode RecursiveJPropTreeNodeBuilder(BibiteCollection col) //BUILD JSON PROPERTIES TREE VIEW
        {
            var jobj = JObject.Parse(col.json);      //----------------------- Deserialize json string to JObject, Why not just use bibcol.dynroot? idk, maybe I had a good reason i dont rememeber now
            TreeNode tn = new TreeNode();
            void AddNode(JToken token, TreeNode inTreeNode)         //-------- Recursive function for descending down tree nodes, adds the token to inTreeNode 
            {
                if (token == null)                  //------------------------ Happens when it gets to the end of a branch
                    return;
                if (token is JValue)                //------------------------ If the token is just a value
                {                                                           // V add a node with the token value
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                    inTreeNode.Text += (":  " + token.ToString()); //--------- Add the token value to the name of the node so users dont have to expand a node to see its value
                    childNode.Tag = token;          //------------------------ Set the node tag = to the token
                }
                else if (token is JObject)          //------------------------ If token is a JObject, ex: transform
                {
                    var obj = (JObject)token;       //------------------------ make sure its being read as a JObject
                    foreach (var property in obj.Properties())
                    {
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name))];
                        childNode.Tag = property;  //------------------------- < Set Tag ^Add Node for property
                        AddNode(property.Value, childNode); //---------------- Recurse on each property
                    }
                }
                else if (token is JArray)     //------------------------------ If token is a JArray, ex: position
                {
                    var array = (JArray)token; //----------------------------- make sure its being read as a JArray
                    for (int i = 0; i < array.Count; i++) //------------------ for each item
                    {                                                       // V Add a node to tree for item
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                        childNode.Tag = array[i]; //-------------------------- set tag to array item 
                        AddNode(array[i], childNode); //---------------------- recurse on item (Unnecessary?)
                    }
                }
            }
            AddNode(jobj, tn);  //-------------------------------------------- Start recursive func on main Jobj
            tn.Text = col.name; //-------------------------------------------- Set main node name to bibite name
            return tn;
        }
        public void buildGenesEditor() //BUILD GENES EDITOR
        {
            //Tag ref:
            //label: "label" string, used when saving genes so the program knows not to do anything with it
            //textbox: Name of the gene
                                                                           //- For each item in the genes obj, create a textbox
            foreach (JProperty item in bibCol.dynRoot["genes"]["genes"].ToList())
            {                                                              //- This is all just WinForms formatting stuff
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
            List<JToken> nodes = bibCol.dynRoot["brain"]["Nodes"].ToList();//- Get list of Neurons
            foreach (string node in middleNodeNames)
            {                              //--------------------------------- Add middle nodes to the middle node dropdown
                AddNeuronComboBox.Items.Add(node);
            }
            foreach (JToken node in nodes) //--------------------------------- Sort through nodes in bibite json and add them to the corresponding dropdowns
            {

                if (node.Value<string>("TypeName").Equals("Input"))  //------- Inputs go to inputs
                {
                    inputComboBox.Items.Add(node.Value<string>("Desc") + " :Input");
                }
                else if (node.Value<string>("Desc").Contains("Hidden")) //---- Middles go to input & output
                {
                    inputComboBox.Items.Add(node.Value<string>("Desc") + " :" + node.Value<string>("TypeName"));
                    outputComboBox.Items.Add(node.Value<string>("Desc") + " :" + node.Value<string>("TypeName"));
                }
                else    //---------------------------------------------------- Outputs go to output
                {
                    outputComboBox.Items.Add(node.Value<string>("Desc") + " :Output");
                }
            }

            inputComboBox.Text = "select";
            outputComboBox.Text = "select";
            
            List<JToken> synaps = bibCol.dynRoot["brain"]["Synapses"].ToList();
            foreach (JToken synap in synaps)   //----------------------------- ^Get list of synapses < for each V pass them to AddSynapsPanel
            {
                AddSynapsePanel(synap);
            }
        }
        private void BrainTrace() //TRACE SYNAPSE CONNECTIONS AND CREATE A TREE VIEW FROM OUTPUTS TO ITS INPUTS
        {
            ConnectionsTreeView.Nodes.Clear();  //---------------------------- Clear the brain tree
            List<string> InputList = new List<string>(); //------------------- Holds the list of inputs to be displayed next to the output neuron for readability
            List<int> NoLoops = new List<int>();   //------------------------- Holds a list of nodes indexes to prevent loops
                                                   //------------------------- V Get output neurons from dynroot, pass each Jtoken into TraceOutputNeurons func
            bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("TypeName") != "Input" && !x.Value<string>("Desc").Contains("Hidden")).ToList().ForEach(TraceOutputNeurons); ;

            void TraceOutputNeurons(JToken n) //Caller for the recursive Tracer function
            {
                InputList.Clear();     //------------------------------------- < V Empty out variables for each output node
                string listtext = string.Empty;
                NoLoops.Clear();
                TreeNode node = new TreeNode();

                Tracer(n, node);  //------------------------------------------ Call Recursive Function
                for (int i = 0; i < InputList.Count; i++)  //----------------- Add input neurons to listtext string
                {
                    listtext += InputList[i];
                    if (i != InputList.Count - 1) listtext += ", "; //-------- Add commas after the inputs except for the last one
                }
                node.Text = n.Value<string>("Desc") + "  Input Neurons : " + listtext;
                //- V if node is not empty, add it to the treeview
                if (node.Nodes.Count > 0) ConnectionsTreeView.Nodes.Add(node);
            }

            void Tracer(JToken n, TreeNode inTreeNode) //Recursive function to trace synapses
            {

                // the neuron being passed in has already been added to the tree.
                // when it reaches the end (input) add it to the list of input neruons
                // for that output, unless its already in there
                if (n.Value<string>("TypeName").Equals("Input") && !InputList.Contains(n.Value<string>("Desc"))) InputList.Add(n.Value<string>("Desc"));
                else if (!NoLoops.Contains(n.Value<int>("Index"))) //--------- make sure the function isnt in an infinite loop
                {
                    NoLoops.Add(n.Value<int>("Index"));
                    List<JToken> inputNeurons = new List<JToken>(); //-------- make list of nodes with synapses going into current neuron
                    List<double> weight = new List<double>();       //-------- and a parallel list of weights
                    void recordSynapseData(JToken s)  // Adds a input Neuron to inputNeurons and weight Lists
                    {
                        inputNeurons.Add(((JToken)bibCol.dynRoot)["brain"]["Nodes"].Where(x => x.Value<int>("Index") == s.Value<int>("NodeIn")).First());
                        weight.Add(s.Value<double>("Weight"));
                    }
                    //-------- V Find Synapses that output to the current neuron, send to recordSynapseData
                    bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == n.Value<int>("Index")).ToList().ForEach(recordSynapseData);
                    for (int i = 0; i < inputNeurons.Count; i++) //----------- for each neuron that inputs to the current neuron
                    {                                                      //- if the current neuron is an input, it will have no input neurons
                        string nodetext;                                   //- and the for loop will not run

                        //- V Set correct name accoring to neuron type
                        if (inputNeurons[i].Value<string>("Desc").Contains("Hidden")) nodetext = inputNeurons[i].Value<string>("TypeName");
                        else nodetext = inputNeurons[i].Value<string>("Desc");
                        //- V Add input Neuron to current neuron
                        var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode((nodetext + ", strength: " + weight[i]).ToString()))];
                        //- Get Synapse that connects the input neuron to the current neuron
                        JToken synapse = bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == n.Value<int>("Index") && x.Value<int>("NodeIn") == inputNeurons[i].Value<int>("Index")).First();
                        //- V create tuple to store NodeIn/Nodeout Pair
                        Tuple<int, int> tup = Tuple.Create(synapse.Value<int>("NodeOut"), synapse.Value<int>("NodeIn"));
                        childNode.Tag = tup;                               //- Set input neuron tag to the tuple
                        Tracer(inputNeurons[i], childNode);                //- Recurse on the input neuron
                    }

                }
            }




        }
        private void AddSynapsePanel(JToken synap) //ADDS SYNAPSE TO SYNAPSE LIST
        {
            //Tag Ref:
            //Panel: relatedNodes JToken list
            //Delete button: Nodein/Nodeout tuple
            //Input box: Nodein int
            //Output box: Nodeout int
            //UpDown box: En bool
            List<JToken> relatedNodes = new List<JToken>();
                                                                    //-------- Get nodes that this synaps connects to add them to list ^
                                                                    //-------- V Need this to display the name of the node, synaps only contains the index
            relatedNodes.Add(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(synap.Value<int>("NodeIn"))).First());
            relatedNodes.Add(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(synap.Value<int>("NodeOut"))).First());
            FlowLayoutPanel p = new FlowLayoutPanel();            //---------- set up panel & visual formatting stuff
            p.Anchor = AnchorStyles.Top;
            TextBox cbIn = new TextBox();
            TextBox cbOut = new TextBox();
            TextBox nud = new TextBox();
            Button del = new Button();
            cbIn.ReadOnly = true;
            cbOut.ReadOnly = true;
            nud.ReadOnly = true;
            
            Tuple<int, int>  tup = Tuple.Create(synap.Value<int>("NodeOut"), synap.Value<int>("NodeIn"));
            del.Tag = tup; //------------------------------------------------- set up delete button with the node indexes in it's tag
            del.Text = "Remove";
            del.Click += synapseDeleteButton_Click;
                                                                            // V Input/Output node text, use Desc for static nodes and Typename for Hidden nodes
            cbIn.Text = synap.Value<int>("NodeIn") < 43 ? relatedNodes[0].Value<String>("Desc") : relatedNodes[0].Value<String>("Desc") + " :" + relatedNodes[0].Value<String>("TypeName");
            cbIn.Tag = relatedNodes[0].Value<int>("Index");
            cbOut.Text = synap.Value<int>("NodeOut") < 43 ? relatedNodes[1].Value<String>("Desc") : relatedNodes[1].Value<String>("Desc") + " :" + relatedNodes[1].Value<String>("TypeName");
            cbOut.Tag = relatedNodes[1].Value<int>("Index");
            nud.Text = synap.Value<string>("Weight");                    //--- < set updown box value for weight and V set tag as en for use in saving
            nud.Tag = synap.Value<bool>("en");

            //more visual formatting stuff
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
        private void synapseDeleteButton_Click(object sender, EventArgs e)
        {
            Tuple<int,int> tagTup =  (Tuple<int,int>)((Button)sender).Tag; //- Get the tag containing NodeIn/Nodeout
            try
            {
                                                                           //- V get synapse token from dynRoot
                JToken syn = bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut") == tagTup.Item1 && x.Value<int>("NodeIn") == tagTup.Item2).First();
                                                                           //- V Remove synapse token from dynRoot
                bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Equals(syn)).First().Remove();
                void removeFromNodesAndDropDown(JToken n)  //----------------- Func to remove unconnected node
                {
                    if (n.Value<string>("Desc").Contains("Hidden"))  //------- Make sure its not a static node
                    {                                                      //- Remove the node from dynroot & input/output node dropdowns
                        bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Equals(n)).First().Remove();
                        inputComboBox.Items.Remove(n.Value<string>("Desc") + " :" + n.Value<string>("TypeName"));
                        outputComboBox.Items.Remove(n.Value<string>("Desc") + " :" + n.Value<string>("TypeName"));
                    }                                                      //- for the in and out node check for any synapses that connect to that node
                }                                                          //- if the node has no connections, pass it into the removeFromNodesAndDropDown function
                                                                           //- V this will not delete static nodes as the function checks for that
                if (bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut").Equals(syn.Value<int>("NodeIn")) || x.Value<int>("NodeIn").Equals(syn.Value<int>("NodeIn"))).Count() == 0) removeFromNodesAndDropDown(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(syn.Value<int>("NodeIn"))).First());
                if (bibCol.dynRoot["brain"]["Synapses"].Where(x => x.Value<int>("NodeOut").Equals(syn.Value<int>("NodeOut")) || x.Value<int>("NodeIn").Equals(syn.Value<int>("NodeOut"))).Count() == 0) removeFromNodesAndDropDown(bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<int>("Index").Equals(syn.Value<int>("NodeOut"))).First());

            } catch(Exception ex) { MessageBox.Show(ex.Message); }
            BrainTrace();    //---------------------------------------------------- Refresh the brain tree without the deleted synapse
            BrainEditorPanel.Controls.Remove(((Button)sender).Parent); //----- Remove the panel from the synapse editor list
        }     
        private void saveButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;                                  
            if (File.Exists(bibCol.saveTo + bibCol.name))           //- delete original file
            {
                File.Delete(bibCol.saveTo + bibCol.name);
            }
            string json = ReplaceValuesInBibiteSettings(bibCol.json);      //- Save Genes
            bibCol.json = json;
            File.WriteAllText(bibCol.saveTo + bibCol.name, json);   //- Write new file

            if (File.Exists(bibCol.saveTo + bibCol.name))           //- Make sure its there
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Failure!");
            }
            Cursor = Cursors.Default;
        }
        private void GenesResetButton_Click(object sender, EventArgs e) //RESETS GENES 
        {
            editorflow.Controls.Clear();
            inputComboBox.Items.Clear();
            outputComboBox.Items.Clear();
            AddNeuronComboBox.Items.Clear();
            buildGenesEditor();
        }
        private void ConnectionsTreeView_AfterSelect(object sender, TreeViewEventArgs e) //HIGHLIGHTS SYNAPSE WHEN SELECTED IN BRAIN TREE
        {
            if (MovingLabel.Text == "")
            {
                MovingLabel.Text = "Selected";
            }
            foreach (Control con in BrainEditorPanel.Controls)
            {                                                              //- Find the synapse panel that corresponds to the selected treenode
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
                                                                           //- Get a synapse to edit, I think editing an empty bibite will break this
                JToken s = bibCol.dynRoot["brain"]["Synapses"].FirstOrDefault().DeepClone();
                string input = inputComboBox.SelectedItem.ToString();      //- Get input and output name, remove text after the space so its only the actual name
                input = input.Remove(input.IndexOf(' '));
                string output = outputComboBox.SelectedItem.ToString();
                output = output.Remove(output.IndexOf(' '));
                                                                           //- get node indexes by input/output names & set them
                s["NodeIn"] = bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("Desc") == input).First().Value<int>("Index");
                s["NodeOut"] = bibCol.dynRoot["brain"]["Nodes"].Where(x => x.Value<string>("Desc") == output).First().Value<int>("Index");
                s["en"] = true;                                            //- < Always enabled and V set weight
                s["Weight"] = double.Parse(strengthUpDown.Value.ToString(), CultureInfo.InvariantCulture);
                s["Inov"] = 0;                                             //- IDK what Inov does
                bibCol.dynRoot["brain"]["Synapses"].Last().AddAfterSelf(s);//- Add to dynroot
                AddSynapsePanel(s);                                        //- Add to editor
                BrainTrace();                                                   //- refresh brain tree
            }
                
        }
        private void AddNeuronButton_Click(object sender, EventArgs e)
        {
            if (AddNeuronComboBox.SelectedIndex > -1)
            {
                JToken n = bibCol.dynRoot["brain"]["Nodes"].FirstOrDefault().DeepClone();
                n["Type"] = AddNeuronComboBox.SelectedIndex + 1;
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
            List<string> names = Directory.EnumerateFiles(bibCol.saveTo).ToList();
            string name = "bibite_" + names.Count() + bibCol.name.Substring(bibCol.name.IndexOf('.'));
            int i = names.Count() + 1;
            while (File.Exists(bibCol.saveTo + name))
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
    }
}
