using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : BaseSkill
{
    public override void Activate()
    {
        base.Activate();

        GameManager.Instance.Player.GetComponent<Animator>().SetTrigger("Bomb");

        StartCoroutine(Bomb());
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(i);
        }

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(i);
        }
    }
}
