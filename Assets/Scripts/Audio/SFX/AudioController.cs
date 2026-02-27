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



    // Public Objects
    public volumeSettingsPackage volSettingsPackage = new volumeSettingsPackage();

    void Update()
    {  
        playerSFXManager.SetVolume(sfxVolumeSlider.value);
        musicManager.SetVolume(musicVolumeSlider.value);
        
        // Assignment of values in the volSettingsPackage
        volSettingsPackage.sfxVolumeAmount = sfxVolumeSlider.value;
        volSettingsPackage.musicVolumeAmount = musicVolumeSlider.value;
    }
}

public struct volumeSettingsPackage
{
    public float sfxVolumeAmount;
    public float musicVolumeAmount;
}
