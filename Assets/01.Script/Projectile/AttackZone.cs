using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public float Duration = 1f;

    private SpriteRenderer sprite;
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(StartAndEnd());
    }

    IEnumerator StartAndEnd()
    {
        float time = 0;

        while (time <= 0.1f)
        {
            sprite.color = Color.Lerp(Color.clear, new Color(1, 0, 0, 0.3f), time*10);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(Duration-0.5f);

        float FlickCount = 0;

        while (FlickCount < 4)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.2f);

            yield return new WaitForSeconds(0.05f);

            GetComponent<SpriteRenderer>().color = Color.clear;

            yield return new WaitForSeconds(0.05f);

            FlickCount++;
        }

        Destroy(gameObject);
    }
}
