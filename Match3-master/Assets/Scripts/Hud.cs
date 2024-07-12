using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Match3
{
    public class Hud : MonoBehaviour
    {
        public Level level;
        public GameOver gameOver;

        public Text remainingText;
        public Text remainingSubText;
        public Text targetText;
        public Text targetSubtext;
        public Text scoreText;
        public Image[] stars;
        public AudioSource audioSource;  // This will be your centralized audio controller within the HUD
        public AudioMixer mixer; // Reference to the Audio Mixer
        public Slider volumeSlider; // Reference to the music volume slider

        private const string MIXER_MUSIC = "MusicVolume";




        private int _starIndex = 0;

        private void Start()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].enabled = i == _starIndex;
            }
            if (volumeSlider != null)
            {
                volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // 假设0.5是默认值
                volumeSlider.onValueChanged.AddListener(SetMusicVolume);
            }
            else
            {
                Debug.LogError("Volume Slider is not assigned in the Inspector");
            }
        }

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();

            int visibleStar = 0;

            if (score >= level.score1Star && score < level.score2Star)
            {
                visibleStar = 1;
            }
            else if (score >= level.score2Star && score < level.score3Star)
            {
                visibleStar = 2;
            }
            else if (score >= level.score3Star)
            {
                visibleStar = 3;
            }

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].enabled = (i == visibleStar);
            }

            _starIndex = visibleStar;
        }

        public void SetTarget(int target) => targetText.text = target.ToString();

        public void SetRemaining(int remaining) => remainingText.text = remaining.ToString();

        public void SetRemaining(string remaining) => remainingText.text = remaining;

        public void SetLevelType(LevelType type)
        {
            switch (type)
            {
                case LevelType.Moves:
                    remainingSubText.text = "moves remaining";
                    targetSubtext.text = "target score";
                    break;
                case LevelType.Obstacle:
                    remainingSubText.text = "moves remaining";
                    targetSubtext.text = "bubbles remaining";
                    break;
                case LevelType.Timer:
                    remainingSubText.text = "time remaining";
                    targetSubtext.text = "target score";
                    break;
            }
        }

        public void OnGameWin(int score)
        {
            gameOver.ShowWin(score, _starIndex);
            if (_starIndex > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, _starIndex);
            }
        }

        public void OnGameLose() => gameOver.ShowLose();

        public void SetMusicVolume(float volume)
        {
            if (mixer != null)
            {
                mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
            }
            else
            {
                Debug.LogError("Audio Mixer is not assigned on " + gameObject.name);
            }

        }
    }
}