using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    private float _speed = 0;
    private float _lifeTime = 3;
    private Vector3 _direction;
    public bool bHomingSpawn;
    public GameObject HomingProjectile;
    public Sprite Level4Sprite;

    private void Start()
    {
        if (bHomingSpawn) GetComponent<SpriteRenderer>().sprite = Level4Sprite;
    }

    public void SetBullet(float Speed, Vector3 Direction, float LifeTime = 3)
    {
        _speed = Speed;
        _lifeTime = LifeTime;
        _direction = Direction;

        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bHomingSpawn && (collision.CompareTag("Enemy")|| collision.CompareTag("Boss")))
        {
            for (int i = -1; i < 2; i++)
            {
                GameObject instance = Instantiate(HomingProjectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 80 * i)));
                instance.GetComponent<HomingProjectile>().target = collision.transform;
            }
        }

        if (this.CompareTag("Shield") && collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
