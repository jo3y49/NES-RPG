using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Stats stats = new(30,4,20,10,20,10,5);
    public int level = 1;
    public int swordLevel = 1;
    public int xp = 0;
    
}
[System.Serializable]
public class StatData
{
    public float playtime = 0;
    public int kills = 0;
    public int damageDealt = 0;
    public int damageTaken = 0;
    public int stuns = 0;
}
[System.Serializable]
public class WorldData
{
    public int currentScene = 1;
    public Vector2 playerPosition = new();
    public int defeatedBosses = 0;
}

[System.Serializable]
public class SettingsData
{
    
}

[System.Serializable]
public class GameData
{
    public PlayerData playerData = new();
    public StatData statData = new();
    public WorldData worldData = new();
    public SettingsData settingsData = new();

    public GameData NewGame()
    {
        playerData = new();
        statData = new();
        worldData = new();

        return this;
    }
}