using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCombatMenu : MonoBehaviour {
    public Button buttonPrefab;
    public GameObject initialButtonContainer, spellButtonContainer, combatExplanationContainer;
    public Button attackButton, spellButton, explanationButton;
    private CombatManager combatManager;
    private CombatMenuManager combatMenuManager;
    private Dictionary<PlayerCombat, PlayerCombatOptions> playerOptions = new();
    private string selectedAction;
    private PlayerCombat activePlayer;
    private EnemyCombat target;

    public void Initialize(PlayerCombat player, EnemyCombat enemy, CombatManager combatManager, CombatMenuManager combatMenuManager)
    {
        this.combatManager = combatManager;
        this.combatMenuManager = combatMenuManager;
        target = enemy;
        
        SetPlayerButtons(player);

        initialButtonContainer.SetActive(true);
        spellButtonContainer.SetActive(false);
        combatExplanationContainer.SetActive(false);
        gameObject.SetActive(false);
    }

    private void PickSpell()
    {
        combatMenuManager.ActiveText("Pick a spell");
        initialButtonContainer.SetActive(false);
        spellButtonContainer.SetActive(true);
    }

    private void BackFromSpell()
    {
        combatMenuManager.ActiveText("");
        initialButtonContainer.SetActive(true);
        spellButtonContainer.SetActive(false);
    }

    private void PickExplanation()
    {
        combatExplanationContainer.SetActive(true);
    }

    private void SelectAttack()
    {
        selectedAction = "Attack";
        initialButtonContainer.SetActive(false);
        SendAction();
    }

    private void SelectSpell(string spell)
    {
        selectedAction = spell;
        spellButtonContainer.SetActive(false);
        SendAction();
    }

    private void SendAction()
    {
        combatManager.ReceiveAction(selectedAction, activePlayer, target);
        gameObject.SetActive(false);
    }

    public void Activate(PlayerCombat player)
    {
        gameObject.SetActive(true);
        initialButtonContainer.SetActive(true);
        activePlayer = player;
        spellButton.enabled = activePlayer.stats.GetMana() > 0;
    }

    private void SetPlayerButtons(PlayerCombat player)
    {
        playerOptions.Clear();
        attackButton.onClick.AddListener(SelectAttack);
        spellButton.onClick.AddListener(PickSpell);
        explanationButton.onClick.AddListener(PickExplanation);

        for (int i = 0; i < spellButtonContainer.transform.childCount; i++)
        {
            Destroy(spellButtonContainer.transform.GetChild(i).gameObject);
        }

        PlayerCombatOptions options = new(buttonPrefab);

        foreach (KeyValuePair<string, Button> spell in options.spells)
        {
            spell.Value.onClick.AddListener(() => SelectSpell(spell.Key));
            // spell.Value.OnPointerEnter((eventData) => HoverAction(spell.Key, eventData));

            spell.Value.transform.SetParent(spellButtonContainer.transform);
        }

        Button back = Instantiate(buttonPrefab, spellButtonContainer.transform);
        back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        back.onClick.AddListener(BackFromSpell);

        playerOptions.Add(player, options);
    }
}