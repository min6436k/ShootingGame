using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 3f;

    public float AttackDamage = 1f;

    public int Score;

    public bool bMustSpawnItem = false;

    public bool bIsFreeze = false;

    private Color _startColor;

    public GameObject Explode;

    void Start()
    {
        _startColor = GetComponent<SpriteRenderer>().color;
        if (gameObject.CompareTag("Boss")) bMustSpawnItem = true;
    }

    void Update()
    {
    }

    public void Dead()
    {
        if (!bMustSpawnItem) GameManager.Instance.GetComponentInChildren<ItemManager>().SpawnRandomItem(0, 3, transform.position);
        else GameManager.Instance.GetComponentInChildren<ItemManager>().SpawnRandomItem(transform.position);

        GameManager.Instance.EnemyDies(Score);

        Instantiate(Explode,transform.position,Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            HP--;

            StartCoroutine(HitFlick());

            if(HP <= 0) Dead();
        }

        if(!gameObject.CompareTag("Boss") && collision.CompareTag("Player"))
        {
            GameInstance.Instance.PlayerHP--;
            Destroy(gameObject);
        }
    }
    IEnumerator HitFlick()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = _startColor;
    }
}