using System.Collections.Generic;
using UnityEngine;

public class HostileAreaManager : MonoBehaviour {
    public GameObject boss;
    public int worldNumber = 1;
    public List<GameObject> barriers = new();

    private void Start() {
        if (GameDataManager.Instance.GetDefeatedBosses() >= worldNumber) {
            boss.SetActive(false);

            foreach (GameObject barrier in barriers) {
                barrier.SetActive(false);
            }
        }

        HostileWorldManager.Instance.ToggleHostility(true);
        MenuManager.Instance.SaveZone(false);
    }
}