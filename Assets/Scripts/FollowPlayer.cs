using UnityEngine;

namespace Easybalo.Gameplay {
    public class FollowPlayer : MonoBehaviour {
        public GameObject player;
        public bool followPlayer;
        public bool followOnZ;
        private Vector3 _startPos;

        private void Start () {
            _startPos = transform.position;
        }

        private void Update () {
            if (followPlayer) {
                if (followOnZ) {
                    FollowOnZ ();
                }
            }
        }

        private void FollowOnZ () {
            Vector3 tmp = transform.position;
            tmp.z = _startPos.z + player.transform.position.z;
            transform.position = tmp;
        }
    }
}
