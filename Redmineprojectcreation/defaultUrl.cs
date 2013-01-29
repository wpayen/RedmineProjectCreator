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
    public partial class defaultUrl : Form
    {
        public Parser parser;
        public defaultUrl()
        {
            InitializeComponent();
        }

        private void apiKeyOk_Click(object sender, EventArgs e)
        {
            parser.changeDefaultUrl(this.apiKeyTextbox.Text);
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
