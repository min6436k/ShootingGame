using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] Enemys;
    public GameObject[] Boss;

    public GameObject Meteor;
    public Transform[] SpawnPos;

    public float EnemySpawnCoolTime = 2;
    public float MeteorSpawnCoolTime = 2;

    public float BossSpawnCount = 15;
    public float CurrentSpawnCount = 0;

    public bool bCanSpawn = true;
    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(MeteorSpawn());
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(2);


        while (bCanSpawn)
        {
            float SpawnAmp = Random.Range(-EnemySpawnCoolTime * 0.3f, EnemySpawnCoolTime * 0.3f);
            float spawnX = Random.Range(SpawnPos[0].position.x, SpawnPos[1].position.x);
            int enemyIndex = Random.Range(0,Enemys.Length);

            yield return new WaitForSeconds(EnemySpawnCoolTime + SpawnAmp);

            Instantiate(Enemys[enemyIndex], new Vector3(spawnX, SpawnPos[0].position.y, 0), Quaternion.identity);

            CurrentSpawnCount++;

            if (CurrentSpawnCount >= BossSpawnCount)
            {
                bCanSpawn = false;

                yield return new WaitForSeconds(EnemySpawnCoolTime + SpawnAmp);

                Instantiate(Boss[GameInstance.Instance.StageLevel-1], new Vector3(0, 8.5f, 0), Quaternion.identity);
            }
        }
    }

    IEnumerator MeteorSpawn()
    {
        yield return new WaitForSeconds(3.5f);

        while (bCanSpawn)
        {
            float SpawnAmp = Random.Range(-MeteorSpawnCoolTime * 0.3f, MeteorSpawnCoolTime * 0.3f);
            float spawnX = Random.Range(SpawnPos[0].position.x, SpawnPos[1].position.x);

            yield return new WaitForSeconds(MeteorSpawnCoolTime + SpawnAmp);

            Instantiate(Meteor, new Vector3(spawnX, SpawnPos[0].position.y, 0), Quaternion.identity);
        }
    }
}
