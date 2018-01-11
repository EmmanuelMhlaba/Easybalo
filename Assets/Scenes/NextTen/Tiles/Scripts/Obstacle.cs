using UnityEngine;
using UnityEngine.UI;

namespace Easybalo.NextTen.Tiles {
    public class Obstacle : MonoBehaviour {
        private Tile _parentTile;
        private Text _numberText;
        private MeshRenderer _meshRenderer;
        private Collider _collider;

        public string Number {
            get {
                if (_numberText != null) {
                    return _numberText.text;
                }
                return null;
            }
            set {
                if (_numberText != null) {
                    _numberText.text = value;
                }
            }
        }

        private void Awake () {
            _numberText = GetComponentInChildren<Text> ();
            _meshRenderer = GetComponent<MeshRenderer> ();
            _collider = GetComponent<Collider> ();
        }

        private void Start () {
            _parentTile = GetComponentInParent<Tile> ();
        }

        public void SetMaterial (Material material) {
            _meshRenderer.material = material;
        }

        private void OnTriggerEnter (Collider other) {
            int tmp;
            if (other.tag == "Player" && _parentTile != null && _numberText != null) {
                if (int.TryParse (_numberText.text, out tmp)) {
                    _parentTile.Report (tmp);
                }
            }
        }

        public void ToggleCollider (bool enabled) {
            _collider.enabled = enabled;
        }
    }
}