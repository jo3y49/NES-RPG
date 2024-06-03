using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : GameManager {
    public static GameDataManager Instance { get; protected set; }
    private (Vector3, int) lastPositionAndScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameData = new GameData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //PlayerData

    public void SetStats(Stats stats)
    {
        gameData.playerData.stats = stats;
    }

    public Stats GetStats()
    {
        return gameData.playerData.stats;
    }

    public bool AddLives(int lives = 1)
    {
        if (gameData.playerData.lives < 0)
        {
            return RemoveLives(lives);
        }

        gameData.playerData.lives += lives;

        return true;
    }

    public bool RemoveLives(int lives = 1)
    {
        Mathf.Abs(lives);
        if (gameData.playerData.lives - lives <= 0)
        {
            gameData.statData.livesLost += gameData.playerData.lives;
            gameData.playerData.lives = 0;
            return false;
        }

        gameData.playerData.lives -= lives;
        gameData.statData.livesLost += lives;

        return true;
    }

    public int GetLives()
    {
        return gameData.playerData.lives;
    }

    public void SetLevel(int level)
    {
        gameData.playerData.level = level;
    }

    public void AddLevel(int level = 1)
    {
        gameData.playerData.level += level;
    }

    public int GetLevel()
    {
        return gameData.playerData.level;
    }

    public void AddXP(int xp = 1)
    {
        gameData.playerData.xp += xp;
    }

    public int GetXP()
    {
        return gameData.playerData.xp;
    }

    public void SetScore(int score)
    {
        gameData.playerData.score = score;
    }

    public void AddScore(int score = 1)
    {
        if (score < 0)
        {
            RemoveScore(score);
        }

        gameData.playerData.score += score;
        gameData.statData.scoreCollected += score;
    }

    public void RemoveScore(int score = 1)
    {
        Mathf.Abs(score);
        if (gameData.playerData.score - score <= 0)
        {
            gameData.playerData.score = 0;
        }

        gameData.playerData.score -= score;
    }

    public int GetScore()
    {
        return gameData.playerData.score;
    }

    //StatData

    public void SetPlaytime(float playtime)
    {
        gameData.statData.playtime = playtime;
    }

    public void AddPlaytime(float playtime)
    {
        gameData.statData.playtime += playtime;
    }

    public float GetPlaytime()
    {
        return gameData.statData.playtime;
    }

    public void AddDeaths(int deaths = 1)
    {
        gameData.statData.deaths += deaths;
    }

    public int GetDeaths()
    {
        return gameData.statData.deaths;
    }

    public void AddKills(int kills = 1)
    {
        gameData.statData.kills += kills;
    }

    public int GetKills()
    {
        return gameData.statData.kills;
    }

    public void AddDamageDealt(int damageDealt = 1)
    {
        gameData.statData.damageDealt += damageDealt;
    }

    public int GetDamageDealt()
    {
        return gameData.statData.damageDealt;
    }

    public void AddDamageTaken(int damageTaken = 1)
    {
        gameData.statData.damageTaken += damageTaken;
    }

    public int GetDamageTaken()
    {
        return gameData.statData.damageTaken;
    }

    // worldData

    public void SetCurrentScene(int currentScene)
    {
        gameData.worldData.currentScene = currentScene;
    }

    public int GetCurrentScene()
    {
        return gameData.worldData.currentScene;
    }

    public void SetPlayerPosition(Vector2 playerPosition)
    {
        gameData.worldData.playerPosition = playerPosition;
    }

    public Vector2 GetPlayerPosition()
    {
        return gameData.worldData.playerPosition;
    }

    // settingsData

    public void SetSettings(SettingsData settingsData)
    {
        gameData.settingsData = settingsData;
    }

    public void SetLastPositionAndScene(Vector3 playerPosition, int currentScene)
    {
        lastPositionAndScene = (playerPosition, currentScene);
    }

    public (Vector3, int) GetLastPositionAndScene()
    {
        return lastPositionAndScene;
    }
}