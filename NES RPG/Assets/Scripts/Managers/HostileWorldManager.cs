using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HostileWorldManager : WorldManager {
    public static new HostileWorldManager Instance { get; protected set; }
    protected EnemyData[] enemyDatas;
    public GameObject enemyPrefab;
    protected InputActions actions;
    public float combatCheckInterval = 1;
    public bool fighting = false;
    private Coroutine combatCheck;

    protected override void Awake()
    {
        actions = new InputActions();
        enemyDatas = Resources.LoadAll<EnemyData>("Enemies");
        Instance = this;
    }

    private void OnEnable() {
        actions.Player.Enable();

        actions.Player.Move.performed += context => CheckForEnemies();
        actions.Player.Move.canceled += context => StopCheck();
    }

    private void OnDisable() {
        actions.Player.Move.performed -= context => CheckForEnemies();
        actions.Player.Move.canceled -= context => StopCheck();

        actions.Player.Disable();
    }

    private void CheckForEnemies()
    {
        if (!fighting)
            combatCheck = StartCoroutine(EnemyCheck());
    }

    private void StopCheck()
    {
        if (combatCheck != null) StopCoroutine(combatCheck);
    }

    private IEnumerator EnemyCheck()
    {
        while (!fighting)
        {
            yield return new WaitForSeconds(combatCheckInterval);
            if (Random.Range(0, 100) < 5)
            {
                fighting = true;
                StartCombat();
            }
        }
    }

    public virtual void StartCombat()
    {
        
        List<GameObject> enemies = new();
        enemies.Add(GetRandomEnemy());

        menuManager.StartCombat(new List<GameObject> {player}, enemies);
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

    public void EndCombat()
    {
        fighting = false;
    }
}