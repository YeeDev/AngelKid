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

        public IEnumerator LoadLevel()
        {
            if (OnLevelLoad != null) { OnLevelLoad(); }

            yield return new WaitForSeconds(timeToLoad);

            int sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % (SceneManager.sceneCount + 1);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}