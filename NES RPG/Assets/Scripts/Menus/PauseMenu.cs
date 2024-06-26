using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public Button resumeButton, saveButton, controlsButton, quitButton;
    public GameObject controlsMenu, buttonContainer;
    public TextMeshProUGUI level, playtime;

    private void Awake() {
        resumeButton.onClick.AddListener(Resume);
        saveButton.onClick.AddListener(Save);
        controlsButton.onClick.AddListener(() => Controls(true));
        quitButton.onClick.AddListener(Quit);
    }

    private void OnEnable() {
        if (GameDataManager.Instance == null) return;
        
        level.text = "Level: " + GameDataManager.Instance.GetLevel();
        playtime.text = "Playtime: " + Utility.FormatTimeToString(GameDataManager.Instance.GetPlaytime());

        controlsMenu.SetActive(false);
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

    public void Controls(bool b) {
        controlsMenu.SetActive(b);
        buttonContainer.SetActive(!b);
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