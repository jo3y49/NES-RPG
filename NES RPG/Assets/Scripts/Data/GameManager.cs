using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour {
    protected GameData gameData;
    
    public virtual void SetGameData(GameData gameData)
    {
        this.gameData = gameData;
    }

    public virtual void SaveGameData()
    {
        SaveSystem.SaveGameData(gameData);
    }

    public virtual void DeleteSaveData()
    {
        // add a prompt to confirm deletion later
        SaveSystem.DeleteSaveData();
    }

    public virtual void QuitGame()
    {
        // destroy every object that is set to persist through scenes
        Destroy(PlayerMovement.Instance.gameObject);
        Destroy(MenuManager.Instance.gameObject);

        // add a prompt to save first later
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}