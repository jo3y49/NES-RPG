using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HostileWorldManager : WorldManager {
    public static new HostileWorldManager Instance { get; protected set; }
    
    protected InputActions actions;
    public float combatCheckInterval = 1;
    public bool fighting = false;
    private Coroutine combatCheck;

    protected override void Awake()
    {
        actions = new InputActions();
        
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
        menuManager.StartCombat(player);
    }

    public void EndCombat()
    {
        fighting = false;
    }
}