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
    // Главная форма
    public partial class MainForm : Form
    {
        // Свойство для хранения уровня сложности (по умолчанию 1)
        public static int SelectedDifficulty { get; set; } = 1;

        // Конструктор
        public MainForm()
        {
            InitializeComponent();
            LoadSelectedDifficulty(); // Загрузка уровня сложности из настроек
        }

        // Метод для загрузки уровня сложности из настроек
        public static void LoadSelectedDifficulty()
        {
            // Проверка если значение в настройках больше 0
            if (Properties.Settings.Default.SelectedDifficulty > 0)
            {
                SelectedDifficulty = Properties.Settings.Default.SelectedDifficulty;
            }
        }

        // Кнопка начать игру
        private void button1_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(SelectedDifficulty);

            // Скрываем текущую форму
            this.Visible = false;

            // Показываем форму
            gameForm.ShowDialog();

            // После завершения делаем видимой
            this.Visible = true;
        }

        // Кнопка выход
        private void button2_Click(object sender, EventArgs e)
        {
            // Окно подтверждения
            if (DialogResult.Yes == MessageBox.Show("Вы точно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                Close();
            }
        }

        // Кнопка обучения
        private void button3_Click(object sender, EventArgs e)
        {
            InstructionsForm instructionForm = new InstructionsForm();
            this.Visible = false;
            instructionForm.ShowDialog();
            this.Visible = true;
        }

        // Кнопка настройки
        private void button4_Click(object sender, EventArgs e)
        {
            DifficultyForm difficultyForm = new DifficultyForm();
            this.Visible = false;
            difficultyForm.ShowDialog();
            this.Visible = true;
        }
    }
}
