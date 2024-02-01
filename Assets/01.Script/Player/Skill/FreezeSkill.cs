using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : BaseSkill
{
    public float FreezeTime = 3;
    public override void Activate()
    {
        base.Activate();

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            StartCoroutine(Freeze(i));
        }
    }

    IEnumerator Freeze(GameObject enemy)
    {
        enemy.GetComponent<Freeze>().Freeze(true);
        enemy.GetComponent<SpriteRenderer>().color = enemy.GetComponent<Enemy>().StartColor - new Color(0.5f,0,0,0);

        yield return new WaitForSeconds(FreezeTime);

        enemy.GetComponent<Freeze>().Freeze(false);
        enemy.GetComponent<SpriteRenderer>().color = enemy.GetComponent<Enemy>().StartColor;

    }
}

interface Freeze
{
    public void Freeze(bool freeze);
}
