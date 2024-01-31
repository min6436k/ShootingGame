using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern4 : MonoBehaviour, Freeze
{
    public float Speed;

    public float StopTime;
    public float MoveTime;

    public GameObject Projectile;
    public GameObject AttackZone;

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

            if (GameManager.Instance.Player.transform.position.y >= transform.position.y)
            {
                break;
            }

            yield return new WaitForSeconds(MoveTime);

            _isAttack = true;

            AttackZone instance = Instantiate(AttackZone, transform.position - new Vector3(0, 8, 0), Quaternion.identity).GetComponent<AttackZone>();
            instance.Duration = StopTime * 0.5f;

            yield return new WaitForSeconds(StopTime * 0.6f);

            ShootProjectile(transform.position, BulletSpeed, Vector3.down);

            yield return new WaitForSeconds(StopTime * 0.4f);

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
