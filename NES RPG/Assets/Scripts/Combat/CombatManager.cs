using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public static CombatManager Instance { get; private set; }
    public PlayerCombat player { get; private set;}
    public List<EnemyCombat> enemies { get; private set;}
    private Queue<CharacterCombat> characterOrder = new();

    [SerializeField] private CombatMenuManager combatMenuManager;
    [SerializeField] private PlayerCombatMenu playerCombatMenu;

    public bool fight = true;
    private bool menuCommandGiven = false;
    public int turnCount = 0;
    public bool endUserTurn = true;

    public float placeholderAnimationWaitTime = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    
    public void Initialize(GameObject playerObject, List<GameObject> enemyObjects)
    {
        gameObject.SetActive(true);
        ResetCountingVariables();
        SortCharactersIntoQueue(playerObject, enemyObjects);

        combatMenuManager.Initialize(player, enemies);
        playerCombatMenu.Initialize(player, enemies, this, combatMenuManager);

        StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        while (fight)
        {
            CharacterCombat currentCharacter = characterOrder.Peek();
            Coroutine characterAction = CharacterAction(currentCharacter);

            yield return characterAction;

            fight = NextTurnLogic(currentCharacter);
        }

        if (player.stats.GetHealth() > 0)
        {
            MenuManager.Instance.EndCombat();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayerTurn(PlayerCombat player)
    {
        combatMenuManager.ActiveText("Player turn");
        menuCommandGiven = false;

        player.StartTurn();
        playerCombatMenu.Activate(player);

        yield return new WaitUntil(() => menuCommandGiven);

        yield return new WaitForSeconds(placeholderAnimationWaitTime);
        
    }

    private IEnumerator EnemyTurn(EnemyCombat enemy)
    {
        combatMenuManager.ActiveText("Enemy turn" + enemy.enemyData.enemyName);

        enemy.StartTurn();
        EnemyAiAction(enemy);

        yield return new WaitForSeconds(placeholderAnimationWaitTime);
    }

    public void ReceiveAction(string action, CharacterCombat user, CharacterCombat target)
    {
        ActionResult actionResult = user.DoAction(action, target);
        
        HandleActionResult(actionResult);

        combatMenuManager.UpdateUI();
        menuCommandGiven = true;
    }

    private void HandleActionResult(ActionResult actionResult)
    {
        if (actionResult.message != null) combatMenuManager.ActiveText(actionResult.message);

        actionResult.DoResult();

        endUserTurn = !actionResult.stun;
    }

    private void EnemyAiAction(EnemyCombat enemy)
    {
        (string, CharacterCombat) action = enemy.DecideAction(player);

        ReceiveAction(action.Item1, enemy, action.Item2);
    }

    private bool NextTurnLogic(CharacterCombat currentCharacter)
    {
        CheckForDeaths();

        if (currentCharacter.stats.GetHealth() >= 0 && endUserTurn)
        {
            characterOrder.Dequeue();
            characterOrder.Enqueue(currentCharacter);
        }

        combatMenuManager.UpdateUI();

        bool fight = characterOrder.Count > 1;
        return fight;
    }

    private void CheckForDeaths()
    {
        foreach (CharacterCombat character in characterOrder)
        {
            if (character.stats.GetHealth() <= 0)
            {
                var newTurnOrder = new Queue<CharacterCombat>(characterOrder.Where(x => x != character));
                characterOrder = newTurnOrder;
                character.EndBattle();

                if (character is PlayerCombat player)
                {
                    foreach (EnemyCombat enemy in enemies)
                    {
                        Destroy(enemy.gameObject);
                    }
                    StopAllCoroutines();
                    GameDataManager.Instance.QuitGame();   
                }
                else
                {
                    enemies.Remove((EnemyCombat)character);
                }
            }
        }
    }

    private Coroutine CharacterAction(CharacterCombat currentCharacter)
    {
        Coroutine combatCoroutine;

        if (currentCharacter is PlayerCombat player) combatCoroutine = StartCoroutine(PlayerTurn(player));

        else combatCoroutine = StartCoroutine(EnemyTurn((EnemyCombat)currentCharacter));

        return combatCoroutine;
    }

    private void ResetCountingVariables()
    {
        enemies = new();
        characterOrder = new();
        fight = true;
        turnCount = 0;
    }

    private void SortCharactersIntoQueue(GameObject playerObject, List<GameObject> enemyObjects)
    {
        // consider changing to ffx system where attacks have a speed value
        SortedSet<CharacterCombat> sortedCharacters = InitiativeOrder();

        foreach (GameObject enemy in enemyObjects)
        {
            enemy.TryGetComponent(out EnemyCombat enemyCombat);
            enemies.Add(enemyCombat);
            sortedCharacters.Add(enemyCombat);
        }

        playerObject.TryGetComponent(out PlayerCombat playerCombat);
        player = playerCombat;
        sortedCharacters.Add(playerCombat);


        foreach (CharacterCombat character in sortedCharacters)
        {
            characterOrder.Enqueue(character);
            character.PrepareBattle();
        }
    }

    private SortedSet<CharacterCombat> InitiativeOrder()
    {
        return new(

            Comparer<CharacterCombat>.Create((c1, c2) =>
            {
                int speedComparison = c2.stats.GetSpeed().CompareTo(c1.stats.GetSpeed());
                if (speedComparison == 0)
                {
                    if (c1 is PlayerCombat && c2 is not PlayerCombat) return -1;
                    if (c2 is PlayerCombat && c1 is not PlayerCombat) return 1;

                    int nameComparison = c1.name.CompareTo(c2.name);
                    return nameComparison;
                }

                return speedComparison;
            })
        );
    }
}