using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    public static WorldManager Instance { get; protected set; }
    [HideInInspector]
    public GameObject player;
    public MenuManager menuManager;

    protected virtual void Awake()
    {
        Instance = this;
    }

    protected virtual void Start() {
        GameDataManager gd = GameDataManager.Instance;
        
        player = PlayerMovement.Instance.gameObject;
        player.transform.position = gd.GetPlayerPosition();
    }
}