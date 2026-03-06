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
    public partial class MainForm : Form
    {
        public static int SelectedDifficulty { get; set; } = 1;

        public MainForm()
        {
            InitializeComponent();
            LoadSelectedDifficulty();
        }

        public static void LoadSelectedDifficulty()
        {
            if (Properties.Settings.Default.SelectedDifficulty > 0)
            {
                SelectedDifficulty = Properties.Settings.Default.SelectedDifficulty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int difficulty = SelectedDifficulty;
            GameForm gameForm = new GameForm(SelectedDifficulty);
            this.Visible = false;
            gameForm.ShowDialog();
            gameForm.Dispose();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InstructionsForm instructionForm = new InstructionsForm();
            this.Visible = false;
            instructionForm.ShowDialog();
            this.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DifficultyForm difficultyForm = new DifficultyForm();
            this.Visible = false;
            difficultyForm.ShowDialog();
            this.Visible = true;
        }
    }
}
