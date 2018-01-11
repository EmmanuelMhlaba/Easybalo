using UnityEngine;

namespace Easybalo.NextTen.Input {
    public class ScreenControls : MonoBehaviour {
        public ScreenZone[] leftZones;
        public ScreenZone[] rightZones;
        public bool LeftZonesActive { get; private set; }
        public bool RightZonesActive { get; private set; }

        private void Awake () {
            LeftZonesActive = false;
            RightZonesActive = false;
        }

        private void Update () {
            LeftZonesActive = CheckZones (leftZones);
            RightZonesActive = CheckZones (rightZones);
        }

        private bool CheckZones (ScreenZone[] zones) {
            for (int i = 0; i < zones.Length; i++) {
                if (zones[i].Active) {
                    return true;
                }
            }
            return false;
        }
    }
}