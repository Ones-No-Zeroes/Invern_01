using UnityEngine;

public class PlayFadeAnimation : MonoBehaviour
{
    /// <summary>
    /// Plays a Fade-out and Fade-in of a black background
    /// </summary>
    public void PlayDeathFade()
    {
        gameObject.GetComponent<Animation>().Play();
    }
}
