using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatMenuManager : MonoBehaviour {
    private Dictionary<PlayerCombat, Stats> playerStats = new();
    private Dictionary<EnemyCombat, Stats> enemyStats = new();
    private List<string> battleLog = new();
    public TextMeshProUGUI battleText;
    public GameObject playerNameContainer, enemyNameContainer;
    public GameObject characterInfoPrefab;
    private Dictionary<CharacterCombat, TextMeshProUGUI> characterInfoList = new();

    public void Initialize(PlayerCombat player, List<EnemyCombat> enemyObjects) {

        playerStats.Clear();
        enemyStats.Clear();
        
        playerStats.Add(player, player.stats);

        foreach (EnemyCombat enemy in enemyObjects) {
            enemyStats.Add(enemy, enemy.stats);
        }

        SetHealthMenus();
        battleText.text = "";
        battleLog.Clear();
    }

    private void SetHealthMenus()
    {
        for (int i = playerNameContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(playerNameContainer.transform.GetChild(i).gameObject);
        }
        for (int i = enemyNameContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(enemyNameContainer.transform.GetChild(i).gameObject);
        }
        foreach (KeyValuePair<PlayerCombat, Stats> player in playerStats) {
            GameObject characterInfo = Instantiate(characterInfoPrefab, playerNameContainer.transform);
            TextMeshProUGUI text = characterInfo.GetComponentInChildren<TextMeshProUGUI>();
            string healthString = $"{player.Key.characterName}: {player.Value.GetHealth()}";
            string manaString = $"{player.Value.GetMana()} spells remaining";
            string elementString = $"Active element: {player.Key.affectedElement}";
            string stunString = player.Key.stunned ? "Stunned" : "";

            text.text = $"{healthString}\n{manaString}\n{elementString}\n{stunString}";
            characterInfoList.Add(player.Key, text);
        }

        foreach (KeyValuePair<EnemyCombat, Stats> enemy in enemyStats) {
            GameObject characterInfo = Instantiate(characterInfoPrefab, enemyNameContainer.transform);
            TextMeshProUGUI text = characterInfo.GetComponentInChildren<TextMeshProUGUI>();
            string healthString = $"{enemy.Key.enemyData.enemyName}: {enemy.Value.GetHealth()}";
            string elementString = $"Active element: {enemy.Key.affectedElement}";
            string stunString = enemy.Key.stunned ? "Stunned" : "";

            text.text = $"{healthString}\n{elementString}\n{stunString}";
            characterInfoList.Add(enemy.Key, text);
        }
    }

    public void UpdateUI() {
        UpdateHealth();
    }

    private void UpdateHealth() {
        foreach (KeyValuePair<PlayerCombat, Stats> player in playerStats) {
            TextMeshProUGUI text = characterInfoList[player.Key];
            if (player.Value.GetHealth() <= 0) {
                text.text = $"{player.Key.characterName}: is Defeated";
            } else {
                string healthString = $"{player.Key.characterName}: {player.Value.GetHealth()}";
                string manaString = $"{player.Value.GetMana()} spells remaining";
                string elementString = $"Active element: {player.Key.affectedElement}";
                string stunString = player.Key.stunned ? "Stunned" : "";    

                text.text = $"{healthString}\n{manaString}\n{elementString}\n{stunString}";
            }
        }

        foreach (KeyValuePair<EnemyCombat, Stats> enemy in enemyStats) {
            TextMeshProUGUI text = characterInfoList[enemy.Key];
            if (enemy.Value.GetHealth() <= 0) {
                text.text = $"{enemy.Key.enemyData.enemyName}: is Defeated";
            } else {
                string healthString = $"{enemy.Key.enemyData.enemyName}: {enemy.Value.GetHealth()}";
                string elementString = $"Active element: {enemy.Key.affectedElement}";
                string stunString = enemy.Key.stunned ? "Stunned" : "";

                text.text = $"{healthString}\n{elementString}\n{stunString}";
            }
        }
    }

    public void ActiveText(string text) {
        battleText.text = text;
        AddToBattleLog(text);
    }

    private void AddToBattleLog(string text) {
        battleLog.Add(text);
    }
}