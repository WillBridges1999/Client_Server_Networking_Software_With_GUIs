using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace locationserver
{
    public partial class LocationServerForm : Form
    {
        public string m_logFilename;
        public int m_timeout;
        public bool m_debug;
        public string m_databaseFilename;
        public LocationServerForm(string logFilename, int timeout, bool debug, string databaseFilename)
        {
            InitializeComponent();

            /// As the ACW specifies, pre-setting the values in the 
            /// input boxes, from the params sent by the server.
            logfileTextbox.Text = logFilename;
            timeoutNumbox.Value = timeout;
            debugCheckbox.Checked = debug;
            databasefileTextbox.Text = databaseFilename;

            /// Now, just assigning the input param values to this Form's variables.
            m_logFilename = logFilename;
            m_timeout = timeout;
            m_debug = debug;
            m_databaseFilename = databaseFilename;

            MessageBox.Show("Some input boxes have been populated with pre-set values from your intial arguments. Click 'OK' to continue.");
        }

        public LocationServerForm()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            /// Once the submit button is clicked, save all the current input values.
            m_logFilename = logfileTextbox.Text;
            m_timeout = (int)timeoutNumbox.Value;
            m_databaseFilename = databasefileTextbox.Text;

            if (debugCheckbox.Checked == true)
            {
                m_debug = true;
            }

            /// After all values are saved, close the form.
            Application.Exit();
        }
    }
}
