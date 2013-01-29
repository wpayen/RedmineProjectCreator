using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redmineprojectcreation
{
    public partial class Apikey : Form
    {
        public Parser parser;
        public Apikey()
        {
            InitializeComponent();
        }

        private void apiKeyOk_Click(object sender, EventArgs e)
        {
            parser.changeApiKey(this.apiKeyTextbox.Text);
            this.Hide();
        }
        
        public void setParser(Parser parser)
        {
            this.parser = parser;
        }

        private void apiKeyOff_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
