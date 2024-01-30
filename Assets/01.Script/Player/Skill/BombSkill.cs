using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : BaseSkill
{
    public override void Activate()
    {
        base.Activate();

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(i);
        }
    }
}
