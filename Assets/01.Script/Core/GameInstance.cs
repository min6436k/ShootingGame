using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance;

    public int PlayerHP = 3;
    public float PlayerFuel = 100;
    public int WeaponLevel = 0;

    public int StageLevel = 1;
    public float StartTime;
    public int Score = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        StartTime = Time.time;
    }
}
