using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 3f;

    public float AttackDamage = 1f;

    public bool bMustSpawnItem = false;

    private bool _bIsDead = false;

    private Color _startColor;

    void Start()
    {
        _startColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HP--;

            StartCoroutine(HitFlick());

            if(HP <= 0) Dead();
        }
    }
    IEnumerator HitFlick()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = _startColor;
    }
}