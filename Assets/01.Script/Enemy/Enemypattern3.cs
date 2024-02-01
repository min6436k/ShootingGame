using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern3 : EnemyPattern1
{
    public GameObject Projectile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "PlayerBullet" || collision.tag == "Shield") && gameObject.GetComponent<Enemy>().bIsFreeze == false)
        {
            GameObject instance = Instantiate(Projectile, transform.position, Quaternion.identity);
            instance.GetComponent<Projectile>().SetBullet(7, Vector3.down);
            bMoveRight = !bMoveRight;
            transform.position = new Vector3(Random.Range(StartPosition.x - Amp, StartPosition.x + Amp), transform.position.y+1, transform.position.z);
        }
    }
}
