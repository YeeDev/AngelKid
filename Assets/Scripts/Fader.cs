using UnityEngine;
using AK.Core;

namespace AK.UI
{
    [RequireComponent(typeof(Animator))]
    public class Fader : MonoBehaviour
    {
        [SerializeField] string fadeOutParameter = "FadeOut";

        Animator animator;
        LevelLoader levelLoader;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            levelLoader = GameObject.FindWithTag("GameController").GetComponent<LevelLoader>();
        }

        private void OnEnable() { levelLoader.OnLevelLoad += PlayFadeOut; }
        private void OnDisable() { levelLoader.OnLevelLoad -= PlayFadeOut; }

        private void PlayFadeOut() { animator.SetTrigger(fadeOutParameter); }
    }
}