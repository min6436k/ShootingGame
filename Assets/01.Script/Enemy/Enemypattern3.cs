using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern3 : MonoBehaviour
{
    public float Speed;

    public float StopTime;
    public float MoveTime;

    public GameObject Projectile;

    public float BulletSpeed;

    private bool _isAttack = false;

    void Start()
    {
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (_isAttack == false)
            transform.position -= new Vector3(0f, Speed * Time.deltaTime, 0f);
    }

    IEnumerator Attack()
    {
        while (true)
        {

        }
    }
    public void ShootProjectile(Vector3 position, float speed, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.SetBullet(speed, direction);
    }

}
