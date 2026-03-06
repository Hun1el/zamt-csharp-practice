using System;
using System.Windows.Forms;

namespace Snake
{
    // Форма 
    public partial class PauseForm : Form
    {
        // Конструктор формы
        public PauseForm()
        {
            InitializeComponent();
        }

        // Обработчик нажатия клавиши SPACE
        private void PauseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
