using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortMatrix
{
    public partial class InputForm : Form
    {

        public int rows = 2;
        public int columns = 2;
        public int min = 0;
        public int max = 0;

        public InputForm()
        {
            InitializeComponent();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            rows = (int)nudRows.Value;
            columns = (int)nudRows.Value;
            min = (int)nudMin.Value;
            max = (int)nudMax.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
