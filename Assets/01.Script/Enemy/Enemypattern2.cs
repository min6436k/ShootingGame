using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern2 : MonoBehaviour,Freeze
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

    public void Freeze(bool freeze)
    {
        if (freeze)
        {
            _isAttack = true;
            StopAllCoroutines();
        }
        else
        {
            _isAttack = false;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {

            yield return new WaitForSeconds(MoveTime);

            _isAttack = true;

            yield return new WaitForSeconds(StopTime * 0.2f);

            Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;
            ShootProjectile(transform.position, BulletSpeed, dir.normalized);

            yield return new WaitForSeconds(StopTime * 0.8f);

            _isAttack = false;


        }
    }
    public void ShootProjectile(Vector3 position, float speed, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.SetBullet(speed, direction);
    }

}
