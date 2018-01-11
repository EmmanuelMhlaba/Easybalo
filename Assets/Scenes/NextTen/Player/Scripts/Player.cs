using UnityEngine;
using UnityEngine.UI;

namespace Easybalo.NextTen.Player {
    public class Player : MonoBehaviour {
        private Movement _movement;
        public GameObject playerPrefab;
        private Text text;

        private void Awake () {
            InstantiatePlayerPrefab ();
            text = GetComponentInChildren<Text> ();
        }

        private void Start () {
            _movement = GetComponent<Movement> ();
        }

        private void InstantiatePlayerPrefab () {
            if (playerPrefab != null) {
                GameObject tmp = Instantiate (playerPrefab);
                tmp.transform.parent = transform;
                tmp.transform.localPosition = Vector3.zero;
                tmp.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
            }
        }

        public void SetNumber (int n) {
            if (text != null) {
                text.text = n.ToString ();
            }
        }

        public void ResetPosition () {
            if (_movement != null) {
                _movement.ResetPlayerMovement ();
            }
        }

        public void IncreaseSpeed (float n) {
            if (_movement != null) {
                _movement.IncreaseSpeed (n);
            }
        }
    }
}
