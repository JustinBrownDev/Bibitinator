using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Bibitinator.Models;
using Bibitinator.Models.BibiteTracer;
using System.Runtime;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Reflection;

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
            WorldSettingsReflect.Root obj = JsonConvert.DeserializeObject<WorldSettingsReflect.Root>(worldsettingsJson);
            //foreach property in worldsettings
            foreach (PropertyInfo prop in typeof(WorldSettingsReflect.Root).GetProperties())
            {
                //assign property to o
                var o = obj.GetType().GetRuntimeProperty(prop.Name).GetValue(obj);
                //verify property is not null, if property has multiple fields (materials), iterate over those fields
                if (prop.PropertyType.GetProperties().Count() > 1 && obj.GetType().GetProperty(prop.Name).GetValue(obj) != null)
                {
                    foreach (PropertyInfo info in typeof(WorldSettingsReflect.Materials).GetProperties())
                    {
                        var e = o.GetType().GetRuntimeProperty(info.Name).GetValue(o);
                        foreach(PropertyInfo info2 in typeof(WorldSettingsReflect.Materials).GetProperty(info.Name).PropertyType.GetProperties())
                        {
                            //assign each field to a panel
                            var n = e.GetType().GetRuntimeProperty(info2.Name).GetValue(e);
                            Label l = new Label();
                            TextBox t = new TextBox();
                            l.Text = System.Text.RegularExpressions.Regex.Replace(info2.Name, "[A-Z]", " $0");
                            l.Tag = "label";
                            t.Text = n.ToString();
                            t.Tag = info2.Name;
                            Panel p = new Panel();
                            p.Width = l.Width + t.Width;
                            p.Height = l.Height + t.Height;
                            l.Parent = p;
                            l.Dock = DockStyle.Left;
                            t.Parent = p;
                            t.Dock = DockStyle.Right;
                            p.Parent = worldSettingsFlow;
                        }                     
                    }
                }
                //for properties with 1 field, assign field to a panel
                else if (!(obj.GetType().GetProperty(prop.Name).GetValue(obj) == null))
                {           
                    Label l = new Label();
                    TextBox t = new TextBox();                 
                    l.Text = System.Text.RegularExpressions.Regex.Replace(prop.Name, "[A-Z]", " $0");
                    l.Tag = "label";
                    t.Text = o.GetType().GetProperty("Value").GetValue(o).ToString();
                    t.Tag = prop.Name;
                    Panel p = new Panel();
                    p.Width = l.Width + t.Width;
                    p.Height = l.Height + t.Height;
                    l.Parent = p;
                    l.Dock = DockStyle.Left;
                    t.Parent = p;
                    t.Dock = DockStyle.Right;
                    p.Parent = worldSettingsFlow;
                }              
            }
        }
        private string replaceValuesInSettings(string json)
        {

            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', 'N', 'f', 't' };
            char[] stops = { ',', '}', ']' };
            if (File.Exists((Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/bibitinator/new/settings" + settingsExtension)))
            {
                File.Delete((Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/bibitinator/new/settings" + settingsExtension));
            }
            foreach (Control c in worldSettingsFlow.Controls)
            {
                foreach (Control o in c.Controls)
                {
                    //ignore labels (iterate over the text boxes)
                    if (!o.Tag.Equals("label") && json.Contains(o.Tag.ToString()))
                    {
                        TextBox box = (TextBox)o;
                        //add 0 to values with a leading decimal point
                        if(box.Text.StartsWith('.')) box.Text = 0 + box.Text;
                        string value = box.Text;
                        if (value.Equals("NaN")) value = "NaN\"";
                        else value = value.ToLower();
                        int propIndex = json.IndexOf('"' + box.Tag.ToString() + '"') + box.Tag.ToString().Length + 2;
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
            if(!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bibites"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bibites");
                File.Create(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bibites\\strange Behavior.txt");
            }            
            //rename files back to correct names within the directory 
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\7z.exe";
            p.Arguments = "a \"" + targetFileName + zipname + "\" \"" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\cbibites\\";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);
            x.WaitForExit();

            p.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\7z.exe";
            p.Arguments = "d \"" + targetFileName + zipname + "\" \"" + "cbibites\\strange Behavior.txt";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process y = Process.Start(p);
            //
            y.WaitForExit();
            p.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\7z.exe";
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
                        var json = r.ReadToEnd();
                        jobj = JObject.Parse(json);
                        result = jobj.ToString(Newtonsoft.Json.Formatting.Indented);
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
    }
}
