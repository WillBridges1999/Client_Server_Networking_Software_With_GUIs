using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace location
{
    public partial class LocationForm : Form
    {
        public string m_serverName = "whois.net.dcs.hull.ac.uk"; // Default serverName.
        public string m_userName;
        public string m_location;
        public string m_protocol = "whois"; // Default protocol.
        public int m_port = 43; // Default port number.
        public int m_timeout = 1000; // Default timeout.
        public bool m_debug = false; // Default as it's optional.
        public LocationForm()
        {
            InitializeComponent();
            MessageBox.Show("If you leave any UI options/ fields empty, the default values will be used!");
        }

        private void serverNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //string input = serverNameTextBox.Text;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            /// Once the submit button is clicked, save all the current UI input values.
            m_serverName = servernameTextbox.Text;
            m_userName = usernameTextbox.Text;
            m_location = locationTextbox.Text;

            if (protocolComboBox.SelectedItem != null)
            {
                m_protocol = protocolComboBox.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("No protocol was set. Thus, setting the protocol to the default: whois.");
                m_protocol = "whois";
            }

            if (portTextbox.Text == null)
            {
                m_port = 43;
            }
            else
            {
                try
                {
                    m_port = Convert.ToInt32(portTextbox.Text);
                }
                catch
                {
                    MessageBox.Show("Your port number was invalid. Setting port number to default port: 43.");
                    m_port = 43;
                }
            }

            m_timeout = (int)timeoutNumbox.Value;

            if (debugCheckbox.Checked == true)
            {
                m_debug = true;
            }

            /// After all values are saved, close the form.
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
