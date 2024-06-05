using System.Collections.Generic;
using UnityEngine;

public class OverWorldManager : MonoBehaviour {
    public List<GameObject> barriers = new();
    private void Start() {
        HostileWorldManager.Instance.ToggleHostility(false);
        HostileWorldManager.Instance.player.GetComponent<PlayerCombat>().EndBattle();
        MenuManager.Instance.SaveZone(true);

        for (int i = 0; i < GameDataManager.Instance.GetDefeatedBosses(); i++) {
            if (i < barriers.Count) barriers[i].SetActive(false);
        }
    }
}