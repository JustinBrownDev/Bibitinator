using System;
using System.Collections.Generic;
using System.Text;

namespace Bibitinator
{
    class dump
    {
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
