using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public float ScrollSpeed;
    public Transform[] Backgrounds;

    private float _bottomPos;
    private float _imageHeight;

    private void Start()
    {
        _imageHeight = GetComponentInChildren<SpriteRenderer>().size.y;
        _bottomPos = -_imageHeight;
    }

    void Update()
    {
        ScrollingBackground();
    }

    void ScrollingBackground()
    {
        foreach(Transform i in Backgrounds)
        {
            if(i.position.y < _bottomPos)
            {
                i.position = new Vector3(0, Backgrounds.Max(x => x.position.y) + _imageHeight - 1);
            }

            i.position += new Vector3(0, -ScrollSpeed * Time.deltaTime);
        }

    }
}
