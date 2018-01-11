using UnityEngine;

namespace Easybalo.Scenes {
    public class SceneManager : MonoBehaviour {
        public void LoadMainMenu () {
            UnityEngine.SceneManagement.SceneManager.LoadScene (0);
        }

        public void LoadNextTen () {
            UnityEngine.SceneManagement.SceneManager.LoadScene (1);
        }

        public void LoadAddition () {
            UnityEngine.SceneManagement.SceneManager.LoadScene (2);
        }

        public void LoadBonds () {
            UnityEngine.SceneManagement.SceneManager.LoadScene (3);
        }
    }
}
