using UnityEngine;
using System.Collections.Generic;

namespace Easybalo.NextTen.Math {
    public class Math : MonoBehaviour {
        public int CurrentNumber { get; private set; }
        public int CorrectAnswer { get; private set; }
        public List<int> PossibleAnswers { get; private set; }

        private void Awake () {
            PossibleAnswers = new List<int> ();
        }

        // 'max' must be a multiple of 10
        public void NewQuestion (int max) {
            Clear ();
            CurrentNumber = Random.Range (0, max);
            for (int i = 1; i <= max / 10; i++) {
                if ((CurrentNumber / 10) + 1 == i) {
                    CorrectAnswer = i * 10;
                }
                PossibleAnswers.Add (i * 10);
            }
        }

        private void Clear () {
            PossibleAnswers.Clear ();
            CorrectAnswer = -1;
        }
    }
}
