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
        characterInfoList.Clear();
        
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
            text.text = PlayerString(player.Key);
            characterInfoList.Add(player.Key, text);
        }

        foreach (KeyValuePair<EnemyCombat, Stats> enemyPair in enemyStats) {
            KeyValuePair<EnemyCombat, Stats> enemy = enemyPair;
            GameObject characterInfo = Instantiate(characterInfoPrefab, enemyNameContainer.transform);
            TextMeshProUGUI text = characterInfo.GetComponentInChildren<TextMeshProUGUI>();
            text.text = EnemyString(enemy.Key);
            text.alignment = TextAlignmentOptions.TopRight;
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
                text.text = PlayerString(player.Key);
            }
        }

        foreach (KeyValuePair<EnemyCombat, Stats> enemy in enemyStats) {
            TextMeshProUGUI text = characterInfoList[enemy.Key];
            if (enemy.Value.GetHealth() <= 0) {
                text.text = $"{enemy.Key.enemyData.enemyName}: is Defeated";
            } else {
                text.text = EnemyString(enemy.Key);
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

    private string PlayerString(PlayerCombat player) {
        return NameString(player) + "\n" + HealthString(player) + "\n" + ManaString(player) + "\n" + ElementString(player) + "\n" + LevelString() + "\n" + StunString(player);
    }

    private string EnemyString(EnemyCombat enemy) {
        return NameString(enemy) + "\n" + HealthString(enemy) + "\n" + ElementString(enemy) + "\n" + StunString(enemy);
    }

    private string NameString(CharacterCombat character) {
        return $"{character.characterName}";
    }

    private string HealthString(CharacterCombat character) {
        return $"{character.stats.GetHealth()} HP";
    }

    private string ManaString(CharacterCombat character) {
        return $"{character.stats.GetMana()} spells";
    }

    private string ElementString(CharacterCombat character) {
        return $"Element: {character.affectedElement}";
    }

    private string LevelString() {
        return $"Level: {GameDataManager.Instance.GetLevel()}";
    }

    private string StunString(CharacterCombat character) {
        return character.stunned ? "Stunned" : "";
    }
}