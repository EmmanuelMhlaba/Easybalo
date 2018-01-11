using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Easybalo.Effects {
    public class EffectsManager : MonoBehaviour {
        public GameObject cameraObject;
        private Vector3 _originalCameraPos;
        public Image flashImage;
        public float flashSpeed = 5.5f;
        public Color flashColour;

        private void Start () {
            if (cameraObject != null) {
                _originalCameraPos = cameraObject.transform.localPosition;
            }
        }

        public void ShakeCamera () {
            if (cameraObject != null) {
                StartCoroutine (ShakeCameraObject ());
            }
        }

        private IEnumerator ShakeCameraObject () {
            Vector3 pos = cameraObject.transform.localPosition;
            float distance = 0.3f;
            float shakeSpeed = 35f;
            int tmp = 1;
            for (int i = 0; i < 4; i++) {
                while (!(pos.x >= distance * tmp - 0.05f && pos.x <= distance * tmp + 0.05f)) {
                    pos = cameraObject.transform.localPosition;
                    pos.x = Mathf.Lerp (pos.x, distance * tmp, shakeSpeed * Time.deltaTime);
                    cameraObject.transform.localPosition = pos;
                    yield return null;
                }
                tmp *= -1;
            }
            while (!(pos.x >= _originalCameraPos.x - 0.05 && pos.x <= _originalCameraPos.x + 0.05)) {
                pos = cameraObject.transform.localPosition;
                pos.x = Mathf.Lerp (pos.x, _originalCameraPos.x, shakeSpeed * Time.deltaTime);
                cameraObject.transform.localPosition = pos;
                yield return null;
            }
        }

        public void Flash () {
            StartCoroutine (FlashScreen ());
        }

        private IEnumerator FlashScreen () {
            Color color = flashColour;
            while (!(color.a >= 0.5f - 0.05f && color.a <= 0.5f + 0.05f)) {
                color.a = Mathf.Lerp (color.a, 0.5f, flashSpeed * Time.deltaTime);
                flashImage.color = color;
                yield return null;
            }
            while (!(color.a >= 0 - 0.05 && color.a <= 0 + 0.05)) {
                color.a = Mathf.Lerp (color.a, 0f, (flashSpeed - 1) * Time.deltaTime);
                flashImage.color = color;
                yield return null;
            }
        }
    }
}