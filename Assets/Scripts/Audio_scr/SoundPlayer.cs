using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource jumpSource = null;
    [SerializeField] AudioSource shotSource = null;

    public void PlayJumpClip()
    {
        jumpSource.pitch = Random.Range(1.05f, 1.15f);
        jumpSource.Play();
    }

    public void PlayShootClip() { shotSource.Play(); }
}
