using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SavePoint : MonoBehaviour {
    private SpriteRenderer sr;

    private void Start() {
        TryGetComponent(out sr);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            MenuManager.Instance.SaveZone(true);
            sr.color = Color.white;
            HostileWorldManager.Instance.ToggleHostility(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            MenuManager.Instance.SaveZone(false);
            sr.color = Color.yellow;
            HostileWorldManager.Instance.ToggleHostility(true);
        }
    }
}