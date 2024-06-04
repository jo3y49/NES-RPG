using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public Button resumeButton, saveButton, quitButton;

    private void Awake() {
        resumeButton.onClick.AddListener(Resume);
        saveButton.onClick.AddListener(Save);
        quitButton.onClick.AddListener(Quit);
    }

    public void Resume() {
        MenuManager.Instance.ToggleMenu(gameObject);
    }

    public void Save() {
        GameDataManager gd = GameDataManager.Instance;
        gd.SetCurrentScene(SceneManager.GetActiveScene().buildIndex);
        gd.SetPlayerPosition(PlayerMovement.Instance.transform.position);
        PlayerMovement.Instance.GetComponent<PlayerCombat>().EndBattle();

        gd.SaveGameData();
    }

    public void Quit() {
        GameDataManager.Instance.QuitGame();
    }

    public void CanSave(bool b) {
        saveButton.interactable = b;

        if (b)
        {
            saveButton.GetComponentInChildren<TextMeshProUGUI>().text = "Save";
        }
        else
        {
            saveButton.GetComponentInChildren<TextMeshProUGUI>().text = "Can't Save Here";
        }
    }
}