using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using Bibitinator.Models;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;

namespace Bibitinator
{
    public partial class Bibitinator : Form
    {
        public Bibitinator()
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabPage0;
        }
        //Used to hold the first reading of the worldsettings Json file, when the world settings
        //reset button is clicked worldsettingsJson is set to equal OgWorldsettingJson
        public string OgWorldsettingJson = string.Empty;
        public string worldsettingsJson = string.Empty;
        //extension variables used to accept both .bb8 & .json files
        public string bibiteExtension = string.Empty;
        public string settingsExtension = string.Empty;
        public string sceneExtension = string.Empty;
        //Directory path where world files are extracted to and operated on before exporting
        public string extractTo;
        private void browseButton_Click(object sender, EventArgs e)
        {
            //for some reason this wont stick in the designer, needs to be set programmatically
            splitContainer1.Cursor = Cursors.Arrow;
            
            bibiteListView.Items.Clear();

            string userFilepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\LocalLow\\The Bibites\\The Bibites";
            string tempFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\bibitinator";

            if (Directory.Exists(userFilepath)) openWorldZipDialog.InitialDirectory = userFilepath;
            openWorldZipDialog.ShowDialog();
            worldFilePathTextBox.Text = openWorldZipDialog.FileName;
            //if the file path has been set and the file is a zip archive, continue with extraction and setup
            if (worldFilePathTextBox.Text != null && File.Exists(openWorldZipDialog.FileName) && openWorldZipDialog.FileName.EndsWith(".zip"))
            {
                //extract the zip file
                Cursor.Current = Cursors.WaitCursor;
                int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
                //extract to directory with the name of the world
                extractTo = tempFilePath + openWorldZipDialog.FileName.Substring(lastSlashIndex, openWorldZipDialog.FileName.LastIndexOf('.') - lastSlashIndex);
                if (Directory.Exists(extractTo))
                {
                    Directory.Delete(extractTo, true);
                }
                ZipFile.ExtractToDirectory(openWorldZipDialog.FileName, extractTo);
                //set the extension variables
                if (File.Exists(extractTo + "\\settings.json"))
                {
                    bibiteExtension = ".json";
                    settingsExtension = ".json";
                    sceneExtension = ".json";
                }
                else if (File.Exists(extractTo + "\\settings.bb8settings"))
                {
                    bibiteExtension = ".bb8";
                    settingsExtension = ".bb8settings";
                    sceneExtension = ".bb8scene";
                }
                else
                {
                    worldFilePathTextBox.Text = "error";
                }
                //populate bibites listview
                string[] bibiteList = Directory.GetFiles(extractTo + "\\bibites");
                foreach (string fileName in bibiteList)
                {
                    string bibiteName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                    bibiteListView.Items.Add(bibiteName);
                }
                bibiteListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //get world settings, populate world settings panel
                using (StreamReader r = new StreamReader(extractTo + "\\settings" + settingsExtension))
                {
                    var json = r.ReadToEnd();
                    //this is the json string being acted on
                    worldsettingsJson = json.ToString();
                    //this holds the initial json string used when the user clicks the reset button
                    OgWorldsettingJson = json.ToString();
                }
                populateSettings();
            }
            else
            {
                worldFilePathTextBox.Text = "invalid";
            }
            
        }
        private void Analyze(object sender, EventArgs e)
        {
            if(bibiteListView.SelectedIndices.Count > 0)
            {
                //initialize bibite collection to pass to editor
                BibiteCollection col = new BibiteCollection();
                col.name = bibiteListView.SelectedItems[0].Text ;
                col.extractFolder = extractTo + "\\bibites\\";
                string filePath = extractTo + "\\bibites\\" + col.name;
                string result = string.Empty;
                JObject jobj;
                //get bibite json file
                try
                {
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        var json = r.ReadToEnd();
                        jobj = JObject.Parse(json);
                        result = jobj.ToString(Newtonsoft.Json.Formatting.Indented);
                    }
                    col.json = result;
                }
                catch
                {

                }
                BibiteEditor editorWindow = new BibiteEditor(col);
                editorWindow.Show();
            }
        }

        private void worldSettingsSaveButton_Click(object sender, EventArgs e)
        {
            //get json file with setting values replaced with those in the editor
            string json = replaceValuesInSettings(worldsettingsJson);
            if (json == "") return; //if they click save without uploading a file, do nothing

            //overwrite settings file
            File.WriteAllText(extractTo + "\\settings" + settingsExtension, json);

            //if the settings file exists, saved successful
            if (File.Exists(extractTo + "\\settings" + settingsExtension))
            {
                MessageBox.Show("Saved Succesfully");
            }
            else
            {
                MessageBox.Show("Save Failed");
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            //if they click export without uploading a world, do nothing
            if(bibiteListView.Items.Count > 0)
            {
                //get name of the world file
                int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
                string zipName = openWorldZipDialog.FileName.Substring(lastSlashIndex);
                //get path to the bibitinator folder in the bibites game files
                //if it doesnt exist already, create it
                string targetFileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                "\\AppData\\LocalLow\\The Bibites\\The Bibites\\Bibitinator\\";
                if (!Directory.Exists(targetFileName))
                {
                    Directory.CreateDirectory(targetFileName);
                }
                //allow the user to select a different directory if preferred
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = targetFileName;
                folderBrowserDialog.ShowDialog();
                //zipping function
                zipInOrder(folderBrowserDialog.SelectedPath, zipName);
            }       
        }
        private void populateSettings()
        {
            //if no file uploaded, do nothing, otherwise, clear any tab pages and build new ones
            if (worldsettingsJson == "") return;
            worldSettingTabControl.TabPages.Clear();

            //this exists to put settings in the correct tab, apply headers, and specify parent properties for
            //Plant & Meat settings values
            settingsDictionary settingsDictionary = new settingsDictionary();
            
            //create model for world settings, use invariant culuture
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.Culture = CultureInfo.InvariantCulture;
            JObject obj = (JObject)JsonConvert.DeserializeObject(worldsettingsJson, jss);
            
            //foreach surface level property in worldsettings 
            List<JToken> props = new List<JToken>();
            foreach (JToken prop in obj.Children())
            {
                settingsDigger(prop);
            }
            //settingsDigger adds to a list of properties, needed because most settings are in
            //"[Property Name]:{ Value: [Value]} format"
            //but some are in
            //"[Parent Property Name]: {[ChildProperty1Name]:[ChildProperty1Value], ...} format
            //creates a list of every property/value pair
            void settingsDigger (JToken prop)
            { 
                if(prop.Values().ToList().Count() > 1)
                {

                    foreach (JToken subprop in prop.Children())
                    { 
                        settingsDigger(subprop);
                    }
                    
                }
                else
                {
                    props.Add(prop);
                }
            }
            //if UsersSettingsData doesnt exist create a copy of SettingsData (the default user settings data) with that name.
            //Deleting UsersSettingsData will reset favorites to default.
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\UserSettingsData.json")) File.Copy(Directory.GetCurrentDirectory() + "\\SettingsData.json", Directory.GetCurrentDirectory() + "\\UserSettingsData.json");
            
            //Convert Json file to list of settings
            JObject jOb;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "\\UserSettingsData.json"))
            {
                var json = r.ReadToEnd();
                
                jOb = (JObject)JsonConvert.DeserializeObject(json);
                
            }
            JArray jAr = jOb.Value<JArray>("knownSettings");
            List<settingsDictionary.setting> knownSettings = jAr.ToObject<settingsDictionary.setting[]>().ToList();
            
            //create favorites Tab
            TabPage favTab = new TabPage();
            favTab.Name = "Favorites";
            favTab.Text = "Favorites";
            FlowLayoutPanel Favflow = new FlowLayoutPanel();
            Favflow.AutoScroll = true;
            Favflow.Parent = favTab;
            Favflow.Name = "Flow";
            Favflow.Dock = DockStyle.Fill;
            Favflow.SetBounds(0, 0, 0, 0);
            Favflow.WrapContents = true;
            worldSettingTabControl.TabPages.Add(favTab);
            Label label = new Label();
            label.Text = "Double click a setting to add or remove it from your favorites";
            label.AutoSize = true;
            label.Parent = Favflow;
            Favflow.SetFlowBreak(label, true);

            //create tabs for settings in knownSettings
            foreach (settingsDictionary.setting setting in knownSettings)
            {
                //if property is the child of another, add a check for the parent to make sure it's the correct instance of that named property
                JToken prop;
                if (setting.internalLocation != "")
                {
                    prop = props.Find(x => ((JProperty)x).Name == setting.internalName && ((JProperty)x.Parent.Parent).Name == setting.internalLocation);
                }
                else
                {
                    prop = props.Find(x => ((JProperty)x).Name == setting.internalName);
                }
                //if this setting's parent 
                if (!worldSettingTabControl.TabPages.ContainsKey(setting.Category))
                {
                    TabPage tab = new TabPage();
                    tab.Name = setting.Category;
                    tab.Text = setting.Category;
                    FlowLayoutPanel flow = new FlowLayoutPanel();
                    flow.AutoScroll = true;
                    flow.Parent = tab;
                    flow.Name = "Flow";
                    flow.Dock = DockStyle.Fill;
                    flow.SetBounds(0, 0, 0, 0);
                    flow.WrapContents = true;
                    worldSettingTabControl.TabPages.Add(tab);
                }
                //build the setting panel in its parent tab
                buildBox(prop, setting, false);
                //if its a favorite setting, put it in favorites too
                if (setting.favorite)
                {
                    buildBox(prop, setting, true);

                }
                
            }
            void buildBox (JToken prop, settingsDictionary.setting setting, bool placeInFavs) 
            {
                // if its a favorite, override flowindex with "Favorites" instead of the settings's category
                int flowIndex;
                if (placeInFavs)
                {
                    flowIndex = worldSettingTabControl.TabPages.IndexOfKey("Favorites");
                }
                else
                {
                    flowIndex = worldSettingTabControl.TabPages.IndexOfKey(setting.Category);
                }
                //find the flow panel to be worked on
                FlowLayoutPanel flow = (FlowLayoutPanel)worldSettingTabControl.TabPages[flowIndex].Controls.Find("Flow", false).First();
                
                //if the setting is the first with it's parent add a label header for it
                if (!flow.Controls.ContainsKey(setting.parent))
                {
                    Label label = new Label();
                    label.Text = setting.parent;
                    label.Font = new System.Drawing.Font("Segoe UI", 15);
                    label.Name = setting.parent;
                    label.Width = 200;
                    label.Height = 40;
                    label.Margin = new Padding(0, 12, 0, 12);
                    flow.Controls.Add(label);
                    flow.SetFlowBreak(label, true);

                }

                string value = string.Empty;
                if (!prop.Children().First().HasValues)
                {
                    value = prop.Children().First().Value<string>();
                }
                else
                {
                    value = prop.Values().First().Values().First().Value<string>();
                    //value = prop.Children().First().Children().First().Children().Values().ToList().First().ToString();
                }
                //build property panel
                Label l = new Label();
                Control c;
                Panel p = new Panel();
                if (setting.isBool)
                {
                    c = new CheckBox();
                    if (value.Equals("True")) ((CheckBox)c).Checked = true;
                }
                else
                {
                    c = new TextBox();
                    c.Text = value;
                }
                //data
                l.Text = setting.externalName;
                l.Tag = setting.Category;
                c.Tag = setting.internalName;
                c.Name = setting.parent + setting.internalName;
                p.Tag = setting.internalLocation;
                l.Anchor = AnchorStyles.Top;
                
                //visuals
                p.Width = l.Width + c.Width;
                p.Height = l.Height + c.Height;
                l.Parent = p;
                l.Dock = DockStyle.Left;
                c.Parent = p;
                c.Dock = DockStyle.Right;

                //add doubleclick function to label for favoriting
                l.DoubleClick += setting_DoubleClick;
                if (setting.isBool)
                {
                    ((CheckBox)p.Controls[1]).CheckedChanged += Bibitinator_FavChanged;
                }
                else
                {
                    ((TextBox)p.Controls[1]).TextChanged += Bibitinator_FavChanged;
                }
                p.Parent = flow;
                
                //if this is the last property with that parent, add a line break
                int setDexNext;
                if (placeInFavs)
                {
                    List<settingsDictionary.setting> favorites = knownSettings.Where(x => x.favorite).ToList();
                    setDexNext = favorites.IndexOf(setting) + 1;
                    if (setDexNext < favorites.Count() && !favorites[setDexNext].parent.Equals(setting.parent)) flow.SetFlowBreak(p, true);
                }
                else
                {
                    setDexNext = knownSettings.IndexOf(setting) + 1;
                    if (setDexNext < knownSettings.Count() && !knownSettings[setDexNext].parent.Equals(setting.parent)) flow.SetFlowBreak(p, true);
                }
                
            }
        }

        private void setting_DoubleClick(object sender, EventArgs e)
        {
            //all of this to get the setting info for the clicked setting, needs to be improved
            JObject jOb;
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "\\UserSettingsData.json"))
            {
                var json = r.ReadToEnd();

                jOb = (JObject)JsonConvert.DeserializeObject(json);

            }
            JArray jAr = jOb.Value<JArray>("knownSettings");
            List<settingsDictionary.setting> knownSettings = jAr.ToObject<settingsDictionary.setting[]>().ToList();
            Control c = (Control)sender;
            settingsDictionary.setting setting = knownSettings.Find(x => x.internalName == c.Parent.Controls[1].Tag.ToString() && x.internalLocation == c.Parent.Tag.ToString());

            //if setting is in favorites, ask to remove, if its not, ask to add
            int index = jAr.IndexOf(jAr.Where(x => x.Value<string>("internalName") == setting.internalName && x.Value<string>("parent") == setting.parent).First());
            if (c.Parent.Parent.Parent.Name.Equals("Favorites"))
            {
                if (MessageBox.Show("Do you want to remove this from your favorites?", c.Parent.Controls[1].Tag.ToString(), MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                {
                    jAr[index]["favorite"] = false;
                }
            }
            else
            {
                if (MessageBox.Show("Do you want to add this to your favorites?", c.Parent.Controls[1].Tag.ToString(), MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                {
                    jAr[index]["favorite"] = true;
                }
            }
            //Edit UserSettingsData to have the changed favorite value
            jOb["knownSettings"].Replace(jAr);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\UserSettingsData.json", jOb.ToString());
            
            //edit worldsettingsjson to include any already made edits to values
            worldsettingsJson = replaceValuesInSettings(worldsettingsJson);
            
            //hold tab index, rebuild settings, then go to the tab index the user was in
            int tabIndex = worldSettingTabControl.SelectedIndex;
            populateSettings();
            worldSettingTabControl.SelectedIndex = tabIndex;
        }

        private void Bibitinator_FavChanged(object sender, EventArgs e)
        {
            
            Control c = (Control)sender;
            int flowIndex;
            string Category = c.Parent.Controls[0].Tag.ToString();
            string internalName = c.Tag.ToString();
            //if the user is editing a value in favorites, target the flow panel where the setting usually resides
            //and vise versa
            if (((TabPage)c.Parent.Parent.Parent).Name == "Favorites")
            {
                flowIndex = worldSettingTabControl.TabPages.IndexOfKey(Category);
            }
            else
            {
                flowIndex = worldSettingTabControl.TabPages.IndexOfKey("Favorites");
            }          
            FlowLayoutPanel flow = (FlowLayoutPanel)worldSettingTabControl.TabPages[flowIndex].Controls.Find("Flow", false).First();


            try
            {
                //find the text/check box in the flow panel
                var con = flow.Controls.Find(internalName, true);
                //if it doesnt exist, do nothing
                //it wont exist when you edit a setting that is not included in favorites
                if (con.Count() > 0)
                {
                    Control control = con.First();

                    //set the value to match
                    if (sender.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)control).Checked = ((CheckBox)sender).Checked;
                    }
                    else if (sender.GetType() == typeof(TextBox))
                    {
                        ((TextBox)control).Text = ((TextBox)sender).Text;
                    }
                }
                
            }
            catch { }

        }

        private string replaceValuesInSettings(string json)
        {
            //chars that represent values
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'N', 'f', 't' };
            //chars that represent non-values
            char[] stops = { '"', '}', ']', ',' };
            //clear temp directory
            if (File.Exists((Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/bibitinator/new/settings" + settingsExtension)))
            {
                File.Delete((Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/bibitinator/new/settings" + settingsExtension));
            }
            foreach (Control tab in worldSettingTabControl.TabPages)
            {
                Control flow = tab.Controls.Find("Flow", false).First();
                foreach (Control c in flow.Controls)
                {
                    //if targeting a label, do nothing
                    Type t = c.GetType();
                    if (!(t == typeof(Label)))
                    {

                        string location = string.Empty;
                        string value = string.Empty;
                        //box = input field
                        Control box = c.Controls[1];

                        //get internalLocation if it exists
                        if (!c.Tag.Equals(string.Empty))
                        {
                            location = c.Tag.ToString();
                        }
                        //format input data
                        if (box.GetType() == typeof(CheckBox))
                        {
                            box = (CheckBox)c.Controls[1];
                            value = ((CheckBox)box).Checked.ToString().ToLower();
                        }
                        else if (box.GetType() == typeof(TextBox))
                        {
                            box = (TextBox)c.Controls[1];
                            value = Regex.Replace(box.Text, ",", ".");
                            if (value.StartsWith('.')) value = 0 + value;
                            if (value.Equals("NaN")) value = "NaN\"";
                            else value = value.ToLower();
                        }

                        //replace old value with new value
                        int propIndex = json.IndexOf('"' + box.Tag.ToString() + '"', json.IndexOf(location)) + box.Tag.ToString().Length + 2;
                        int valIndex = json.IndexOfAny(nums, propIndex);
                        int stopRemoving = json.IndexOfAny(stops, valIndex) - valIndex;
                        json = json.Remove(valIndex, stopRemoving);
                        json = json.Insert(valIndex, value);
                    }
                    

                }            
            }
            //remove whitespace
            json = Regex.Replace(json, @"\s", "");
            return json;
        }
        //This is the funky one.
        private void zipInOrder(string targetFileName, string zipname)
        {
            int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
            
            //delete temp directory
            if (Directory.Exists(extractTo + "\\temp\\")) Directory.Delete(extractTo + "\\temp\\");
            
            //create bibites and eggs folders if they dont exist, happens when there are no bibites or eggs in the sim
            if (!Directory.Exists(extractTo + "\\bibites")) Directory.CreateDirectory(extractTo + "\\bibites");
            if (!Directory.Exists(extractTo + "\\eggs")) Directory.CreateDirectory(extractTo + "\\eggs");
            
            //create temporary directory
            Directory.CreateDirectory(extractTo + "\\temp\\");
            
            //In order for the game to load the zip correctly, the files must be in this order according to the offset: settings, scene, bibites, eggs
            //create temp directory, rename files to be in the correct order alphabetically
            //files within the zip folder need to be saved in the right order, then renamed back to their original names
            File.Copy(extractTo + "\\settings" + settingsExtension, extractTo + "\\temp\\asettings" + settingsExtension);
            File.Copy(extractTo + "\\scene" + sceneExtension, extractTo + "\\temp\\bscene" + sceneExtension);

            //move bibites and eggs folder to temp directory, these need to get put back at the end
            Directory.Move(extractTo + "\\bibites", extractTo + "\\temp\\cbibites");
            Directory.Move(extractTo + "\\eggs", extractTo + "\\temp\\deggs");
            if (File.Exists(targetFileName + zipname))
            {
                zipname = zipname.Insert(zipname.IndexOf('.'), Guid.NewGuid().ToString().Substring(24));
            }
            
            //create zip file
            ZipFile.CreateFromDirectory(extractTo + "\\temp", targetFileName + zipname);

            //create strange Behavior.txt, need to add and remove a file to the directory for the game to load correctly
            if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\bibites"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\bibites");
                File.Create(Directory.GetCurrentDirectory() + "\\bibites\\strange Behavior.txt");
            }
            ProcessStartInfo p = new ProcessStartInfo();
            
            //add strange behavior.txt to bibite directory while renamed 
            p.FileName = Directory.GetCurrentDirectory() + "\\7z.exe";
            p.Arguments = "a \"" + targetFileName + zipname + "\" \"" + Directory.GetCurrentDirectory() + "\\cbibites\\";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);
            x.WaitForExit();
            
            //remove strange behavior.txt
            p.FileName = Directory.GetCurrentDirectory() + "\\7z.exe";
            p.Arguments = "d \"" + targetFileName + zipname + "\" \"" + "cbibites\\strange Behavior.txt";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process y = Process.Start(p);
            
            //rename files back to correct names
            y.WaitForExit();
            p.FileName = Directory.GetCurrentDirectory() + "\\7z.exe";
            p.Arguments = "rn \"" + targetFileName + zipname + "\" asettings" + settingsExtension + " settings" + settingsExtension + " bscene" + sceneExtension + " scene" + sceneExtension + " cbibites\\ bibites\\ deggs\\ eggs\\";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process z = Process.Start(p);
            z.WaitForExit();
            
            //return the bibites & eggs folders back & delete the temp folder 
            Directory.Move(extractTo + "\\temp\\cbibites", extractTo + "\\bibites");
            Directory.Move(extractTo + "\\temp\\deggs", extractTo + "\\eggs");
            Directory.Delete(extractTo + "\\temp\\", true);
            MessageBox.Show("Save Completed");

            //Why does it work like this? I do not know, I assure you every strange extra step of complexity was the result 
            //of much headscratching, Its the only way for the game to load correctly, I know not why.
        }
        private void WorldResetButton_Click(object sender, EventArgs e)
        {
            //reset worldsettings to their original state when the world file was loaded
            worldsettingsJson = OgWorldsettingJson;
            populateSettings();
        }

        private void bibiteBrowseButton_Click(object sender, EventArgs e)
        {
            
            bibiteListView.Items.Clear();
            string userFilepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents";
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (Directory.Exists(userFilepath)) fileDialog.InitialDirectory = userFilepath;
            fileDialog.ShowDialog();
            bibiteBrowseTextBox.Text = fileDialog.FileName;
            //if file upload success, continute, otherwise show invalid
            if (bibiteBrowseTextBox.Text != null && File.Exists(fileDialog.FileName))
            {
                //set bibite extension
                if (fileDialog.FileName.EndsWith(".json"))
                {
                    bibiteExtension = ".json";
                }
                else if (fileDialog.FileName.EndsWith(".bb8"))
                {
                    bibiteExtension = ".bb8";
                }
                else
                {
                    worldFilePathTextBox.Text = "error";
                }
                try
                {
                    //initiate bibite collection
                    BibiteCollection col = new BibiteCollection();
                    string result = string.Empty;
                    using (StreamReader r = new StreamReader(fileDialog.FileName))
                    {
                        result = r.ReadToEnd();
                    }
                    col.json = result;
                    int nameStart = fileDialog.FileName.LastIndexOf("\\") + 1;
                    col.name = fileDialog.FileName.Substring(nameStart);
                    col.extractFolder = fileDialog.FileName.Substring(0, fileDialog.FileName.Length - col.name.Length);
                    BibiteEditor editorWindow = new BibiteEditor(col);
                    editorWindow.Show();
                }
                catch
                {

                }
            }
            else
            {
                worldFilePathTextBox.Text = "invalid";
            }
        }

        private void DownloadLatest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://drive.google.com/file/d/1qEVMuttBSJbB0fDtXi09xOSAlZv-5g8U/view?usp=sharing";
            myProcess.Start();
        }

        private void Github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://github.com/JustinBrownDev/Bibitinator";
            myProcess.Start();
        }
    }
}
