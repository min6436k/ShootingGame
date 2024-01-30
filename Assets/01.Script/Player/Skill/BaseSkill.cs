using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public EnumTypes.PlayerSkill SkillTypes = 0;

    public float CoolTime;
    private float _currentCoolTime;
    private bool _bisCoolTime;

    void Update()
    {
        if (_currentCoolTime >= 0)
        {
            _currentCoolTime -= Time.deltaTime;
        }
        else _bisCoolTime = false;
    }
    public void Use()
    {
        if (_bisCoolTime) return;
        Activate();
    }

    public virtual void Activate()
    {
        _bisCoolTime = true;
        _currentCoolTime = CoolTime;
    }
}
