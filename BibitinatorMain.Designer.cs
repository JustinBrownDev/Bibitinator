namespace Bibitinator
{
    partial class Bibitinator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.exportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bibiteListView = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.WorldResetButton = new System.Windows.Forms.Button();
            this.worldSettingsSaveButton = new System.Windows.Forms.Button();
            this.worldSettingsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.openWorldZipDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPage0.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.Location = new System.Drawing.Point(274, 6);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(75, 23);
            this.AnalyzeButton.TabIndex = 7;
            this.AnalyzeButton.Text = "Analyze";
            this.AnalyzeButton.UseVisualStyleBackColor = true;
            this.AnalyzeButton.Click += new System.EventHandler(this.Analyze);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(211, 35);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(56, 25);
            this.browseButton.TabIndex = 6;
            this.browseButton.Text = "browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(8, 36);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(255, 23);
            this.filePathTextBox.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage0);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 2;
            this.tabControl1.Size = new System.Drawing.Size(1344, 766);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage0
            // 
            this.tabPage0.Controls.Add(this.exportButton);
            this.tabPage0.Controls.Add(this.label1);
            this.tabPage0.Controls.Add(this.bibiteListView);
            this.tabPage0.Controls.Add(this.AnalyzeButton);
            this.tabPage0.Controls.Add(this.browseButton);
            this.tabPage0.Controls.Add(this.filePathTextBox);
            this.tabPage0.Location = new System.Drawing.Point(4, 24);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage0.Size = new System.Drawing.Size(1336, 738);
            this.tabPage0.TabIndex = 5;
            this.tabPage0.Text = "File";
            this.tabPage0.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(6, 65);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 10;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Upload World Folder";
            // 
            // bibiteListView
            // 
            this.bibiteListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.bibiteListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bibiteListView.HideSelection = false;
            this.bibiteListView.Location = new System.Drawing.Point(274, 36);
            this.bibiteListView.MultiSelect = false;
            this.bibiteListView.Name = "bibiteListView";
            this.bibiteListView.Size = new System.Drawing.Size(1056, 696);
            this.bibiteListView.TabIndex = 8;
            this.bibiteListView.UseCompatibleStateImageBehavior = false;
            this.bibiteListView.View = System.Windows.Forms.View.List;
            this.bibiteListView.DoubleClick += new System.EventHandler(this.Analyze);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer1);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1336, 738);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "World Settings";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.WorldResetButton);
            this.splitContainer1.Panel1.Controls.Add(this.worldSettingsSaveButton);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.worldSettingsFlow);
            this.splitContainer1.Size = new System.Drawing.Size(1330, 732);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.TabIndex = 1;
            // 
            // WorldResetButton
            // 
            this.WorldResetButton.Location = new System.Drawing.Point(5, 32);
            this.WorldResetButton.Name = "WorldResetButton";
            this.WorldResetButton.Size = new System.Drawing.Size(75, 23);
            this.WorldResetButton.TabIndex = 1;
            this.WorldResetButton.Text = "Reset";
            this.WorldResetButton.UseVisualStyleBackColor = true;
            this.WorldResetButton.Click += new System.EventHandler(this.WorldResetButton_Click);
            // 
            // worldSettingsSaveButton
            // 
            this.worldSettingsSaveButton.Location = new System.Drawing.Point(3, 3);
            this.worldSettingsSaveButton.Name = "worldSettingsSaveButton";
            this.worldSettingsSaveButton.Size = new System.Drawing.Size(75, 23);
            this.worldSettingsSaveButton.TabIndex = 0;
            this.worldSettingsSaveButton.Text = "Save";
            this.worldSettingsSaveButton.UseVisualStyleBackColor = true;
            this.worldSettingsSaveButton.Click += new System.EventHandler(this.worldSettingsSaveButton_Click);
            // 
            // worldSettingsFlow
            // 
            this.worldSettingsFlow.AutoScroll = true;
            this.worldSettingsFlow.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.worldSettingsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.worldSettingsFlow.Location = new System.Drawing.Point(0, 0);
            this.worldSettingsFlow.Name = "worldSettingsFlow";
            this.worldSettingsFlow.Size = new System.Drawing.Size(1240, 732);
            this.worldSettingsFlow.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // Bibitinator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 766);
            this.Controls.Add(this.tabControl1);
            this.Name = "Bibitinator";
            this.Text = "Bibitinator";
            this.tabControl1.ResumeLayout(false);
            this.tabPage0.ResumeLayout(false);
            this.tabPage0.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage0;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView bibiteListView;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.OpenFileDialog openWorldZipDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.FlowLayoutPanel worldSettingsFlow;
        private System.Windows.Forms.Button worldSettingsSaveButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button WorldResetButton;
    }
}

