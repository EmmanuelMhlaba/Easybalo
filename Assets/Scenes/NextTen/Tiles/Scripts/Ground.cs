using UnityEngine;

namespace Easybalo.NextTen.Tiles {
    public class Ground : MonoBehaviour {
        private MeshRenderer _meshRenderer;

        private void Awake () {
            _meshRenderer = GetComponent<MeshRenderer> ();
        }

        public void SetMaterial (Material material) {
            _meshRenderer.material = material;
        }
    }
}
