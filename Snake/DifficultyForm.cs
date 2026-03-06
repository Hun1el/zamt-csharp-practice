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
    public partial class DifficultyForm : Form
    {
        private int selectedDifficulty;

        public DifficultyForm()
        {
            InitializeComponent();
            LoadSelectedDifficulty();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                selectedDifficulty = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                selectedDifficulty = 2;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                selectedDifficulty = 3;
            }
        }

        private void LoadSelectedDifficulty()
        {
            if (Properties.Settings.Default.SelectedDifficulty > 0)
            {
                selectedDifficulty = Properties.Settings.Default.SelectedDifficulty;
                switch (selectedDifficulty)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        break;
                    case 3:
                        radioButton3.Checked = true;
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SelectedDifficulty = selectedDifficulty;
            Properties.Settings.Default.Save();
            MainForm.SelectedDifficulty = selectedDifficulty;
            this.Close();
        }
    }
}