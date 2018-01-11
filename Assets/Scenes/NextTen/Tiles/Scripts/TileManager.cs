using System.Collections.Generic;
using UnityEngine;

namespace Easybalo.NextTen.Tiles {
    public class TileManager : MonoBehaviour {
        public Gameplay.GameManager gameManager;
        public Math.Math math;
        public Transform currentAttachPoint;
        private Transform initialAttachPoint;
        public GameObject tilePrefab;
        private Stack<GameObject> tiles = new Stack<GameObject> ();
        private List<GameObject> allTiles = new List<GameObject> ();
        public Stack<GameObject> Tiles { get { return tiles; } }

        private void Start () {
            initialAttachPoint = currentAttachPoint;
        }

        private void CreateTiles (int n) {
            if (tilePrefab != null) {
                for (int i = 0; i < n; i++) {
                    GameObject t = Instantiate (tilePrefab);
                    t.transform.parent = transform;
                    t.SetActive (false);
                    tiles.Push (t);
                    allTiles.Add (t);
                }
            }
        }

        public void SpawnTile () {
            if (tiles.Count == 0) {
                CreateTiles (10);
            }
            GameObject t = tiles.Pop ();
            t.SetActive (true);
            t.transform.position = currentAttachPoint.transform.position;
            t.GetComponent<Tile> ().tileManager = this;
            t.GetComponent<Tile> ().PrintOnObstacles ();
            currentAttachPoint = t.transform.GetChild (1).transform.GetChild (0);
        }

        public void SpawnTiles (int n) {
            for (int i = 0; i < n; i++) {
                SpawnTile ();
            }
        }

        public void ResetTiles () {
            currentAttachPoint = initialAttachPoint;
            foreach (GameObject go in allTiles) {
                Destroy (go);
            }
            allTiles.Clear ();
        }

        public bool Report (int ans) {
            if (ans == math.CorrectAnswer) {
                gameManager.CorrectAnswer ();
                return true;
            } else {
                gameManager.IncorrectAnswer ();
                return false;
            }
        }
    }
}