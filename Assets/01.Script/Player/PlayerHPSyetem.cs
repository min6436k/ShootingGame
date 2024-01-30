using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHPSyetem : MonoBehaviour
{
    [HideInInspector]
    public int MaxPlayerHP = 3;
    IEnumerator HitFlick()
    {
        GameManager.Instance.PlayerCharacter.SetInvincibility(1);

        int i = 0;
        while (i < 5)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

            yield return new WaitForSeconds(0.1f);

            GetComponent<SpriteRenderer>().color = Color.white;

            yield return new WaitForSeconds(0.1f);

            i++;
        }
    }

    public void StopHitFlick()
    {
        StopCoroutine(HitFlick());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") && GameManager.Instance.PlayerCharacter.GetInvincibility == false)
        {
            StartCoroutine(HitFlick());
            GameInstance.Instance.PlayerHP--;
        }

        if (collision.CompareTag("Item"))
        {
            collision.GetComponent<BaseItem>().OnGetItem();
        }
    }
}
