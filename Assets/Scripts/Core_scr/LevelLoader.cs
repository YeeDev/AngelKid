using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AK.Core
{
    public class LevelLoader : MonoBehaviour
    {
        public event Action OnLevelLoad;

        [SerializeField] float timeToLoad = 1.5f;

        public IEnumerator LoadLevel(bool isDeath)
        {
            if (OnLevelLoad != null) { OnLevelLoad(); }

            yield return new WaitForSeconds(timeToLoad);

            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int sceneToLoad = !isDeath ? (currentScene + 1) % SceneManager.sceneCountInBuildSettings : currentScene;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}