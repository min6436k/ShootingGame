using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossA : MonoBehaviour
{

    public GameObject Projectile;

    private PolygonCollider2D _collider;

    #region Pattern
    public int MaxPatternIndex;
    public int[] UsePatternCount;
    public int PatternIntervalCount;
    public float PatternIntervalTime = 2;
    #endregion

    #region SideMove
    public float SideMoveSpeed = 2;
    private bool _bmoving = false;
    private bool _moveRight = true;
    private float _moveDistance = 5;
    #endregion
    
    private Vector3 _startPos;
    private Vector3 _targetPos;

    private void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _collider.enabled = false;
        _startPos = transform.position;
        UsePatternCount = new int[MaxPatternIndex];
        StartCoroutine(MoveDownAndStartPattern());
    }


    // Update is called once per frame
    void Update()
    {
        if (_bmoving)
            MoveSideWay();
    }
    private void MoveSideWay()
    {
        if (_moveRight)
            transform.position += new Vector3(SideMoveSpeed, 0, 0) * Time.deltaTime;
        else
            transform.position -= new Vector3(SideMoveSpeed, 0, 0) * Time.deltaTime;

        if(Mathf.Abs(transform.position.x) >= _moveDistance)
            _moveRight = !_moveRight;
    }
    IEnumerator MoveDownAndStartPattern()
    {
        float downTIme = 0;
        _targetPos = new Vector3(0, 5, 0);
        while (downTIme <= 1.5f)
        {
            transform.position = Vector3.Lerp(_startPos, _targetPos, downTIme / 1.5f);
            downTIme += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        _collider.enabled = true;
        _bmoving = true;

        yield return new WaitForSeconds(0.5f);

        NextPattern();
    }

    public virtual void NextPattern()
    {
        //int CurrentPatternIndex = FindPattern();
        //UsePatternCount[CurrentPatternIndex] = PatternIntervalCount+1; 
        //이부분 FindPattern으로 옮길거임

        for (int i = 0; i < UsePatternCount.Length; i++)
        {
            if (UsePatternCount[i] != 0) UsePatternCount[i]--;
        }

        switch (CurrentPatternIndex)
        {
            case 0:
                StartCoroutine(Pattern1());
                break;
            case 1:
                StartCoroutine(Pattern2());
                break;
            case 2:
                StartCoroutine(Pattern3());
                break;
            case 3:
                StartCoroutine(Pattern4());
                break;
        }
    }

    public int FindPattern()
    {
        int index = Random.Range(0, MaxPatternIndex);

        if (UsePatternCount[index] == 0) return index;
        else return FindPattern();
    }

    public virtual IEnumerator Pattern1()
    {
        CircleShoot(12, 7);

        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public virtual IEnumerator Pattern2()
    {
        CircleShoot(24, 4);

        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public virtual IEnumerator Pattern3()
    {
        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public virtual IEnumerator Pattern4()
    {
        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public void CircleShoot(int BulletCount, float Speed)
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float x = Mathf.Cos(360 / BulletCount * i * Mathf.Deg2Rad);
            float y = Mathf.Sin(360 / BulletCount * i * Mathf.Deg2Rad);

            Vector3 dir = new Vector3(x, y, 0);

            ShootProjectile(transform.position, Speed, dir.normalized);
        }
    }

    public void ShootProjectile(Vector3 position, float speed, Vector3 direction, float Size = 1)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);

        instance.transform.localScale *= Size;

        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.SetBullet(speed, direction);
    }
}
