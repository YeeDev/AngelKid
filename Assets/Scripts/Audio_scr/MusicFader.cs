using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Core;

namespace AK.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicFader : MonoBehaviour
    {
        [SerializeField] float fadeTime = 1f;

        AudioSource audioSource = null;
        LevelLoader levelLoader;

        float maxVolume;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            levelLoader = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelLoader>();

            InitializeMusic();
        }

        private void InitializeMusic()
        {
            maxVolume = audioSource.volume;
            audioSource.volume = 0;
            StartCoroutine(FadeMusic(true));
        }

        private void OnEnable() { levelLoader.OnLevelLoad += FadeOut; }
        private void OnDisable() { levelLoader.OnLevelLoad -= FadeOut; }

        private void FadeOut() { StartCoroutine(FadeMusic(false)); }

        private IEnumerator FadeMusic(bool fadeIn)
        {
            float timer = 0;

            float startingVolume = audioSource.volume;
            float targetVolume = fadeIn ? maxVolume : 0;

            while (timer < fadeTime)
            {
                yield return new WaitForEndOfFrame();

                audioSource.volume = Mathf.Lerp(startingVolume, targetVolume, timer);
                timer += Time.deltaTime;
            }

            audioSource.volume = targetVolume;
        }
    }
}