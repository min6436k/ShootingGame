using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBPattern6 : MonoBehaviour
{

    public int Index = 0;
    public GameObject BossBullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pattern());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator pattern()
    {
        float moveTime = 0;
        while (moveTime < 1)
        {
            transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.right * Index, moveTime);
            moveTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(Mathf.Abs(Index) == 1 ? 0.2f : 0.4f);


        for (int i = 0; i < 4; i++)
        {
            ShootProjectile(transform.position, 6, Vector3.down,1.1f);
            ShootProjectile(transform.position, 6, new Vector3(0.3f, -1, 0),1.1f);
            ShootProjectile(transform.position, 6, new Vector3(-0.3f, -1, 0), 1.1f);

            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(0.5f);


        for (int i = 0; i < 5; i++)
        {
            float fadeTime = 0;
            while (fadeTime < 1)
            {
                SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();

                Color startcolor = sprite.color;

                sprite.color = Color.Lerp(startcolor, Color.clear, fadeTime);
                fadeTime += Time.deltaTime / 1;
                yield return null;
            }
        }
    }

    public void ShootProjectile(Vector3 position, float speed, Vector3 direction, float Size = 1)
    {
        GameObject instance = Instantiate(BossBullet, position, Quaternion.identity);

        instance.transform.localScale *= Size;

        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.SetBullet(speed, direction);
    }
}
