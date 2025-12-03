using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private const string VolumeKey = "MasterVolume";

    void Start()
    {
        // Lade gespeicherten Wert oder Standard 1.0f
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        slider.value = savedVolume;
        SetVolume(savedVolume);
        
        // Auf Slider-Änderungen hören
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Slider (0–1) -> dB
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("MasterVolume", dB);

        // Speichern
        PlayerPrefs.SetFloat(VolumeKey, value);
    }
    void Awake()
{
    DontDestroyOnLoad(gameObject);
}

}
