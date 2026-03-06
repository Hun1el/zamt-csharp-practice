using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class GameOverForm : Form
    {
        public GameOverForm()
        {
            InitializeComponent();
        }

        private void GameOverForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                this.DialogResult = DialogResult.Retry;
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Abort;
                Close();
            }
        }
    }
}
