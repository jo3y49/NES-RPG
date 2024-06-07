using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatMenu : MonoBehaviour {
    public Button buttonPrefab;
    public GameObject initialButtonContainer, spellButtonContainer, combatExplanationContainer;
    public Button attackButton, spellButton, explanationButton;
    private CombatManager combatManager;
    private CombatMenuManager combatMenuManager;
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
        initialButtonContainer.SetActive(false);
        combatExplanationContainer.SetActive(true);
    }

    public void BackFromExplanation()
    {
        initialButtonContainer.SetActive(true);
        combatExplanationContainer.SetActive(false);
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
        attackButton.onClick.AddListener(SelectAttack);
        spellButton.onClick.AddListener(PickSpell);
        explanationButton.onClick.AddListener(PickExplanation);

        ButtonManager buttonManager = spellButtonContainer.GetComponent<ButtonManager>();
        buttonManager.ClearButtons();

        for (int i = 0; i < spellButtonContainer.transform.childCount; i++)
        {
            Destroy(spellButtonContainer.transform.GetChild(i).gameObject);
        }

        PlayerCombatOptions options = new();

        foreach (string spell in options.spells)
        {
            Button spellButton = Instantiate(buttonPrefab, spellButtonContainer.transform);
            spellButton.GetComponentInChildren<TextMeshProUGUI>().text = spell;
            spellButton.onClick.AddListener(() => SelectSpell(spell));
            buttonManager.AddButton(spellButton);
        }

        Button back = Instantiate(buttonPrefab, spellButtonContainer.transform);
        back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        back.onClick.AddListener(BackFromSpell);
        buttonManager.AddButton(back);
    }
}