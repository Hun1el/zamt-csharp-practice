using System;
using System.Windows.Forms;

namespace Snake
{
    public partial class PauseForm : Form
    {
        public PauseForm()
        {
            InitializeComponent();
        }

        private void PauseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
