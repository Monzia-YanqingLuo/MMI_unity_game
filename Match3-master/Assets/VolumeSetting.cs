using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Match3
{
    public class NewBehaviourScript : MonoBehaviour
    {
        [SerializeField] AudioMixer mixer;
        [SerializeField] Slider musicSlider;

        const string MIXER_MUSIC = "MusicVolume";

        void Awake()
        {
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        void SetMusicVolume(float value)
        {
            mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value)*20);
        }
    }
}
