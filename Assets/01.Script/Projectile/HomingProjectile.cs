using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private float speed = 16f;
    private float rotateSpeed = 1000f;
    private bool IsTracking = true;

    public Transform target;

    private Rigidbody2D rb;
    private BoxCollider2D col;

    Rigidbody2D rigid;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        StartCoroutine(HitCooldown());
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            IsTracking = false;
            col.enabled = false;
            rb.angularVelocity = 0;
        }

        rb.velocity = transform.up * speed;

        if (!IsTracking) return;

        Vector3 dir = target.position - (Vector3)rb.position;

        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;

        if (rotateAmount == 0) rotateAmount = 1;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
    }


    IEnumerator SpeedUp()
    {
        while (speed >= 25f)
        {
            yield return new WaitForSeconds(0.1f);
            speed *= 1.1f;
            rotateSpeed += 250;
        }

    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
    }

}
