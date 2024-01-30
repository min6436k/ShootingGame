using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseItem : MonoBehaviour
{
    public EnumTypes.ItemName ItemName;
    protected void Update()
    {
        transform.Translate(new Vector3(0, -2, 0f)*Time.deltaTime);
    }
    public virtual void OnGetItem() { }
}

public class ItemManager : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();

    public void SpawnItem(EnumTypes.ItemName ItemName, Vector3 Pos)
    {
        GameObject targetItem = Items.Find(x => x.name == ItemName.ToString());

        Instantiate(targetItem, Pos, Quaternion.identity);
    }
}
