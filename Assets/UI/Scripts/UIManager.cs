using UnityEngine;

namespace Easybalo.UI {
    public class UIManager : MonoBehaviour {
        public GameObject[] panels;
        public GameObject[] buttons;
        public GameObject[] texts;

        public virtual void DisablePanel (int index) {
            TogglePanel (index, false);
        }

        public virtual void EnablePanel (int index) {
            TogglePanel (index, true);
        }

        public void DisableButton (int index) {
            ToggleGameObject (buttons, index, false);
        }

        public void EnableButton (int index) {
            ToggleGameObject (buttons, index, true);
        }

        public void DisableText (int index) {
            ToggleGameObject (texts, index, false);
        }

        public void EnableText (int index) {
            ToggleGameObject (texts, index, true);
        }

        private void TogglePanel (int index, bool active) {
            if (active) {
                foreach (GameObject go in panels) {
                    go.SetActive (false);
                }
            }
            ToggleGameObject (panels, index, active);
        }

        private void ToggleGameObject (GameObject[] array, int index, bool active) {
            if (index < array.Length) {
                array[index].SetActive (active);
            }
        }
    }
}
