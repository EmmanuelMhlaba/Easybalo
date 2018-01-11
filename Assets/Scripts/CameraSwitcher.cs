using UnityEngine;

namespace Easybalo.Gameplay {
    public class CameraSwitcher : MonoBehaviour {
        public GameObject[] cameraObjects;

        public void ShowMenuCamera () {
            ToggleGameObject (0, false);
            ToggleGameObject (1, true);
        }

        public void ShowMainCamera () {
            ToggleGameObject (1, false);
            ToggleGameObject (0, true);
        }

        private void ToggleGameObject (int index, bool active) {
            if (index < cameraObjects.Length) {
                cameraObjects[index].SetActive (active);
            }
        }
    }
}
