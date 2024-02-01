using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    #region Move
    public float MoveSpeed;
    private Vector3 _moveInput;
    private Animator _animator;
    #endregion

    #region Skills
    public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();
    public int MaxWeaponLevel = 4;
    public PlayerUI PlayerUI;
    #endregion

    #region invincibility
    private bool _invincibility = false;
    public bool GetInvincibility => _invincibility;
    private Coroutine _invinCoroutine;
    #endregion

    #region AddOn
    public int MaxAddOnCount = 2;
    public Transform[] AddOnTransform;
    public GameObject AddOnPrefap;
    #endregion

    public GameObject Explode;

    void Start()
    {
        InitializeSkills();

        _animator = GetComponent<Animator>();

        for (int i = 0; i < GameInstance.Instance.AddOnCount; i++)
        {
            AddOnItem.SpawnAddOn(AddOnPrefap, AddOnTransform[i], false);
        }
    }
    void Update()
    {
        UpdateMovement();
        UpdateSkillInput();

        if (GameInstance.Instance.PlayerHP <= 0 || GameInstance.Instance.PlayerFuel <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    private void UpdateMovement()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");

        transform.position += _moveInput * MoveSpeed * Time.deltaTime;

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        position.x = Mathf.Clamp01(position.x);
        position.y = Mathf.Clamp01(position.y);


        if (_moveInput.x == 0) _animator.SetInteger("Move", 0);
        else _animator.SetInteger("Move", _moveInput.x < 0 ? -1 : 1);

        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private void UpdateSkillInput()
    {
        if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary);
        if (Input.GetKeyDown(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair);
        if (Input.GetKeyDown(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb);
        if (Input.GetKeyDown(KeyCode.V)) ActivateSkill(EnumTypes.PlayerSkill.Freeze);
        if (Input.GetKeyDown(KeyCode.B)) ActivateSkill(EnumTypes.PlayerSkill.BulletShield);
    }

    private void InitializeSkills()
    {
        foreach (BaseSkill i in transform.GetChild(0).GetComponents<BaseSkill>())
        {
            Skills.Add(i.SkillTypes, i);
        }
    }

    private void ActivateSkill(EnumTypes.PlayerSkill skillType)
    {

        if (Skills[skillType].bIsCoolTime == false)
        {
            Skills[skillType].Use();
        }
        else
        {
            if (skillType != EnumTypes.PlayerSkill.Primary)
                PlayerUI.CoolNoticeCoolTime(skillType);
        }
    }

    public void SetInvincibility(float time)
    {
        if (_invinCoroutine != null) StopCoroutine(_invinCoroutine);

        _invinCoroutine = StartCoroutine(InvincibilityCoroutine(time));
    }

    public void InitCoolTime()
    {
        foreach (BaseSkill i in Skills.Values)
        {
            i.CurrentCoolTime = 0;
        }
    }

    IEnumerator Dead()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Main");
    }

    IEnumerator InvincibilityCoroutine(float time)
    {
        _invincibility = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(time);

        GetComponent<SpriteRenderer>().color = Color.white;
        _invincibility = false;
    }


}
