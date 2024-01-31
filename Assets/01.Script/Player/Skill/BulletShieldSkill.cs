using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShieldSkill : BaseSkill
{
    public int ShieldCount = 12;
    public GameObject Projectile;
    public float DurationTime = 4;
    public float RotationSpeed = 10;

    public override void Activate()
    {
        base.Activate();

        float angle = 360 / ShieldCount;
        for (int i = 0; i < ShieldCount; i++)
        {
            GameObject instance = Instantiate(Projectile, gameObject.transform);

            Vector3 temp = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.up * 0.8f;

            instance.transform.position = temp + transform.position;

            instance.transform.rotation = Quaternion.AngleAxis(angle * i - 90, Vector3.forward);

            Destroy(instance,DurationTime);
        }
    }
    private void FixedUpdate()
    {
        if (transform.childCount != 0) transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);
    }
}
