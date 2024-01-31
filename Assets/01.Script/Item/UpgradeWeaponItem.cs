using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeaponItem : BaseItem
{
    public override void OnGetItem()
    {
        base.OnGetItem();

        if (GameInstance.Instance.WeaponLevel < GameManager.Instance.PlayerCharacter.MaxWeaponLevel)
            GameInstance.Instance.WeaponLevel++;

        Destroy(gameObject);
    }
}
