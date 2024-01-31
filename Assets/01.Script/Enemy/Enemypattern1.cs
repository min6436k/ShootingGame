using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern1 : MonoBehaviour,Freeze
{
    public float Speed;
    public float Amp;

    public bool bMoveRight = true;
    public Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }

    public void Freeze(bool freeze)
    {
        if (freeze)
        {
            gameObject.GetComponent<Enemy>().bIsFreeze = true;
        }
        else
        {
            gameObject.GetComponent<Enemy>().bIsFreeze = false;
        }
    }

    void Update()
    {
        if (gameObject.GetComponent<Enemy>().bIsFreeze) return;

        if (bMoveRight && transform.position.x - StartPosition.x < Amp)
            transform.position += new Vector3(1.2f, -1, 0) * Speed * Time.deltaTime;

        else if (!bMoveRight && transform.position.x - StartPosition.x > -Amp)
            transform.position += new Vector3(-1.2f, -1, 0) * Speed * Time.deltaTime;

        else bMoveRight = !bMoveRight;
    }
}
