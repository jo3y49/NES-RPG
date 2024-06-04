using System.Collections.Generic;
using UnityEngine;

public class OverWorldManager : MonoBehaviour {
    public List<GameObject> barriers = new();
    private void Start() {
        HostileWorldManager.Instance.hostile = false;
        PlayerMovement.Instance.GetComponent<PlayerCombat>().EndBattle();

        for (int i = 0; i < GameDataManager.Instance.GetDefeatedBosses(); i++) {
            if (i < barriers.Count) barriers[i].SetActive(false);
        }
    }
}