using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : BaseSkill
{
    public override void Activate()
    {
        base.Activate();

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            StartCoroutine(FreezeTime(i.GetComponent<Freeze>()));
        }
    }

    IEnumerator FreezeTime(Freeze enemy)
    {
        enemy.Freeze(true);

        yield return new WaitForSeconds(3);

        enemy.Freeze(false);

    }
}

interface Freeze
{
    public void Freeze(bool freeze);
}
