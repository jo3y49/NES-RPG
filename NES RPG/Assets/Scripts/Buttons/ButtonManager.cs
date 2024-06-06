using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour {
    public List<Button> buttons = new();
    private Dictionary<Button, ButtonHandler> buttonHandlers = new();
    private Button highlightedButton;

    private InputActions actions;

    private void Awake() {
        foreach (Button button in buttons) {
            buttonHandlers.Add(button, button.GetComponent<ButtonHandler>());
        }

        actions = new InputActions();
    }

    private void OnEnable() {
        actions.Menu.Enable();
        actions.Menu.Navigate.performed += Navigate;
        actions.Menu.Select.performed += _ => Select();

        if (buttons.Count > 0) {
            highlightedButton = buttons[0];
            buttonHandlers[highlightedButton].Highlight();
        }
    }

    private void OnDisable() {
        foreach (Button button in buttons) {
            buttonHandlers[button].UnHighlight();
        }

        actions.Menu.Navigate.performed -= Navigate;
        actions.Menu.Select.performed -= _ => Select();
        actions.Menu.Disable();
    }

    private void Navigate(InputAction.CallbackContext context) {
        if (highlightedButton == null) return;
        Vector2 input = context.ReadValue<Vector2>();

        bool selected = false;
        int attemptCount = 0;  // safeguard against infinite loop
        int maxAttempts = buttons.Count;

        while (!selected && attemptCount < maxAttempts) {
            if (input.y > 0) {
                buttonHandlers[highlightedButton].UnHighlight();
                int index = buttons.IndexOf(highlightedButton);

                if (index == 0) {
                    highlightedButton = buttons[buttons.Count - 1];
                } else {
                    highlightedButton = buttons[index - 1];
                }

                if (highlightedButton.interactable)
                {
                    selected = true;
                    buttonHandlers[highlightedButton].Highlight();
                }
                
            } else if (input.y < 0) {
                buttonHandlers[highlightedButton].UnHighlight();
                int index = buttons.IndexOf(highlightedButton);

                if (index == buttons.Count - 1) {
                    highlightedButton = buttons[0];
                } else {
                    highlightedButton = buttons[index + 1];
                }

                if (highlightedButton.interactable)
                {
                    selected = true;
                    buttonHandlers[highlightedButton].Highlight();
                }
            }

            attemptCount++;
        }
    }

    private void Select() {
        if (highlightedButton == null) return;

        buttonHandlers[highlightedButton].UnHighlight();
        highlightedButton.onClick.Invoke();
    }

    public void AddButtons(List<Button> newButtons) {
        foreach (Button button in newButtons) {
            AddButton(button);
        }
    }

    public void AddButton(Button newButton) {
        buttons.Add(newButton);
        buttonHandlers.Add(newButton, newButton.GetComponent<ButtonHandler>());

        if (highlightedButton == null) {
            highlightedButton = newButton;
            buttonHandlers[highlightedButton].Highlight();
        }
    }

    public void ClearButtons() {
        buttons.Clear();
        buttonHandlers.Clear();
        highlightedButton = null;
    }
}