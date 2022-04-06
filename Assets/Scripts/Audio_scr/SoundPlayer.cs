using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource jumpSource = null;
    [SerializeField] AudioSource shotSource = null;

    public void PlayJumpClip() { jumpSource.Play(); }
    public void PlayShootClip() { shotSource.Play(); }
}
