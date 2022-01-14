
namespace Bibitinator
{
    partial class BibiteEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EditorTabControl = new System.Windows.Forms.TabControl();
            this.brainEditor = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ConnectionsTreeView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BrainResetButton = new System.Windows.Forms.Button();
            this.BrainSaveButton = new System.Windows.Forms.Button();
            this.BrainSaveCopyButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.AddNeuronComboBox = new System.Windows.Forms.ComboBox();
            this.addSynapse = new System.Windows.Forms.Button();
            this.AddNeuronButton = new System.Windows.Forms.Button();
            this.inputComboBox = new System.Windows.Forms.ComboBox();
            this.outputComboBox = new System.Windows.Forms.ComboBox();
            this.strengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MovingLabel = new System.Windows.Forms.Label();
            this.BrainEditorPanel = new System.Windows.Forms.Panel();
            this.Genes = new System.Windows.Forms.TabPage();
            this.GenesResetButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.editorflow = new System.Windows.Forms.FlowLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.Properties = new System.Windows.Forms.TabPage();
            this.propertiesTree = new System.Windows.Forms.TreeView();
            this.EditorTabControl.SuspendLayout();
            this.brainEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strengthUpDown)).BeginInit();
            this.Genes.SuspendLayout();
            this.Properties.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditorTabControl
            // 
            this.EditorTabControl.Controls.Add(this.brainEditor);
            this.EditorTabControl.Controls.Add(this.Genes);
            this.EditorTabControl.Controls.Add(this.Properties);
            this.EditorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorTabControl.Location = new System.Drawing.Point(0, 0);
            this.EditorTabControl.Name = "EditorTabControl";
            this.EditorTabControl.SelectedIndex = 0;
            this.EditorTabControl.Size = new System.Drawing.Size(1389, 704);
            this.EditorTabControl.TabIndex = 0;
            // 
            // brainEditor
            // 
            this.brainEditor.Controls.Add(this.splitContainer1);
            this.brainEditor.Location = new System.Drawing.Point(4, 24);
            this.brainEditor.Name = "brainEditor";
            this.brainEditor.Size = new System.Drawing.Size(1381, 676);
            this.brainEditor.TabIndex = 3;
            this.brainEditor.Text = "Brain Editor";
            this.brainEditor.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Size = new System.Drawing.Size(1381, 676);
            this.splitContainer1.SplitterDistance = 788;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ConnectionsTreeView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(788, 643);
            this.panel2.TabIndex = 3;
            // 
            // ConnectionsTreeView
            // 
            this.ConnectionsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionsTreeView.Location = new System.Drawing.Point(0, 0);
            this.ConnectionsTreeView.Name = "ConnectionsTreeView";
            this.ConnectionsTreeView.Size = new System.Drawing.Size(788, 643);
            this.ConnectionsTreeView.TabIndex = 1;
            this.ConnectionsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ConnectionsTreeView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BrainResetButton);
            this.panel1.Controls.Add(this.BrainSaveButton);
            this.panel1.Controls.Add(this.BrainSaveCopyButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 33);
            this.panel1.TabIndex = 2;
            // 
            // BrainResetButton
            // 
            this.BrainResetButton.Location = new System.Drawing.Point(3, 3);
            this.BrainResetButton.Name = "BrainResetButton";
            this.BrainResetButton.Size = new System.Drawing.Size(52, 23);
            this.BrainResetButton.TabIndex = 9;
            this.BrainResetButton.Text = "Reset";
            this.BrainResetButton.UseVisualStyleBackColor = true;
            this.BrainResetButton.Click += new System.EventHandler(this.BrainResetButton_Click);
            // 
            // BrainSaveButton
            // 
            this.BrainSaveButton.Location = new System.Drawing.Point(61, 3);
            this.BrainSaveButton.Name = "BrainSaveButton";
            this.BrainSaveButton.Size = new System.Drawing.Size(58, 23);
            this.BrainSaveButton.TabIndex = 8;
            this.BrainSaveButton.Text = "Save";
            this.BrainSaveButton.UseVisualStyleBackColor = true;
            this.BrainSaveButton.Click += new System.EventHandler(this.BrainSaveButton_Click);
            // 
            // BrainSaveCopyButton
            // 
            this.BrainSaveCopyButton.Location = new System.Drawing.Point(125, 3);
            this.BrainSaveCopyButton.Name = "BrainSaveCopyButton";
            this.BrainSaveCopyButton.Size = new System.Drawing.Size(86, 23);
            this.BrainSaveCopyButton.TabIndex = 7;
            this.BrainSaveCopyButton.Text = "Save as Copy";
            this.BrainSaveCopyButton.UseVisualStyleBackColor = true;
            this.BrainSaveCopyButton.Click += new System.EventHandler(this.BrainSaveCopyButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            this.splitContainer2.Panel1.Controls.Add(this.MovingLabel);
            this.splitContainer2.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AccessibleName = "editorPanel";
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.BrainEditorPanel);
            this.splitContainer2.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer2.Size = new System.Drawing.Size(589, 676);
            this.splitContainer2.SplitterDistance = 62;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.AddNeuronComboBox);
            this.panel3.Controls.Add(this.addSynapse);
            this.panel3.Controls.Add(this.AddNeuronButton);
            this.panel3.Controls.Add(this.inputComboBox);
            this.panel3.Controls.Add(this.outputComboBox);
            this.panel3.Controls.Add(this.strengthUpDown);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 62);
            this.panel3.TabIndex = 2;
            // 
            // AddNeuronComboBox
            // 
            this.AddNeuronComboBox.FormattingEnabled = true;
            this.AddNeuronComboBox.Location = new System.Drawing.Point(126, 33);
            this.AddNeuronComboBox.Name = "AddNeuronComboBox";
            this.AddNeuronComboBox.Size = new System.Drawing.Size(144, 23);
            this.AddNeuronComboBox.TabIndex = 11;
            // 
            // addSynapse
            // 
            this.addSynapse.Location = new System.Drawing.Point(3, 3);
            this.addSynapse.Name = "addSynapse";
            this.addSynapse.Size = new System.Drawing.Size(117, 23);
            this.addSynapse.TabIndex = 0;
            this.addSynapse.Text = "Add Synapse";
            this.addSynapse.UseVisualStyleBackColor = true;
            this.addSynapse.Click += new System.EventHandler(this.addSynapse_Click);
            // 
            // AddNeuronButton
            // 
            this.AddNeuronButton.Location = new System.Drawing.Point(3, 32);
            this.AddNeuronButton.Name = "AddNeuronButton";
            this.AddNeuronButton.Size = new System.Drawing.Size(117, 23);
            this.AddNeuronButton.TabIndex = 10;
            this.AddNeuronButton.Text = "Add Neuron";
            this.AddNeuronButton.UseVisualStyleBackColor = true;
            this.AddNeuronButton.Click += new System.EventHandler(this.AddNeuronButton_Click);
            // 
            // inputComboBox
            // 
            this.inputComboBox.FormattingEnabled = true;
            this.inputComboBox.Location = new System.Drawing.Point(126, 5);
            this.inputComboBox.Name = "inputComboBox";
            this.inputComboBox.Size = new System.Drawing.Size(144, 23);
            this.inputComboBox.TabIndex = 1;
            // 
            // outputComboBox
            // 
            this.outputComboBox.FormattingEnabled = true;
            this.outputComboBox.Location = new System.Drawing.Point(301, 5);
            this.outputComboBox.Name = "outputComboBox";
            this.outputComboBox.Size = new System.Drawing.Size(136, 23);
            this.outputComboBox.TabIndex = 2;
            // 
            // strengthUpDown
            // 
            this.strengthUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.strengthUpDown.Location = new System.Drawing.Point(501, 4);
            this.strengthUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.strengthUpDown.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            -2147483648});
            this.strengthUpDown.Name = "strengthUpDown";
            this.strengthUpDown.Size = new System.Drawing.Size(78, 23);
            this.strengthUpDown.TabIndex = 6;
            this.strengthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Strength";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "To";
            // 
            // MovingLabel
            // 
            this.MovingLabel.AutoSize = true;
            this.MovingLabel.Location = new System.Drawing.Point(3, 42);
            this.MovingLabel.Name = "MovingLabel";
            this.MovingLabel.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.MovingLabel.Size = new System.Drawing.Size(0, 22);
            this.MovingLabel.TabIndex = 0;
            // 
            // BrainEditorPanel
            // 
            this.BrainEditorPanel.AutoScroll = true;
            this.BrainEditorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrainEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrainEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.BrainEditorPanel.Name = "BrainEditorPanel";
            this.BrainEditorPanel.Size = new System.Drawing.Size(589, 610);
            this.BrainEditorPanel.TabIndex = 0;
            // 
            // Genes
            // 
            this.Genes.Controls.Add(this.GenesResetButton);
            this.Genes.Controls.Add(this.saveButton);
            this.Genes.Controls.Add(this.editorflow);
            this.Genes.Controls.Add(this.splitter1);
            this.Genes.Location = new System.Drawing.Point(4, 24);
            this.Genes.Name = "Genes";
            this.Genes.Padding = new System.Windows.Forms.Padding(3);
            this.Genes.Size = new System.Drawing.Size(1381, 676);
            this.Genes.TabIndex = 0;
            this.Genes.Text = "Genes Editor";
            this.Genes.UseVisualStyleBackColor = true;
            // 
            // GenesResetButton
            // 
            this.GenesResetButton.Location = new System.Drawing.Point(8, 50);
            this.GenesResetButton.Name = "GenesResetButton";
            this.GenesResetButton.Size = new System.Drawing.Size(75, 23);
            this.GenesResetButton.TabIndex = 3;
            this.GenesResetButton.Text = "Reset";
            this.GenesResetButton.UseVisualStyleBackColor = true;
            this.GenesResetButton.Click += new System.EventHandler(this.GenesResetButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(8, 6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // editorflow
            // 
            this.editorflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorflow.Location = new System.Drawing.Point(97, 3);
            this.editorflow.Name = "editorflow";
            this.editorflow.Size = new System.Drawing.Size(1281, 670);
            this.editorflow.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitter1.Location = new System.Drawing.Point(3, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(94, 670);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // Properties
            // 
            this.Properties.Controls.Add(this.propertiesTree);
            this.Properties.Location = new System.Drawing.Point(4, 24);
            this.Properties.Name = "Properties";
            this.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.Properties.Size = new System.Drawing.Size(1381, 676);
            this.Properties.TabIndex = 1;
            this.Properties.Text = "Property Explorer";
            this.Properties.UseVisualStyleBackColor = true;
            // 
            // propertiesTree
            // 
            this.propertiesTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesTree.Location = new System.Drawing.Point(3, 3);
            this.propertiesTree.Name = "propertiesTree";
            this.propertiesTree.Size = new System.Drawing.Size(1375, 670);
            this.propertiesTree.TabIndex = 0;
            // 
            // BibiteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 704);
            this.Controls.Add(this.EditorTabControl);
            this.Name = "BibiteEditor";
            this.Text = "BibiteEditor";
            this.Load += new System.EventHandler(this.BibiteEditor_Load);
            this.EditorTabControl.ResumeLayout(false);
            this.brainEditor.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strengthUpDown)).EndInit();
            this.Genes.ResumeLayout(false);
            this.Properties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl EditorTabControl;
        private System.Windows.Forms.TabPage Genes;
        private System.Windows.Forms.TabPage Properties;
        private System.Windows.Forms.TreeView propertiesTree;
        private System.Windows.Forms.FlowLayoutPanel editorflow;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TabPage brainEditor;
        private System.Windows.Forms.Button GenesResetButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox outputComboBox;
        private System.Windows.Forms.ComboBox inputComboBox;
        private System.Windows.Forms.Button addSynapse;
        private System.Windows.Forms.NumericUpDown strengthUpDown;
        private System.Windows.Forms.TreeView ConnectionsTreeView;
        private System.Windows.Forms.Panel BrainEditorPanel;
        private System.Windows.Forms.Button BrainResetButton;
        private System.Windows.Forms.Button BrainSaveButton;
        private System.Windows.Forms.Button BrainSaveCopyButton;
        private System.Windows.Forms.Label MovingLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AddNeuronButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox AddNeuronComboBox;
    }
}