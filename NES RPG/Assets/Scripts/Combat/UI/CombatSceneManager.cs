using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatSceneManager : MonoBehaviour {
    public Transform playerLocation, enemy1Location, enemy2Location, enemy3Location;
    protected EnemyData[] enemyDatas;
    public GameObject enemyPrefab;
    private void Awake() {
        enemyDatas = Resources.LoadAll<EnemyData>("Enemies");

        GameObject playerObject = PlayerMovement.Instance.gameObject;
        List<GameObject> enemyObjects = new List<GameObject> { GetRandomEnemy() };

        CombatManager.Instance.Initialize(playerObject, enemyObjects);

        CombatManager.Instance.player.transform.position = playerLocation.position;

        List<Transform> enemies = enemyObjects.Select(enemy => enemy.transform).ToList();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].position = i switch
            {
                0 => enemy1Location.position,
                1 => enemy2Location.position,
                2 => enemy3Location.position,
                _ => enemies[i].position,
            };
        }
    }

    private GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, enemyDatas.Length);
        EnemyData enemyData = enemyDatas[index];
        GameObject enemyObject = Instantiate(enemyPrefab);
        EnemyCombat enemy = enemyObject.GetComponent<EnemyCombat>();
        enemy.SetEnemyData(enemyData);
        return enemyObject;
    }
}