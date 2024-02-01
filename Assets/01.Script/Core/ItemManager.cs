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

    public void SpawnItem(EnumTypes.ItemName Name, Vector3 Pos)
    {
        GameObject targetItem = Items.Find(x => x.name == (Name.ToString()+"Item"));

        targetItem = Instantiate(targetItem, Pos, Quaternion.identity);

        targetItem.GetComponent<Animator>().SetInteger("ItemNum", (int)Name);
    }

    public void SpawnRandomItem(int min, int max, Vector3 position)
    {
        if (Random.Range(0, 3) == 0)
        {
            SpawnItem(EnumTypes.ItemName.Refuel, position);
            return;
        }

        if (Random.Range(min, max) == min)
        {
            SpawnRandomItem(position);
        }
    }

    public void SpawnRandomItem(Vector3 position)
    {
        int randomindex = Random.Range(0, (int)EnumTypes.ItemName.Last);
        EnumTypes.ItemName name = (EnumTypes.ItemName)randomindex;
        SpawnItem(name, position);
    }
}
