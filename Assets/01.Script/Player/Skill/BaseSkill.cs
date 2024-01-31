using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public EnumTypes.PlayerSkill SkillTypes = 0;

    public float CoolTime;
    public float CurrentCoolTime;
    public bool bIsCoolTime;

    void Update()
    {
        if (CurrentCoolTime >= 0)
        {
            CurrentCoolTime -= Time.deltaTime;
        }
        else bIsCoolTime = false;
    }
    public void Use()
    {
        if (bIsCoolTime) return;
        Activate();
    }

    public virtual void Activate()
    {
        bIsCoolTime = true;
        CurrentCoolTime = CoolTime;
    }
}
