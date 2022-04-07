using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource jumpSource = null;
    [SerializeField] AudioSource shotSource = null;
    [SerializeField] float deathClipPitch = 0.3f;
    [SerializeField] AudioClip deathClip = null; 

    public void PlayJumpClip()
    {
        jumpSource.pitch = Random.Range(1.05f, 1.15f);
        jumpSource.Play();
    }

    public void PlayShootClip() { shotSource.Play(); }

    public void PlayDeathClip()
    {
        shotSource.Stop();
        jumpSource.pitch = deathClipPitch;
        jumpSource.PlayOneShot(deathClip);
    }
}
