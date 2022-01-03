﻿using System;
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
            filePathTextBox.Text = openWorldZipDialog.FileName;
            if (filePathTextBox.Text != null && File.Exists(openWorldZipDialog.FileName) && openWorldZipDialog.FileName.EndsWith(".zip"))
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
                    filePathTextBox.Text = "error";
                }
                //populate listview
                string[] bibiteList = Directory.GetFiles(extractTo + "\\bibites");
                foreach (string fileName in bibiteList)
                {
                    string bibiteName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                    bibiteListView.Items.Add(bibiteName);
                }
                bibiteListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                using (StreamReader r = new StreamReader(extractTo + "\\settings" + settingsExtension))
                {
                    var json = r.ReadToEnd();
                    worldsettingsJson = json.ToString();
                }
                populateSettings();
            }
            else
            {
                filePathTextBox.Text = "invalid";
            }
            
        }
        
        private void Analyze(object sender, EventArgs e)
        {
            if(bibiteListView.SelectedIndices.Count > 0)
            {
                BibiteCollection col = new BibiteCollection();
                col.name = bibiteListView.SelectedItems[0].Text ;
                col.extractFolder = extractTo;
                string filePath = extractTo + "\\bibites\\" +col.name;
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
            int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
            string zipName = openWorldZipDialog.FileName.Substring(lastSlashIndex, openWorldZipDialog.FileName.LastIndexOf('.') - lastSlashIndex);
            string targetFileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + 
            "\\AppData\\LocalLow\\The Bibites\\The Bibites\\Bibitinator\\" + zipName + ".zip";

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = targetFileName;
            folderBrowserDialog.ShowDialog();

            zipInOrder(folderBrowserDialog.SelectedPath);

        }
        private void populateSettings()
        {
            WorldSettingsReflect.Root obj = JsonConvert.DeserializeObject<WorldSettingsReflect.Root>(worldsettingsJson);
            foreach (PropertyInfo prop in typeof(WorldSettingsReflect.Root).GetProperties())
            {
                var o = obj.GetType().GetRuntimeProperty(prop.Name).GetValue(obj);
                if (prop.PropertyType.GetProperties().Count() > 1 && obj.GetType().GetProperty(prop.Name).GetValue(obj) != null)
                {
                    foreach (PropertyInfo info in typeof(WorldSettingsReflect.Materials).GetProperties())
                    {
                        var e = o.GetType().GetRuntimeProperty(info.Name).GetValue(o);
                        foreach(PropertyInfo info2 in typeof(WorldSettingsReflect.Materials).GetProperty(info.Name).PropertyType.GetProperties())
                        {
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
                    if (!o.Tag.Equals("label"))
                    {
                        TextBox box = (TextBox)o;
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
        private void zipInOrder(string targetFileName)
        {
            int lastSlashIndex = openWorldZipDialog.FileName.LastIndexOf('\\');
            
            if (Directory.Exists(extractTo + "\\temp\\")) Directory.Delete(extractTo + "\\temp\\");
            Directory.CreateDirectory(extractTo + "\\temp\\");

            //create temp directory, rename files to be in the correct order alphabetically
            //files within the zip folder need to be saved in the right order
            File.Copy(extractTo + "\\settings" + settingsExtension, extractTo + "\\temp\\asettings" + settingsExtension);
            File.Copy(extractTo + "\\scene" + sceneExtension, extractTo + "\\temp\\bscene" + sceneExtension);
            Directory.Move(extractTo + "\\bibites", extractTo + "\\temp\\cbibites");
            Directory.Move(extractTo + "\\eggs", extractTo + "\\temp\\deggs");

            //create zip file
            ZipFile.CreateFromDirectory(extractTo + "\\temp", targetFileName);

            //rename files back to correct names within the directory 
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = "C:\\Users\\justi\\source\\repos\\Bibitinator\\7z.exe";
            p.Arguments = "rn \"" + targetFileName + "\" asettings" + settingsExtension + " settings" + settingsExtension + " bscene" + sceneExtension + " scene" + sceneExtension + " cbibites\\ bibites\\ deggs\\ eggs\\";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);

            //return the bibites & eggs folders back & delete the temp folder 
            x.WaitForExit();
            Directory.Move(extractTo + "\\temp\\cbibites", extractTo + "\\bibites");
            Directory.Move(extractTo + "\\temp\\deggs", extractTo + "\\eggs");
            Directory.Delete(extractTo + "\\temp\\", true);
        }
        private void WorldResetButton_Click(object sender, EventArgs e)
        {
            worldSettingsFlow.Controls.Clear();
            populateSettings();
        }
    }
}