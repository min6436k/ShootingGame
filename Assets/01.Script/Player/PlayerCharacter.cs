using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    #region Move
    public float MoveSpeed;
    private Vector3 _moveInput;
    #endregion

    #region Skills
    public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();
    public int MaxWeaponLevel = 4;
    #endregion

    #region invincibility
    private bool _invincibility = false;
    public bool GetInvincibility => _invincibility;
    private Coroutine _invinCoroutine;
    #endregion

    void Start()
    {
        InitializeSkills();
    }
    void Update()
    {
        UpdateMovement();
        UpdateSkillInput();
    }

    private void UpdateMovement()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");

        transform.position += _moveInput * MoveSpeed * Time.deltaTime;

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        position.x = Mathf.Clamp01(position.x);
        position.y = Mathf.Clamp01(position.y);

        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private void UpdateSkillInput()
    {
        if(Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary);
        if(Input.GetKey(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair);
        if(Input.GetKey(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb);
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
        Skills[skillType].Use();
    }

    public void SetInvincibility(float time)
    {
        if(_invinCoroutine != null) StopCoroutine(_invinCoroutine);

        _invinCoroutine = StartCoroutine(InvincibilityCoroutine(time));
    }
    IEnumerator InvincibilityCoroutine(float time)
    {
        _invincibility = true;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);

        yield return new WaitForSeconds(time);

        GetComponent<SpriteRenderer>().color = Color.white;
        _invincibility = false;
    }


}
