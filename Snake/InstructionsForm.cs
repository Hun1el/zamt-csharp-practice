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
    // Форма
    public partial class InstructionsForm : Form
    {
        // Конструктор формы
        public InstructionsForm()
        {
            InitializeComponent();
        }

        // Кнопка которая перекидывает обратно на главную форму
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
