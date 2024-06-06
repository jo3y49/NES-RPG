using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHandler : MonoBehaviour {
    private Button button;
    private bool highlighted = false;
    private void Awake() {
        if (button == null) {
            TryGetComponent(out button);
        }
        
        if (!highlighted) UnHighlight();
    }
    public void Highlight() {
        if (button == null) {
            TryGetComponent(out button);
        }

        highlighted = true;
        Debug.Log("Highlighting " + button.name);
    }

    public void UnHighlight() {
        highlighted = false;
    }
}