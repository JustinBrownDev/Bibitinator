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

namespace Bibitinator
{
    public partial class Bibitinator : Form
    {
        public Bibitinator()
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabPage0;
        }
        
        public string worldsettingsJson = string.Empty;
        public string bibiteExtension = string.Empty;
        public string settingsExtension = string.Empty;
        public string sceneExtension = string.Empty;
        public string extractTo;
        private void browseButton_Click(object sender, EventArgs e)
        {
            bibiteListView.Items.Clear();
            string userFilepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\LocalLow\\The Bibites\\The Bibites";
            string tempFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\bibitinator";

            if (Directory.Exists(userFilepath)) openWorldZipDialog.InitialDirectory = userFilepath;
            openWorldZipDialog.ShowDialog();
            worldFilePathTextBox.Text = openWorldZipDialog.FileName;
            if (worldFilePathTextBox.Text != null && File.Exists(openWorldZipDialog.FileName) && openWorldZipDialog.FileName.EndsWith(".zip"))
            {
                //extract the zip file
                Cursor.Current = Cursors.WaitCursor;
                int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
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
                //populate listview
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
                    worldsettingsJson = json.ToString();
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
                BibiteCollection col = new BibiteCollection();
                col.name = bibiteListView.SelectedItems[0].Text ;
                col.extractFolder = extractTo + "\\bibites\\";
                string filePath = extractTo + "\\bibites\\" + col.name;
                string result = string.Empty;
                JObject jobj = new JObject();
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
            string json = replaceValuesInSettings(worldsettingsJson);
            if (json == "") return;
            File.WriteAllText(extractTo + "\\settings" + settingsExtension, json);

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
            if(bibiteListView.Items.Count > 0)
            {
                int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
                string zipName = openWorldZipDialog.FileName.Substring(lastSlashIndex);
                string targetFileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                "\\AppData\\LocalLow\\The Bibites\\The Bibites\\Bibitinator\\";
                if (!Directory.Exists(targetFileName))
                {
                    Directory.CreateDirectory(targetFileName);
                }
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = targetFileName;
                folderBrowserDialog.ShowDialog();

                zipInOrder(folderBrowserDialog.SelectedPath, zipName);
            }       
        }
        private void populateSettings()
        {
            worldSettingsFlow.Controls.Clear();
            //create model for world settings
            JObject obj = (JObject)JsonConvert.DeserializeObject(worldsettingsJson);
            //Invariant Culture
            if (worldsettingsJson == "") return;
            //foreach property in worldsettings
            //foreach (PropertyInfo prop in typeof(WorldSettingsReflect.Root).GetProperties())
            string location = string.Empty;
            foreach (JToken prop in obj.Children())
            {
                settingsDigger(prop);
            }            
            void settingsDigger (JToken prop)
            { 
                if(prop.Values().ToList().Count() > 1)
                {
                    if (prop is JProperty) location = Regex.Replace(((JProperty)prop).Name, "Settings", "");

                    foreach (JToken subprop in prop.Children())
                    { 
                        settingsDigger(subprop);
                    }
                    location = string.Empty;
                }
                else
                {
                    buildBox(prop, location);
                }
            }
            void buildBox (JToken prop, string location) 
            {
                string name = ((JProperty)prop).Name;
                string value = string.Empty;
                if (!prop.Children().First().HasValues)
                {
                    value = prop.Children().First().Value<string>();
                }
                else
                {
                    value = prop.Children().First().Children().First().Children().Values().ToList().First().ToString();
                }
                Label l = new Label();
                TextBox t = new TextBox();
                l.Text = location + " " + Regex.Replace(name, "[A-Z]", " $0");
                l.Tag = "label";
                t.Text = value;
                t.Tag = name;
                Panel p = new Panel();
                p.Width = l.Width + t.Width;
                p.Height = l.Height + t.Height;
                p.Tag = location;
                l.Parent = p;
                l.Dock = DockStyle.Left;
                t.Parent = p;
                t.Dock = DockStyle.Right;
                p.Parent = worldSettingsFlow;
            }
        }
        private string replaceValuesInSettings(string json)
        {

            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'N', 'f', 't' };
            char[] stops = { '"', '}', ']', ',' };
            if (File.Exists((Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/bibitinator/new/settings" + settingsExtension)))
            {
                File.Delete((Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/bibitinator/new/settings" + settingsExtension));
            }
            foreach (Control c in worldSettingsFlow.Controls)
            {
                string location = string.Empty;
                if (!c.Tag.Equals(string.Empty)) location = c.Tag + "Settings";
                
                TextBox box = (TextBox)c.Controls[1];
                string value = Regex.Replace(box.Text, ",", ".");
                if (value.StartsWith('.')) value = 0 + value;
                if (value.Equals("NaN")) value = "NaN\"";
                else value = value.ToLower();
                int propIndex = json.IndexOf('"' + box.Tag.ToString() + '"', json.IndexOf(location)) + box.Tag.ToString().Length + 2;
                int valIndex = json.IndexOfAny(nums, propIndex);
                int stopRemoving = json.IndexOfAny(stops, valIndex) - valIndex;
                json = json.Remove(valIndex, stopRemoving);
                json = json.Insert(valIndex, value);
            }
            //remove whitespace
            json = Regex.Replace(json, @"\s", "");
            return json;
        }
        //This is the funky one.
        private void zipInOrder(string targetFileName, string zipname)
        {
            int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
            
            if (Directory.Exists(extractTo + "\\temp\\")) Directory.Delete(extractTo + "\\temp\\");
            Directory.CreateDirectory(extractTo + "\\temp\\");
            
            //In order for the game to load the zip correctly, the files must be in this order according to the offset: settings, scene, bibites, eggs
            //create temp directory, rename files to be in the correct order alphabetically
            //files within the zip folder need to be saved in the right order, then renamed back to their original names
            File.Copy(extractTo + "\\settings" + settingsExtension, extractTo + "\\temp\\asettings" + settingsExtension);
            File.Copy(extractTo + "\\scene" + sceneExtension, extractTo + "\\temp\\bscene" + sceneExtension);
            Directory.Move(extractTo + "\\bibites", extractTo + "\\temp\\cbibites");
            Directory.Move(extractTo + "\\eggs", extractTo + "\\temp\\deggs");
            if (File.Exists(targetFileName + zipname))
            {
                zipname = zipname.Insert(zipname.IndexOf('.'), Guid.NewGuid().ToString().Substring(24));
            }
            
            //create zip file
            ZipFile.CreateFromDirectory(extractTo + "\\temp", targetFileName + zipname);
            if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\bibites"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\bibites");
                File.Create(Directory.GetCurrentDirectory() + "\\bibites\\strange Behavior.txt");
            }
            //rename files back to correct names within the directory
            ProcessStartInfo p = new ProcessStartInfo();
            
            p.FileName = Directory.GetCurrentDirectory() + "\\7z.exe";
            p.Arguments = "a \"" + targetFileName + zipname + "\" \"" + Directory.GetCurrentDirectory() + "\\cbibites\\";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);
            x.WaitForExit();

            p.FileName = Directory.GetCurrentDirectory() + "\\7z.exe";
            p.Arguments = "d \"" + targetFileName + zipname + "\" \"" + "cbibites\\strange Behavior.txt";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process y = Process.Start(p);
            //
            y.WaitForExit();
            p.FileName = Directory.GetCurrentDirectory() + "\\7z.exe";
            p.Arguments = "rn \"" + targetFileName + zipname + "\" asettings" + settingsExtension + " settings" + settingsExtension + " bscene" + sceneExtension + " scene" + sceneExtension + " cbibites\\ bibites\\ deggs\\ eggs\\";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process z = Process.Start(p);
            
            //return the bibites & eggs folders back & delete the temp folder 
            z.WaitForExit();
            
            Directory.Move(extractTo + "\\temp\\cbibites", extractTo + "\\bibites");
            Directory.Move(extractTo + "\\temp\\deggs", extractTo + "\\eggs");
            Directory.Delete(extractTo + "\\temp\\", true);
            MessageBox.Show("Save Completed");
        }
        private void WorldResetButton_Click(object sender, EventArgs e)
        {
            worldSettingsFlow.Controls.Clear();
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
            if (bibiteBrowseTextBox.Text != null && File.Exists(fileDialog.FileName))
            {
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
                    BibiteCollection col = new BibiteCollection();
                    string result = string.Empty;
                    JObject jobj = new JObject();
                    using (StreamReader r = new StreamReader(fileDialog.FileName))
                    {
                        //var json = r.ReadToEnd();
                        //jobj = JObject.Parse(json);
                        //result = jobj.ToString();
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
