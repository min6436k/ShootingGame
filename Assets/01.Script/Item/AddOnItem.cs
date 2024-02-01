using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnItem : BaseItem
{
    public GameObject AddOnPrefap;

    public override void OnGetItem()
    {
        base.OnGetItem();

        if(GameInstance.Instance.AddOnCount < 2)
            SpawnAddOn(AddOnPrefap, GameManager.Instance.PlayerCharacter.AddOnTransform[GameInstance.Instance.AddOnCount]);

        Destroy(gameObject);
    }

    public static void SpawnAddOn(GameObject prefab, Transform addOnTargetTransform, bool PlusAddOnCount = true)
    {
        GameObject instance = Instantiate(prefab, GameManager.Instance.Player.transform.position,Quaternion.identity);

        instance.GetComponent<AddOn>().FollowPos = addOnTargetTransform;

        if (PlusAddOnCount) GameInstance.Instance.AddOnCount++;
    }
}
