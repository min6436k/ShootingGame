using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelItem : BaseItem
{
    public override void OnGetItem()
    {
        base.OnGetItem();

        GameInstance.Instance.PlayerFuel += GameManager.Instance.PlayerFuelSyetem.MaxPlayerFuel / 3;

        Destroy(gameObject);
    }
}