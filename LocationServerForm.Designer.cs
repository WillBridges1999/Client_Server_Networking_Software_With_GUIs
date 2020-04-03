namespace locationserver
{
    partial class LocationServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationServerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.timeoutNumbox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.debugCheckbox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.logfileTextbox = new System.Windows.Forms.TextBox();
            this.databasefileTextbox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumbox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "How long before Timeout (ms)?";
            // 
            // timeoutNumbox
            // 
            this.timeoutNumbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.timeoutNumbox.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeoutNumbox.Location = new System.Drawing.Point(33, 76);
            this.timeoutNumbox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.timeoutNumbox.Name = "timeoutNumbox";
            this.timeoutNumbox.Size = new System.Drawing.Size(129, 23);
            this.timeoutNumbox.TabIndex = 1;
            this.timeoutNumbox.ThousandsSeparator = true;
            this.timeoutNumbox.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(366, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Do you want the debug feature?";
            // 
            // debugCheckbox
            // 
            this.debugCheckbox.AutoSize = true;
            this.debugCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.debugCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.debugCheckbox.ForeColor = System.Drawing.Color.White;
            this.debugCheckbox.Location = new System.Drawing.Point(369, 78);
            this.debugCheckbox.Name = "debugCheckbox";
            this.debugCheckbox.Size = new System.Drawing.Size(102, 21);
            this.debugCheckbox.TabIndex = 3;
            this.debugCheckbox.Text = "Tick for Yes";
            this.debugCheckbox.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(30, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "What is the Filename (path) for Logging?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(366, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "What is the Database Filename (path)?";
            // 
            // logfileTextbox
            // 
            this.logfileTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.logfileTextbox.Location = new System.Drawing.Point(33, 164);
            this.logfileTextbox.Name = "logfileTextbox";
            this.logfileTextbox.Size = new System.Drawing.Size(203, 23);
            this.logfileTextbox.TabIndex = 6;
            // 
            // databasefileTextbox
            // 
            this.databasefileTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.databasefileTextbox.Location = new System.Drawing.Point(369, 164);
            this.databasefileTextbox.Name = "databasefileTextbox";
            this.databasefileTextbox.Size = new System.Drawing.Size(203, 23);
            this.databasefileTextbox.TabIndex = 7;
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Gray;
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(477, 317);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(122, 72);
            this.submitButton.TabIndex = 8;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // LocationServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(638, 423);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.databasefileTextbox);
            this.Controls.Add(this.logfileTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.debugCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeoutNumbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LocationServerForm";
            this.Text = "Server Operations Form";
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timeoutNumbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox debugCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox logfileTextbox;
        private System.Windows.Forms.TextBox databasefileTextbox;
        private System.Windows.Forms.Button submitButton;
    }
}