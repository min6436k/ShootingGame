using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject Player;

    public TextMeshProUGUI ClearTime;
    public TextMeshProUGUI Score;
    public GameObject ResultUI;

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
        if (Player != null)
        {
            PlayerCharacter = Player.GetComponent<PlayerCharacter>();
            PlayerHPSyetem = Player.GetComponent<PlayerHPSyetem>();
            PlayerFuelSyetem = Player.GetComponent<PlayerFuelSyetem>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && bStageCleared)
        {
            switch (GameInstance.Instance.StageLevel)
            {
                case 1:
                    SceneManager.LoadScene("Stage2");
                    break;
                case 2:
                    SceneManager.LoadScene("Result");
                    break;
            }
            GameInstance.Instance.StageLevel++;

        }

        if (Input.GetKeyUp(KeyCode.F1))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject i in enemies)
            {
                Enemy enemy = i.GetComponent<Enemy>();
                enemy.Dead();
            }
        }

        if (Input.GetKeyUp(KeyCode.F2))
        {
            GameInstance.Instance.WeaponLevel = 3;
        }

        if (Input.GetKeyUp(KeyCode.F3))
        {
            PlayerCharacter.InitCoolTime();
        }

        if (Input.GetKeyUp(KeyCode.F4))
        {
            PlayerHPSyetem.InitHP();
        }

        if (Input.GetKeyUp(KeyCode.F5))
        {
            PlayerFuelSyetem.Initfuel();
        }

        if (Input.GetKeyUp(KeyCode.F6))
        {
            StageClear();
        }
    }

    public void AddScore(int score)
    {
        GameInstance.Instance.Score += score; //게임 인스턴스의 스코어에 매개변수만큼 더하기
    }

    public void GameStart()
    {
        GameInstance.Instance = null;

        SceneManager.LoadScene("Stage1");
    }

    public void EnemyDies(int score)
    {
        AddScore(score);
    }

    public void StageClear()
    {
        AddScore(500);
        AddScore(100-Mathf.RoundToInt(Time.time - GameInstance.Instance.StartTime));
        AddScore(GameInstance.Instance.PlayerHP * 100);

        bStageCleared = true;
        ResultUI.SetActive(true);

        ClearTime.text = $"ClearTime : {(Time.time - GameInstance.Instance.StartTime).ToString("F1")}";
        Score.text = $"Score : {GameInstance.Instance.Score}";
    }
}
