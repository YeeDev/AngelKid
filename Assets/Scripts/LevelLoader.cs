using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AK.Core
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] float timeToLoad = 1f;

        bool alreadyLoading;

        public IEnumerator LoadLevel()
        {
            if (alreadyLoading) { yield break; }

            alreadyLoading = true;

            yield return new WaitForSeconds(timeToLoad);

            int sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1) % (SceneManager.sceneCount + 1);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}