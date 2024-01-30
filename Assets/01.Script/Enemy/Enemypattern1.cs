using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern1 : MonoBehaviour
{
    public float Speed;
    public float Amp;

    private bool _bmoveRight = true;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (_bmoveRight && transform.position.x - _startPosition.x < Amp)
            transform.position += new Vector3(1.2f, -1, 0) * Speed * Time.deltaTime;

        else if (!_bmoveRight && transform.position.x - _startPosition.x > -Amp)
            transform.position += new Vector3(-1.2f, -1, 0) * Speed * Time.deltaTime;

        else _bmoveRight = !_bmoveRight;
    }
}
