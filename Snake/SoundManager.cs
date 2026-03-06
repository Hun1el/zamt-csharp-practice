using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public class SoundManager
    {
        private SoundPlayer effectPlayer;
        private bool isSoundOn;

        public SoundManager()
        {
            effectPlayer = new SoundPlayer();
            isSoundOn = Properties.Settings.Default.SoundVolume == 1;
        }

        public void PlayEatSound()
        {
            if (isSoundOn)
                Task.Run(() => PlayEffect("Resources/eat.wav"));
        }

        public void PlayGameOverSound()
        {
            if (isSoundOn)
                Task.Run(() => PlayEffect("Resources/gameover.wav"));
        }

        private void PlayEffect(string soundPath)
        {
            try
            {
                using (var player = new SoundPlayer(soundPath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка звука: {ex.Message}");
            }
        }

        public void SetSoundState(bool state)
        {
            isSoundOn = state;
            Properties.Settings.Default.SoundVolume = state ? 1 : 0;
            Properties.Settings.Default.Save();
        }
    }
}
