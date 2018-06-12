namespace OpenLawNZ
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkToPDFButton = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.downloadPDFButton = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.folderStructureComboBox = new System.Windows.Forms.ComboBox();
			this.removeCitationButton = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.resultsGridView = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.resultsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkToPDFButton);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Location = new System.Drawing.Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(393, 112);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Link to PDFs on openlaw.nz";
			// 
			// linkToPDFButton
			// 
			this.linkToPDFButton.Location = new System.Drawing.Point(312, 83);
			this.linkToPDFButton.Name = "linkToPDFButton";
			this.linkToPDFButton.Size = new System.Drawing.Size(75, 23);
			this.linkToPDFButton.TabIndex = 2;
			this.linkToPDFButton.Text = "Run";
			this.linkToPDFButton.UseVisualStyleBackColor = true;
			this.linkToPDFButton.Click += new System.EventHandler(this.linkToPDFButton_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Info;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(6, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(381, 58);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "Citation matches will link to openlaw.nz";
			// 
			// downloadPDFButton
			// 
			this.downloadPDFButton.Location = new System.Drawing.Point(312, 82);
			this.downloadPDFButton.Name = "downloadPDFButton";
			this.downloadPDFButton.Size = new System.Drawing.Size(75, 23);
			this.downloadPDFButton.TabIndex = 2;
			this.downloadPDFButton.Text = "Run";
			this.downloadPDFButton.UseVisualStyleBackColor = true;
			this.downloadPDFButton.Click += new System.EventHandler(this.downloadPDFButton_Click);
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Info;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(6, 19);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(381, 58);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "Citation matches will download PDF files into a folder adjacent to your document";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.folderStructureComboBox);
			this.groupBox2.Controls.Add(this.downloadPDFButton);
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Location = new System.Drawing.Point(4, 122);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(393, 112);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Download PDFs locally";
			// 
			// folderStructureComboBox
			// 
			this.folderStructureComboBox.FormattingEnabled = true;
			this.folderStructureComboBox.Items.AddRange(new object[] {
            "Court of Appeal Appelant",
            "Court of Appeal Respondent"});
			this.folderStructureComboBox.Location = new System.Drawing.Point(7, 84);
			this.folderStructureComboBox.Name = "folderStructureComboBox";
			this.folderStructureComboBox.Size = new System.Drawing.Size(187, 21);
			this.folderStructureComboBox.TabIndex = 3;
			this.folderStructureComboBox.Text = "Default folder structure";
			// 
			// removeCitationButton
			// 
			this.removeCitationButton.Location = new System.Drawing.Point(312, 83);
			this.removeCitationButton.Name = "removeCitationButton";
			this.removeCitationButton.Size = new System.Drawing.Size(75, 23);
			this.removeCitationButton.TabIndex = 2;
			this.removeCitationButton.Text = "Run";
			this.removeCitationButton.UseVisualStyleBackColor = true;
			this.removeCitationButton.Click += new System.EventHandler(this.removeCitationButton_Click);
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Info;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(6, 19);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(381, 58);
			this.textBox3.TabIndex = 1;
			this.textBox3.Text = "Citation links will be removed. Note: Downloaded files will not be deleted.";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.removeCitationButton);
			this.groupBox3.Controls.Add(this.textBox3);
			this.groupBox3.Location = new System.Drawing.Point(4, 240);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(393, 112);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Remove citation links";
			// 
			// resultsGridView
			// 
			this.resultsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.resultsGridView.Location = new System.Drawing.Point(4, 390);
			this.resultsGridView.Name = "resultsGridView";
			this.resultsGridView.ReadOnly = true;
			this.resultsGridView.Size = new System.Drawing.Size(393, 324);
			this.resultsGridView.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 371);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Results";
			// 
			// UserControl1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.resultsGridView);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(400, 843);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.resultsGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button linkToPDFButton;
        private System.Windows.Forms.Button downloadPDFButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button removeCitationButton;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox folderStructureComboBox;
        private System.Windows.Forms.DataGridView resultsGridView;
		private System.Windows.Forms.Label label1;
	}
}
