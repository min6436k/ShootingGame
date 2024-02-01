using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private float _speed = 16f;
    private float _rotateSpeed = 1000f;
    private bool _bisTracking = true;

    public Transform Target;

    private Rigidbody2D _rigid;
    private BoxCollider2D _collider;
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        StartCoroutine(HitCooldown());
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        if (Target == null)
        {
            _bisTracking = false;
            _collider.enabled = false;
            _rigid.angularVelocity = 0;
        }

        _rigid.velocity = transform.up * _speed;

        if (!_bisTracking) return;

        Vector3 dir = Target.position - (Vector3)_rigid.position;

        dir.Normalize();

        float rotate = Vector3.Cross(dir, transform.up).z;

        if (rotate == 0) rotate = 1;

        _rigid.angularVelocity = -rotate * _rotateSpeed;
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        _collider.enabled = true;
    }

}
