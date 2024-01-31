using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AddOn : MonoBehaviour
{
    public GameObject Projectile;

    [HideInInspector]
    public Transform TargetTransform;
    private Transform TargetEnemyTransform;

    public int Speed = 20;
    public float AttackInterval = 0.5f;
    public float BulletSpeed = 16;

    private void Start()
    {
        StartCoroutine(ShootProjectile());
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetTransform.position, Speed * Time.deltaTime);
    }

    IEnumerator ShootProjectile()
    {
        while (true)
        {
            TargetEnemyTransform = SearchEnemy();

            GameObject instance = Instantiate(Projectile, transform.position, Quaternion.identity);
            instance.GetComponent<HomingProjectile>().target = TargetEnemyTransform;

            yield return new WaitForSeconds(AttackInterval);
        }
    }

    private Transform SearchEnemy()
    {
        float distance = float.MaxValue;
        Transform target = null;

        if (GameObject.FindGameObjectWithTag("Boss") != null) target = GameObject.FindGameObjectWithTag("Boss").transform;
        else
        {
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (distance > Vector3.Distance(i.transform.position, transform.position))
                {
                    target = i.transform;
                    distance = Vector3.Distance(i.transform.position, transform.position);
                }
            }
        }


        return target;
    }
}
