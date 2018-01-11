using UnityEngine;
using Easybalo.NextTen.Input;

namespace Easybalo.NextTen.Player {
    public class Movement : MonoBehaviour {
        public ScreenControls screenControls;
        public Gameplay.GameManager gameManager;
        private const float FORWARD_SPEED = 6f;
        private float forwardSpeed = FORWARD_SPEED;
        private float sideWaySpeed = 10f;
        private float currentPos;
        private float nextPos;
        private float posRange = 0.1f;
        private const float CENTRE_POS = 0f;
        private const float LEFT_POS = -1f;
        private const float RIGHT_POS = 1f;
        private bool check = true;

        private void Start () {
            currentPos = CENTRE_POS;
            nextPos = CENTRE_POS;
        }

        public void Update () {
            if (check) {
                if (screenControls.RightZonesActive) {
                    if (currentPos <= CENTRE_POS + posRange && currentPos >= CENTRE_POS - posRange) {
                        nextPos = RIGHT_POS;
                    } else if (currentPos <= LEFT_POS + posRange && currentPos >= LEFT_POS - posRange) {
                        nextPos = CENTRE_POS;
                    }
                } else if (screenControls.LeftZonesActive) {
                    if (currentPos <= CENTRE_POS + posRange && currentPos >= CENTRE_POS - posRange) {
                        nextPos = LEFT_POS;
                    } else if (currentPos <= RIGHT_POS + posRange && currentPos >= RIGHT_POS - posRange) {
                        nextPos = CENTRE_POS;
                    }
                }
                check = false;
            }
        }

        private void LateUpdate () {
            if (gameManager.Playing) {
                Vector3 tmp = transform.position;
                currentPos = Mathf.Lerp (currentPos, nextPos, sideWaySpeed * Time.deltaTime);
                tmp.x = currentPos;
                transform.position = tmp;
                transform.Translate (new Vector3 (0, 0, forwardSpeed * Time.deltaTime));
                if (currentPos <= nextPos + posRange && currentPos >= nextPos - posRange) {
                    check = true;
                }
            }
        }

        public void ResetPlayerMovement () {
            transform.position = new Vector3 (0, 1f, 0);
            forwardSpeed = FORWARD_SPEED;
        }

        public void IncreaseSpeed (float n) {
            if (forwardSpeed + n < 20) {
                forwardSpeed += n;
            }
        }
    }
}