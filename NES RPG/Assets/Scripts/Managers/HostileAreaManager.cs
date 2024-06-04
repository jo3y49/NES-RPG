using UnityEngine;

public class HostileAreaManager : MonoBehaviour {
    public GameObject boss;
    public int worldNumber = 1;

    private void Start() {
        if (GameDataManager.Instance.GetDefeatedBosses() >= worldNumber) {
            boss.SetActive(false);
        }
    }
}