using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

namespace Match3
{
    public class LevelSelect : MonoBehaviour
    {
        [System.Serializable]
        public struct ButtonPlayerPrefs
        {
            public GameObject gameObject;
            public string playerPrefKey;
        };

        public ButtonPlayerPrefs[] buttons;
        public GameObject dialogPanel; // Reference to the dialog panel
        public TextMeshProUGUI dialogText;        // Reference to the text component on the dialog
        public AudioSource Master;     // Reference to an AudioSource for playing clips
        public AudioClip confirmationClip; // Audio clip to play on button press
        public Button yesButton; // Reference to the Yes button
        public Button noButton; // Reference to the No button
        public AudioMixer mixer; // Reference to the Audio Mixer
        public Slider musicSlider; // Reference to the music volume slider

        private const string MIXER_MUSIC = "MusicVolume";
        private string selectedLevelName;

        private void Start()
        {
            dialogPanel.SetActive(false); // Make sure the dialog panel is hidden on start
            if (musicSlider != null)
            {
                musicSlider.onValueChanged.AddListener(SetMusicVolume);
            }
            else
            {
                Debug.LogError("Music Slider is not assigned on " + gameObject.name);
            }
        }

        public void OnButtonPress(string levelName)
        {
            selectedLevelName = levelName;
            Debug.Log("Selected level to load: " + selectedLevelName);
            dialogText.text = $"Are you sure you want to go to {levelName}?";
            dialogPanel.SetActive(true);
            Master.PlayOneShot(confirmationClip);
        }

        public void OnYesButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(selectedLevelName);
        }

        public void OnNoButton()
        {
            dialogPanel.SetActive(false);
        }

        private void SetMusicVolume(float value)
        {
            if (mixer != null)
            {
                mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
            }
            else
            {
                Debug.LogError("Audio Mixer is not assigned on " + gameObject.name);
            }
        }
    }
}
