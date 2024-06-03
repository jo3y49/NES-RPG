using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour {
    public DebugBattleManager debugBattleManager;

    private void Start() {
        DebugBattleManager[] debugBattleManagers = FindObjectsOfType<DebugBattleManager>();
        if (debugBattleManagers.Length == 0)
        {
            GameDataManager.Instance.QuitGame();
        } else
        {
            foreach (DebugBattleManager dbm in debugBattleManagers)
            {
                dbm.gameObject.SetActive(true);
            }
        }
    }
}