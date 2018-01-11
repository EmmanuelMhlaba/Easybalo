using UnityEngine;

namespace Easybalo.NextTen.Gameplay {
    public class GameManager : Easybalo.Gameplay.GameManager {
        public Effects.EffectsManager effectsManager;
        public Tiles.TileManager tileManager;
        public UI.GamePlayUI playUI;
        public Math.Math math;
        public Player.Player player;
        private int _initialTiles = 10;
        private int _maxNumber = 100;
        private int _playtime = 0;

        private void Update () {
            if (Playing) {
                playUI.SetScoreText (score.ToString ());
                if (score < 0) {
                    StopGame ();
                }
                player.IncreaseSpeed (Time.deltaTime * 0.05f);
            }
        }

        public override void PauseGame () {
            playing = false;
        }

        public override void RestartGame () {
            player.ResetPosition ();
            score = 0;
            _playtime = 0;
            playing = true;
            tileManager.ResetTiles ();
            tileManager.SpawnTiles (_initialTiles);
        }

        public override void ResumeGame () {
            playing = true;
        }

        public override void StartGame () {
            if (tileManager != null && math != null && player != null) {
                NewQuestion ();
                tileManager.SpawnTiles (_initialTiles);
                playing = true;
                InvokeRepeating ("UpdatePlayTime", 0, 1f);
            }
        }

        private void UpdatePlayTime () {
            if (Playing) {
                _playtime += 1;
                playUI.SetTimeText ((_playtime / 60).ToString () + ":" + (_playtime % 60).ToString ());
            }
        }

        private void NewQuestion () {
            math.NewQuestion (_maxNumber);
            player.SetNumber (math.CurrentNumber);
        }

        public override void StopGame () {
            playing = false;
            playUI.ShowGameOver ();
        }

        public void CorrectAnswer () {
            score += 1;
            NewQuestion ();
        }

        public void IncorrectAnswer () {
            score -= 1 + score / 10;
            effectsManager.ShakeCamera ();
            effectsManager.Flash ();
            NewQuestion ();
        }
    }
}
