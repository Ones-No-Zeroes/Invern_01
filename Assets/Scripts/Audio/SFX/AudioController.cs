using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("General Audio Controller Attributes")]
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [Header("SFX Attributes")]
    [SerializeField] PlayerSFXManager playerSFXManager;

    [Header("Music Attributes")]
    [SerializeField] MusicManager musicManager;


    void Update()
    {
        playerSFXManager.SetVolume(sfxVolumeSlider.value);
        musicManager.SetVolume(musicVolumeSlider.value);
    }
}
