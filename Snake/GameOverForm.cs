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
    // Форма проигрыша
    public partial class GameOverForm : Form
    {
        // Конструктор формы
        public GameOverForm()
        {
            InitializeComponent();
        }

        // Обработка нажатий клавиш
        private void GameOverForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R) // Если нажать R
            {
                this.DialogResult = DialogResult.Retry;
                Close();
            }
            else if (e.KeyCode == Keys.Escape) // Если нажать ESC
            {
                this.DialogResult = DialogResult.Abort;
                Close();
            }
        }
    }
}
