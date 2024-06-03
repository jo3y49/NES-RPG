using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HostileWorldManager : WorldManager {
    public static new HostileWorldManager Instance { get; protected set; }
    
    protected InputActions actions;
    protected EnemyData[] enemyDatas;
    public GameObject enemyPrefab;
    public List<GameObject> activeEnemies = new();
    public float combatCheckInterval = 1;
    public bool hostile = true;
    private Coroutine combatCheck;

    protected override void Awake()
    {
        if (Instance == null)
        {
            actions = new InputActions();
            enemyDatas = Resources.LoadAll<EnemyData>("Enemies");
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        actions.Player.Enable();

        actions.Player.Move.performed += context => CheckForEnemies();
        actions.Player.Move.canceled += context => StopCheck();
    }

    private void OnDisable() {
        if (actions == null) return;

        actions.Player.Move.performed -= context => CheckForEnemies();
        actions.Player.Move.canceled -= context => StopCheck();

        actions.Player.Disable();
    }

    private void CheckForEnemies()
    {
        if (combatCheck == null && hostile)
        {
            combatCheck = StartCoroutine(EnemyCheck());
            Debug.Log("Checking for enemies");
        }
    }

    private void StopCheck()
    {
        if (combatCheck != null)
        {
            StopCoroutine(combatCheck);
            combatCheck = null;
            Debug.Log("Stopped checking for enemies");
        }
    }

    private IEnumerator EnemyCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(combatCheckInterval);
            if (Random.Range(0, 100) < 10)
            {
                hostile = false;
                StartCombat();
                StopCheck();
            }
        }
    }

    public virtual void StartCombat()
    {
        activeEnemies.Clear();
        
        GameObject enemyObject = GetRandomEnemy();
        activeEnemies.Add(enemyObject);
        
        menuManager.StartCombat();
    }

    public virtual void StartCombat(EnemyData enemy)
    {
        activeEnemies.Clear();
        
        GameObject enemyObject = GetEnemy(enemy);
        activeEnemies.Add(enemyObject);

        menuManager.StartCombat();
    }

    private GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, enemyDatas.Length);
        EnemyData enemyData = enemyDatas[index];
        return GetEnemy(enemyData);
    }

    private GameObject GetEnemy(EnemyData enemy)
    {
        GameObject enemyObject = Instantiate(enemyPrefab);
        DontDestroyOnLoad(enemyObject);
        EnemyCombat enemyCombat = enemyObject.GetComponent<EnemyCombat>();
        enemyCombat.SetEnemyData(enemy);
        enemyObject.SetActive(false);
        return enemyObject;
    }

    public void EndCombat()
    {
        hostile = true;
    }
}