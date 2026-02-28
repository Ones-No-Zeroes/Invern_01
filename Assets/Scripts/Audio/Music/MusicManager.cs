using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour, IVolumeAdjustable
{
    // Enum containing the position in the array for the Lightmode and Darkmode Background Musics.
    private enum BackgroundMusics 
    {
        LightModeBackgroundMusic = 0,
        DarkModeBackgroundMusic = 1
    }

    // Private Fields
    private float[] timeStamps = new float[2] { 0f, 0f }; // Array containing the timeStamps of the two BGMs.
    [SerializeField] private AudioClip[] backgroundMusics = new AudioClip[2]; // Array containing the two Background musics.
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusics[(int)BackgroundMusics.LightModeBackgroundMusic];
        audioSource.Play();
    }

    /// <summary>
    /// Method used to Switch the Music and play said Music between two clips. Clip 1 being Lightmode and clip 2 being Darkmode.
    /// </summary>
    public void SwitchMusic()
    {   
        // Is the clip being played the Lightmode BGM? If so, switch BGM to Darkmode.
        if(audioSource.clip.name == backgroundMusics[(int)BackgroundMusics.LightModeBackgroundMusic].name)
        {
            AssignCurrentTimeStampAndNewClip(backgroundMusics[(int)BackgroundMusics.DarkModeBackgroundMusic], BackgroundMusics.LightModeBackgroundMusic);
            audioSource.Play();

        }
        else // Lightmode BGM is not playing, therefore it is the Darkmode BGM, meaning it should switch to Lightmode
        {
            AssignCurrentTimeStampAndNewClip(backgroundMusics[(int)BackgroundMusics.LightModeBackgroundMusic], BackgroundMusics.DarkModeBackgroundMusic);
            audioSource.Play();

        }
    }

    /// <summary>
    /// Assigns a new BGM that must be played. Takes the currently playing BGM and takes its time stamp at the current moment.
    /// </summary>
    /// <param name="newClipToPlay"></param>
    /// <param name="currentMusicPlaying"></param>
    private void AssignCurrentTimeStampAndNewClip(AudioClip newClipToPlay, BackgroundMusics currentMusicPlaying)
    {
        timeStamps[(int)currentMusicPlaying] = audioSource.time;
        audioSource.clip = newClipToPlay;
        audioSource.time = timeStamps[(int)currentMusicPlaying];
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
