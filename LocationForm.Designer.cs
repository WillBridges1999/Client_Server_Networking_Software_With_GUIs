namespace location
{
    partial class LocationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationForm));
            this.ServerNameLabel = new System.Windows.Forms.Label();
            this.servernameTextbox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.portTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.locationTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.debugCheckbox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.protocolComboBox = new System.Windows.Forms.ComboBox();
            this.timeoutNumbox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumbox)).BeginInit();
            this.SuspendLayout();
            // 
            // ServerNameLabel
            // 
            this.ServerNameLabel.AutoSize = true;
            this.ServerNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.ServerNameLabel.Location = new System.Drawing.Point(21, 20);
            this.ServerNameLabel.Name = "ServerNameLabel";
            this.ServerNameLabel.Size = new System.Drawing.Size(130, 13);
            this.ServerNameLabel.TabIndex = 0;
            this.ServerNameLabel.Text = "What is the Server name?";
            // 
            // servernameTextbox
            // 
            this.servernameTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.servernameTextbox.Location = new System.Drawing.Point(24, 36);
            this.servernameTextbox.Name = "servernameTextbox";
            this.servernameTextbox.Size = new System.Drawing.Size(129, 20);
            this.servernameTextbox.TabIndex = 2;
            this.servernameTextbox.TextChanged += new System.EventHandler(this.serverNameTextBox_TextChanged);
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.submitButton.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.Black;
            this.submitButton.Location = new System.Drawing.Point(348, 229);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(93, 52);
            this.submitButton.TabIndex = 3;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(242, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "What is the Port?";
            // 
            // portTextbox
            // 
            this.portTextbox.Location = new System.Drawing.Point(245, 36);
            this.portTextbox.Name = "portTextbox";
            this.portTextbox.Size = new System.Drawing.Size(129, 20);
            this.portTextbox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(21, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "What is the User name?";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(24, 89);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(129, 20);
            this.usernameTextbox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(242, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "What is the Location?";
            // 
            // locationTextbox
            // 
            this.locationTextbox.Location = new System.Drawing.Point(245, 89);
            this.locationTextbox.Name = "locationTextbox";
            this.locationTextbox.Size = new System.Drawing.Size(129, 20);
            this.locationTextbox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(242, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Do you want the Debug feature?";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(21, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(286, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "How long before Timeout, in Milli-Secs? (Default is 1000ms)";
            // 
            // debugCheckbox
            // 
            this.debugCheckbox.AutoSize = true;
            this.debugCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.debugCheckbox.Location = new System.Drawing.Point(245, 147);
            this.debugCheckbox.Name = "debugCheckbox";
            this.debugCheckbox.Size = new System.Drawing.Size(83, 17);
            this.debugCheckbox.TabIndex = 15;
            this.debugCheckbox.Text = "Tick for Yes";
            this.debugCheckbox.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(21, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(173, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "What protocol do you want to use?";
            // 
            // protocolComboBox
            // 
            this.protocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.protocolComboBox.FormattingEnabled = true;
            this.protocolComboBox.Items.AddRange(new object[] {
            "whois",
            "HTTP/0.9",
            "HTTP/1.0",
            "HTTP/1.1"});
            this.protocolComboBox.Location = new System.Drawing.Point(24, 140);
            this.protocolComboBox.Name = "protocolComboBox";
            this.protocolComboBox.Size = new System.Drawing.Size(121, 21);
            this.protocolComboBox.TabIndex = 16;
            // 
            // timeoutNumbox
            // 
            this.timeoutNumbox.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeoutNumbox.Location = new System.Drawing.Point(24, 194);
            this.timeoutNumbox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.timeoutNumbox.Name = "timeoutNumbox";
            this.timeoutNumbox.Size = new System.Drawing.Size(129, 20);
            this.timeoutNumbox.TabIndex = 18;
            this.timeoutNumbox.ThousandsSeparator = true;
            this.timeoutNumbox.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // LocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(455, 294);
            this.Controls.Add(this.timeoutNumbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.protocolComboBox);
            this.Controls.Add(this.debugCheckbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.locationTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.usernameTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.portTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.servernameTextbox);
            this.Controls.Add(this.ServerNameLabel);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LocationForm";
            this.Text = "Client User Interface";
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ServerNameLabel;
        private System.Windows.Forms.TextBox servernameTextbox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox locationTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox debugCheckbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox protocolComboBox;
        private System.Windows.Forms.NumericUpDown timeoutNumbox;
    }
}