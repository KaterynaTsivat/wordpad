using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notepad
{
    public partial class FontSettings : Form
    {
        public FontSettings()
        {
            InitializeComponent();
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            name_example.Font = new Font(name_example.Font.FontFamily, int.Parse(comboBox1.SelectedItem.ToString()));

        }

    }
}
