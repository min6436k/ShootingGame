using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossB : MonoBehaviour
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
        int CurrentPatternIndex = FindPattern();
        UsePatternCount[CurrentPatternIndex] = PatternIntervalCount + 1;

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
            case 4:
                StartCoroutine(Pattern5());
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
        int BulletCount = 36;
        float Speed = 4;
        float BulletIntervalTime = 0.05f;


        for (int i = 0; i < BulletCount; i++)
        {
            float x = Mathf.Cos(720 / BulletCount * i * Mathf.Deg2Rad);
            float y = Mathf.Sin(720 / BulletCount * i * Mathf.Deg2Rad);

            Vector3 dir = new Vector3(x, y, 0);

            ShootProjectile(transform.position, Speed, dir.normalized);

            yield return new WaitForSeconds(BulletIntervalTime);

        }

        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public virtual IEnumerator Pattern2()
    {
        int BulletCount = 12;
        float Speed = 7;

        for (int i = 0; i < BulletCount; i++)
        {
            float x = Mathf.Cos(360 / BulletCount * i * Mathf.Deg2Rad);
            float y = Mathf.Sin(360 / BulletCount * i * Mathf.Deg2Rad);

            Vector3 dir = new Vector3(x, y, 0);

            ShootProjectile(transform.position, Speed, dir.normalized);
        }

        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public virtual IEnumerator Pattern3()
    {
        int BulletCount = 5;
        float BulletIntervalTime = 0.8f;
        for (int i = 0; i < BulletCount; i++)
        {
            Vector3 dir = GameManager.Instance.Player.transform.position - transform.position;
            ShootProjectile(transform.position, 5.5f, dir.normalized);

            yield return new WaitForSeconds(BulletIntervalTime);
        }


        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    public virtual IEnumerator Pattern4()
    {
        int BulletCount = 15;
        float BulletInterval = 0.6f;

        for (int i = 0; i < BulletCount; i++)
        {
            Vector3 BulletGap =new Vector3(0.3f,0,0);

            ShootProjectile(transform.position -BulletGap, 16, Vector3.down);
            ShootProjectile(transform.position +BulletGap, 16, Vector3.down);

            yield return new WaitForSeconds(BulletInterval);
            if (BulletInterval > 0.05f) BulletInterval *= 0.6f;
        }

        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();
    }

    IEnumerator Pattern5()
    {
        float R = 8;
        float deg = 00;
        Vector3 originPosition = transform.position;

        _bmoving = false;

        float moveTime = 0;
        while (moveTime < 1f)
        {
            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originPosition, GameManager.Instance.Player.transform.position + new Vector3(0, 7, 0), moveTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Pattern5Bullet());

        while (deg < 720)
        {

            deg += Time.deltaTime * 300;

            var rad = Mathf.Deg2Rad * deg;
            var x = R * Mathf.Sin(rad);
            var y = R * Mathf.Cos(rad);
            transform.position = GameManager.Instance.Player.transform.position + new Vector3(x, y);

            yield return null;
        }

        yield return new WaitForSeconds(1);

        moveTime = 0;
        while (moveTime < 1f)
        {
            moveTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, originPosition, moveTime);
            yield return null;
        }

        _bmoving = true;

        yield return new WaitForSeconds(PatternIntervalTime);

        NextPattern();

    }

    IEnumerator Pattern5Bullet()
    {
        for (int i = 0; i < 24; i++)
        {
            Vector3 playerDirection = (GameManager.Instance.Player.transform.position - transform.position).normalized;
            ShootProjectile(transform.position, 7, playerDirection);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShootProjectile(Vector3 position, float speed, Vector3 direction, float Size = 1)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);

        instance.transform.localScale *= Size;

        Projectile projectile = instance.GetComponent<Projectile>();

        projectile.SetBullet(speed, direction);
    }

    private void OnDestroy()
    {
        GameManager.Instance.StageClear();
    }
}
