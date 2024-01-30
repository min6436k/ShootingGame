using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float Speed;
    void Update()
    {
        transform.position -= new Vector3(0, Speed) * Time.deltaTime;
    }
}
