using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ButtonHandler : MonoBehaviour {
    private Button button;
    private Image image;
    private bool highlighted = false;
    private void Awake() {
        if (button == null) {
            TryGetComponent(out button);
        }
        if (image == null) {
            TryGetComponent(out image);
        }
        
        if (!highlighted) UnHighlight();
    }
    public void Highlight() {
        if (button == null) {
            TryGetComponent(out button);
        }
        if (image == null) {
            TryGetComponent(out image);
        }

        highlighted = true;
        image.color = Color.yellow;
    }

    public void UnHighlight() {
        highlighted = false;
        image.color = Color.white;
    }
}