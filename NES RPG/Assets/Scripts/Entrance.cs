using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour {
    public string newSceneName;
    public bool hostile = true;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            HostileWorldManager.Instance.hostile = hostile;
            
            SceneManager.LoadScene(newSceneName);
            PlayerMovement.Instance.transform.position = Vector2.zero;
        }
    }
}