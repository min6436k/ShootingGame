using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 0;
    private float _lifeTime = 3;
    private Vector3 _direction;
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

}
