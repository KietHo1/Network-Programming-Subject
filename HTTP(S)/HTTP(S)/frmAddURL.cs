using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTP_S_
{
    public partial class frmAddURL : Form
    {
        public frmAddURL()
        {
            InitializeComponent();
        }

        public string Url { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Url = txtURL.Text;
        }
    }
}
