using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : BaseItem
{
    public override void OnGetItem()
    {
        base.OnGetItem();

        if (GameInstance.Instance.PlayerHP < GameManager.Instance.PlayerHPSyetem.MaxPlayerHP)
            GameInstance.Instance.PlayerHP++;

        Destroy(gameObject);
    }
}
