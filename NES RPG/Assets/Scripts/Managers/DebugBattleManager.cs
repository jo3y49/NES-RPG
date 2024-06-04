using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugBattleManager : MonoBehaviour {
    public GameObject enemyButtonContainer;
    public TextMeshProUGUI mainText;
    public GameObject buttonPrefab;
    public List<EnemyData> enemies = new();
    public List<Button> enemyButtons = new();
    public EnemyData[] enemyDatas;

    private void Start() {
        enemyDatas = Resources.LoadAll<EnemyData>("");

        for(int i = 0; i < enemyDatas.Length; i++) {
            EnemyData enemyData = enemyDatas[i];
            Button enemyButton = Instantiate(buttonPrefab, enemyButtonContainer.transform).GetComponent<Button>();
            
            enemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemyData.name;
            enemyButton.onClick.AddListener(() => SelectEnemy(enemyData));
            enemyButtons.Add(enemyButton);
        }

        Button randomButton = Instantiate(buttonPrefab, enemyButtonContainer.transform).GetComponent<Button>();
        randomButton.GetComponentInChildren<TextMeshProUGUI>().text = "Random Enemy";
        randomButton.onClick.AddListener(RandomEnemy);
        enemyButtons.Add(randomButton);

        Button backButton = Instantiate(buttonPrefab, enemyButtonContainer.transform).GetComponent<Button>();
        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        backButton.onClick.AddListener(() => GameDataManager.Instance.QuitGame());

        enemyButtonContainer.SetActive(true);

        gameObject.SetActive(true);

        mainText.text = "Select who you want to fight";

        HostileWorldManager.Instance.hostile = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void SelectEnemy(EnemyData enemyData)
    {
        HostileWorldManager.Instance.StartCombat(enemyData);
        gameObject.SetActive(false);
        
    }
    public void RandomEnemy()
    {
        int randomEnemy = Random.Range(0, enemyDatas.Length);
        SelectEnemy(enemyDatas[randomEnemy]);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name or build index of the loaded scene
        if (scene.name == "DebugBattle")
        {
            gameObject.SetActive(true);
            HostileWorldManager.Instance.hostile = false;
            PlayerMovement.Instance.ToggleActive(false);
            MenuManager.Instance.DeactivateMenuControls();
        }
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}