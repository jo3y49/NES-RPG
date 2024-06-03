using UnityEngine;

public class OverWorldManager : MonoBehaviour {
    private void Start() {
        HostileWorldManager.Instance.hostile = false;
    }
}