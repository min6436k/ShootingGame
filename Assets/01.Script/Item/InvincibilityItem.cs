using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityItem : BaseItem
{
    public float InvinTime = 3;
    public override void OnGetItem()
    {
        base.OnGetItem();

        GameManager.Instance.PlayerHPSyetem.StopHitFlick();
        GameManager.Instance.PlayerCharacter.SetInvincibility(InvinTime);

        Destroy(gameObject);
    }
}
