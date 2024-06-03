using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatSceneManager : MonoBehaviour {
    public Transform playerLocation, enemy1Location, enemy2Location, enemy3Location;
    private GameObject player;
    private List<GameObject> enemies;
    private void Awake() 
    {
        player = PlayerMovement.Instance.gameObject;
        enemies = HostileWorldManager.Instance.activeEnemies;
        CombatManager.Instance.Initialize(player, enemies);

        SetCharacterLocations();
    }

    private void SetCharacterLocations()
    {
        player.transform.position = playerLocation.position;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = i switch
            {
                0 => enemy1Location.position,
                1 => enemy2Location.position,
                2 => enemy3Location.position,
                _ => enemies[i].transform.position,
            };
            
            enemies[i].SetActive(true);
        }
    }
}