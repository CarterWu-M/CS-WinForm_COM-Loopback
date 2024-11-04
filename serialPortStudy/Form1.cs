using System;
using System.Drawing;
using System.Windows.Forms;
using System.Management;
using serialPortStudy.Utilities;
using System.Text.RegularExpressions;

namespace serialPortStudy
{
    public partial class Form1 : Form
    {
        private ComPort _comPort;
        public Form1()
        {
            InitializeComponent();
            searchComPortNames();
        }
        private void searchComPortNames()
        {
            cbComPorts.Items.Clear();

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    string szName = obj["Name"]?.ToString();
                    if (szName != null)
                    {
                        cbComPorts.Items.Add(szName);
                    }
                }
                if (cbComPorts.Items.Count > 0)
                {
                    cbComPorts.SelectedIndex = 0;
                }
                else
                {
                    cbComPorts.Text = "";
                }
            }
        }

        private string getComPortName(string szDeviceName)
        {
            string szPattern = @"\(COM\d+\)";
            Match match = Regex.Match(szDeviceName, szPattern);
            if (match.Success)
            {
                return match.Value.Trim('(', ')');
            }
            else
            {
                return "";
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.searchComPortNames();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            string szComPortName = getComPortName(cbComPorts.SelectedItem?.ToString());
            this._comPort = new ComPort(szComPortName, 115200);
            if (this._comPort.Open())
            {
                btnOpen.BackColor = Color.LightGray;
                btnClose.BackColor = Color.LightGreen;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this._comPort.Close();

            btnOpen.BackColor = Color.Thistle;
            btnClose.BackColor = Color.LightGray;
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            txtRead.Text = this._comPort?.readStr();
        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            this._comPort?.writeStr(txtWrite.Text);
        }


    }
}
