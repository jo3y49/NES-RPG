using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance { get; private set; }
    private InputActions actions;

    public GameObject pauseMenu;
    private List<GameObject> menus;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            actions = new InputActions();

            menus = new List<GameObject> { pauseMenu };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Time.timeScale = 1;

        CloseMenus();
        CombatManager.Instance.gameObject.SetActive(false);

        StartCoroutine(CountPlaytime());
    }

    private void OnEnable() {
        ActivateMenuControls();
    }

    private void OnDisable() {
        DeactivateMenuControls();
    }

    public void ToggleMenu(GameObject menu)
    {
        bool active = menu.activeSelf;
        foreach (GameObject m in menus)
        {
            if (m != menu)
            {
                m.SetActive(false);
            }
        }

        Time.timeScale = active ? 1 : 0;
        PlayerMovement.Instance.ToggleActive(active);
        menu.SetActive(!active);
    }

    private void CloseMenus()
    {
        foreach (GameObject m in menus)
        {
            m.SetActive(false);
        }
    }

    public void StartCombat()
    {
        GameDataManager.Instance.SetLastPositionAndScene(PlayerMovement.Instance.transform.position, SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Combat");
        PlayerMovement.Instance.ToggleActive(false);
        DeactivateMenuControls();
    }

    public void EndCombat()
    {
        (Vector3, int) lastPositionAndScene = GameDataManager.Instance.GetLastPositionAndScene();
        SceneManager.LoadScene(lastPositionAndScene.Item2);
        PlayerMovement.Instance.transform.position = lastPositionAndScene.Item1;
        PlayerMovement.Instance.ToggleActive(true);
        HostileWorldManager.Instance.EndCombat();
        ActivateMenuControls();
    }

    private IEnumerator CountPlaytime()
    {
        float previousTime = Time.time;

        while (true)
        {
            if (pauseMenu.activeSelf == false)
            {
                float deltaTime = Time.time - previousTime;
                GameDataManager.Instance.AddPlaytime(deltaTime);
            }

            previousTime = Time.time;

            yield return null;
        }
    }

    private void ActivateMenuControls()
    {
        actions.Menu.Enable();
        actions.Menu.Pause.performed += context => ToggleMenu(pauseMenu);
    }

    private void DeactivateMenuControls()
    {
        if (actions == null) return;
        actions.Menu.Pause.performed -= context => ToggleMenu(pauseMenu);
        actions.Menu.Disable();
    }
}