using UnityEngine;

namespace Easybalo.Gameplay {
    public abstract class GameManager : MonoBehaviour {
        protected bool playing;
        protected int score;

        public bool Playing {
            get {
                return playing;
            }
        }

        public abstract void StartGame ();
        public abstract void PauseGame ();
        public abstract void ResumeGame ();
        public abstract void RestartGame ();
        public abstract void StopGame ();
    }
}
