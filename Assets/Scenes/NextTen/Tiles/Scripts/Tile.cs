using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Easybalo.NextTen.Tiles {
    public class Tile : MonoBehaviour {
        public Ground groundPrefab;
        private Ground[] _groundObjects = new Ground[2];
        public Obstacle obstaclePrefab;
        private List<Obstacle> _obstacleObjects = new List<Obstacle> ();
        private const int MAX_OBSTACLES = 3;
        public Material[] materials;
        private const int DEFAULT_MAT = 0;
        private const int CORRECT_MAT = 1;
        private const int INCORRECT_MAT = 2;
        [HideInInspector] public TileManager tileManager;

        private void Awake () {
            SpawnGround ();
            SpawnObstacles ();
        }

        private void Update () {
            UpdateAnswerWall ();
        }

        private void SpawnGround () {
            if (groundPrefab != null) {
                for (int i = 0; i < 2; i++) {
                    _groundObjects[i] = Instantiate (groundPrefab);
                    _groundObjects[i].transform.parent = transform;
                    _groundObjects[i].transform.localPosition = new Vector3 (0, 0.25f, 2f + (4f * i));
                }
            }
        }

        private void SpawnObstacles () {
            if (obstaclePrefab != null) {
                int tmp = Random.Range (1, MAX_OBSTACLES + 1);
                for (int i = 0; i < tmp; i++) {
                    Obstacle o = Instantiate (obstaclePrefab);
                    o.transform.parent = transform;
                    o.transform.localPosition = new Vector3 (-2, -2, -2);
                    _obstacleObjects.Add (o);
                }
                AdjustObstacles ();
            }
        }

        private void AdjustObstacles () {
            if (_obstacleObjects.Count == MAX_OBSTACLES) {
                for (int i = 0; i < _obstacleObjects.Count; i++) {
                    _obstacleObjects[i].transform.localPosition = new Vector3 (-1f + i, 1f, 2f);
                }
            } else if (_obstacleObjects.Count == 2) {
                for (int i = 0; i < _obstacleObjects.Count; i++) {
                    int pos = Random.Range (-1, 2);
                    bool notUnique = true;
                    while (notUnique) {
                        notUnique = false;
                        foreach (Obstacle o in _obstacleObjects) {
                            if (o.transform.localPosition.x == pos) {
                                notUnique = true;
                                pos = Random.Range (-1, 2);
                            }
                        }
                    }
                    _obstacleObjects[i].transform.localPosition = new Vector3 (pos, 1f, 2f);
                }
            } else if (_obstacleObjects.Count == 1) {
                _obstacleObjects[0].transform.localPosition = new Vector3 (Random.Range (-1, 2), 1f, 2f);
            }
        }

        private void UpdateAnswerWall () {
            int tmp;
            bool foundCorrect = false;
            if (_obstacleObjects.Count == 3) {
                foreach (Obstacle o in _obstacleObjects) {
                    if (int.TryParse (o.Number, out tmp)) {
                        if (tmp == tileManager.math.CorrectAnswer) {
                            foundCorrect = true;
                        }
                    }
                }
                if (!foundCorrect) {
                    List<int> answers = tileManager.math.PossibleAnswers;
                    tmp = Random.Range (0, 3);
                    if (answers.Count > 0) {
                        for (int i = 0; i < 3; i++) {
                            if (i != tmp) {
                                _obstacleObjects[i].Number = answers[Random.Range (0, answers.Count)].ToString ();
                            } else {
                                _obstacleObjects[tmp].Number = tileManager.math.CorrectAnswer.ToString ();
                            }
                        }
                    }
                }
            }
        }

        private void SetMaterial (Material material) {
            _groundObjects[0].SetMaterial (material);
            foreach (Obstacle o in _obstacleObjects) {
                o.SetMaterial (material);
            }
        }

        private void ToggleObstacleColliders (bool enabled) {
            foreach (Obstacle o in _obstacleObjects) {
                o.ToggleCollider (enabled);
            }
        }

        public void Report (int ans) {
            ToggleObstacleColliders (false);
            if (tileManager.Report (ans)) {
                SetMaterial (materials[CORRECT_MAT]);
            } else {
                SetMaterial (materials[INCORRECT_MAT]);
            }
        }

        public void PrintOnObstacles () {
            List<int> ans = tileManager.math.PossibleAnswers;
            foreach (Obstacle o in _obstacleObjects) {
                if (ans.Count > 0) {
                    o.Number = ans[Random.Range (0, ans.Count)].ToString ();
                }
            }
        }

        private void OnTriggerExit (Collider other) {
            if (other.tag == "Player") {
                StartCoroutine (Fall ());
            }
        }
        
        private IEnumerator Fall () {
                yield return new WaitForSeconds (1.5f);
                AdjustObstacles ();
                tileManager.Tiles.Push (gameObject);
                ResetObstacles ();
                gameObject.SetActive (false);
                tileManager.SpawnTile ();
        }

        private void ResetObstacles () {
            foreach (Obstacle o in _obstacleObjects) {
                o.Number = string.Empty;
                o.ToggleCollider (true);
                SetMaterial (materials[DEFAULT_MAT]);
            }
        }
    }
}
