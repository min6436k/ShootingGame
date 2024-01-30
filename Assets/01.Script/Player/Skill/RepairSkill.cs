using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSkill : BaseSkill
{
    public override void Activate()
    {
        base.Activate();

        if (GameInstance.Instance.PlayerHP < GameManager.Instance.PlayerHPSyetem.MaxPlayerHP
        && GameInstance.Instance.PlayerFuel >= GameManager.Instance.PlayerFuelSyetem.MaxPlayerFuel * 0.4)
        {
            GameInstance.Instance.PlayerHP++;
            GameInstance.Instance.PlayerFuel -= GameManager.Instance.PlayerFuelSyetem.MaxPlayerFuel / 3;
        }
    }
}
