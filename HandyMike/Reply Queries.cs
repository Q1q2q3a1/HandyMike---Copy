using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandyMike
{
    public partial class Reply_Queries : Form
    {
        public Reply_Queries()
        {
            InitializeComponent();
        }

        private void Reply_Queries_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
        }
    }
}
