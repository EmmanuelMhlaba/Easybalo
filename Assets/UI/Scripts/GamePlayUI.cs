using Easybalo.Gameplay;
using Easybalo.Background;
using UnityEngine;
using UnityEngine.UI;

namespace Easybalo.UI {
    public class GamePlayUI : UIManager {
        public GameManager gameManager;
        public FloatingObjectManager floatingObjectManager;
        private CameraSwitcher _cameraSwitcher;

        private void Start () {
            _cameraSwitcher = GetComponent<CameraSwitcher> ();
        }

        public void StartGame () {
            DisablePanel (0);
            EnableButton (0);
            EnableText (0);
            EnableText (1);
            _cameraSwitcher.ShowMainCamera ();
            if (gameManager != null) {
                gameManager.StartGame ();
            }
        }

        public void PauseGame () {
            EnablePanel (1);
            DisableButton (0);
            DisableText (0);
            DisableText (1);
            _cameraSwitcher.ShowMenuCamera ();
            if (gameManager != null) {
                gameManager.PauseGame ();
            }
        }

        public void ResumeGame () {
            DisablePanel (1);
            EnableButton (0);
            EnableText (0);
            EnableText (1);
            _cameraSwitcher.ShowMainCamera ();
            if (gameManager != null) {
                gameManager.ResumeGame ();
            }
        }

        public void RetryGame () {
            DisablePanel (2);
            EnableButton (0);
            EnableText (0);
            _cameraSwitcher.ShowMainCamera ();
            if (gameManager != null) {
                gameManager.RestartGame ();
            }
        }

        public void ShowGameOver () {
            EnablePanel (2);
            DisableButton (0);
            DisableText (0);
            _cameraSwitcher.ShowMenuCamera ();
        }

        public void SetScoreText (string s) {
            SetText (0, s);
        }

        public void SetTimeText (string s) {
            SetText (1, s);
        }

        private void SetText (int index, string s) {
            if (texts[index] != null) {
                texts[index].GetComponent<Text> ().text = s;
            }
        }

        public override void DisablePanel (int index) {
            base.DisablePanel (index);
            ToggleFloatingObjects (false);
        }

        public override void EnablePanel (int index) {
            base.EnablePanel (index);
            ToggleFloatingObjects (true);
        }

        private void ToggleFloatingObjects (bool active) {
            if (floatingObjectManager != null) {
                if (active) {
                    floatingObjectManager.SpawnObjects ();
                } else {
                    floatingObjectManager.DestroyObjects ();
                }
            }
        }
    }
}
