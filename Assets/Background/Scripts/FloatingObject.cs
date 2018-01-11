using UnityEngine;
using System.Collections;

namespace Easybalo.Background {
    public class FloatingObject : MonoBehaviour {
        private bool _movingToPosition = false;
        public float movementSpeed = 5f;

        public Vector3 minBounds = Vector3.zero;
        public Vector3 maxBounds = Vector3.zero;

        private Vector3 rotationAxis;
        private float rotationSpeed;

        private void Start () {
            rotationSpeed = Random.Range (0, 5f);
            rotationAxis = new Vector3 (Random.Range (0, 2), Random.Range (0, 2), Random.Range (0, 2));
        }

        private void Update () {
            if (!_movingToPosition) {
                _movingToPosition = true;
                StartCoroutine (MoveToPosition (NewPosition ()));
            }
            transform.Rotate (rotationAxis, rotationSpeed);
        }

        private IEnumerator MoveToPosition (Vector3 newPos) {
            Vector3 currentPos = transform.localPosition;
            while (!CompareVectors (currentPos, newPos)) {
                currentPos.x = Mathf.Lerp (currentPos.x, newPos.x, movementSpeed * Time.deltaTime);
                currentPos.y = Mathf.Lerp (currentPos.y, newPos.y, movementSpeed * Time.deltaTime);
                currentPos.z = Mathf.Lerp (currentPos.z, newPos.z, movementSpeed * Time.deltaTime);
                transform.localPosition = currentPos;
                yield return null;
            }
            _movingToPosition = false;
        }

        private bool CompareVectors (Vector3 a, Vector3 b) {
            if (CompareFloats (a.x, b.x) && CompareFloats (a.y, b.y) && CompareFloats (a.z, b.z)) {
                return true;
            }
            return false;
        }

        private bool CompareFloats (float a, float b) {
            float range = 0.2f;
            if (a >= b - range && a <= b + range) {
                return true;
            }
            return false;
        }

        private Vector3 NewPosition () {
            return new Vector3 (
                Random.Range (minBounds.x, maxBounds.x),
                Random.Range (minBounds.y, maxBounds.y),
                Random.Range (minBounds.z, maxBounds.z)
            );
        }
    }
}