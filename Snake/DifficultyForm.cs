using System;
using System.Windows.Forms;

namespace Snake
{
    public partial class DifficultyForm : Form
    {
        private int selectedDifficulty;
        private SoundManager soundManager;

        public DifficultyForm()
        {
            InitializeComponent();
            LoadSettings();
            soundManager = new SoundManager();
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

        private void LoadSettings()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Применить настройки?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                Properties.Settings.Default.SelectedDifficulty = selectedDifficulty;

                Properties.Settings.Default.Save();

                MainForm.SelectedDifficulty = selectedDifficulty;
                Close();
            }
            else
            {
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Сбросить настройки по умолчанию?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                Properties.Settings.Default.SelectedDifficulty = 1;
                Properties.Settings.Default.GameSpeed = 20;
                Properties.Settings.Default.SoundVolume = 1;
                Properties.Settings.Default.MaxScore = 0;
                Properties.Settings.Default.Save();

                LoadSettings();

                MainForm.SelectedDifficulty = 1;
                Close();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                Properties.Settings.Default.SoundVolume = 0;
                Properties.Settings.Default.Save();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                Properties.Settings.Default.SoundVolume = 1;
                Properties.Settings.Default.Save();
            }
        }
    }
}
