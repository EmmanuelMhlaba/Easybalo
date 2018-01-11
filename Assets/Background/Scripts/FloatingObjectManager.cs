using UnityEngine;

namespace Easybalo.Background {
    public class FloatingObjectManager : MonoBehaviour {
        public bool spawnOnStart = true;
        public GameObject prefab;
        private GameObject[] objects;
        public int maxObjects = 0;
        public float maxSpeed = 0f;
        public Vector3 minBounds = Vector3.zero;
        public Vector3 maxBounds = Vector3.zero;
        public Material[] materials;

        private void Start () {
            if (spawnOnStart) {
                SpawnObjects ();
            }
        }

        public void SpawnObjects () {
            if (prefab != null) {
                objects = new GameObject[maxObjects];
                for (int i = 0; i < maxObjects; i++) {
                    objects[i] = Instantiate (prefab);
                    objects[i].GetComponent<MeshRenderer> ().material = materials[Random.Range (0, materials.Length)];
                    objects[i].transform.parent = transform;
                    FloatingObject fo = objects[i].AddComponent<FloatingObject> ();
                    fo.minBounds = minBounds;
                    fo.maxBounds = maxBounds;
                    fo.movementSpeed = Random.Range (0, maxSpeed);
                }
            }
        }

        public void DestroyObjects () {
            for (int i = 0; i < objects.Length; i++) {
                Destroy (objects[i]);
                objects[i] = null;
            }
        }
    }
}
