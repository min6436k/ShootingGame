using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject Player;

    [HideInInspector]
    public PlayerCharacter PlayerCharacter;
    [HideInInspector]
    public PlayerHPSyetem PlayerHPSyetem;
    [HideInInspector]
    public PlayerFuelSyetem PlayerFuelSyetem;

    public bool bStageCleared = false;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else Destroy(this.gameObject);
    }

    void Start()
    {
        PlayerCharacter = Player.GetComponent<PlayerCharacter>();
        PlayerHPSyetem = Player.GetComponent<PlayerHPSyetem>();
        PlayerFuelSyetem = Player.GetComponent<PlayerFuelSyetem>();
    }
}
