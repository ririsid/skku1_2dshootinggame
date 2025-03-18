using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public GameObject BulletPrefab;

    public Vector2 Position;

    public float MoveSpeed = 3f;

    public float ShootCooltime = 1f;

    public float FollowInterval = 0.5f;

    public float FollowThreshold = 0.5f; // 따라가기 시작할 거리 임계값

    private bool _canShoot = true;

    private bool _readyToCheckPosition = true; // 위치 확인 가능 여부
    private bool _shouldMove = false; // 이동 여부 결정

    private GameObject _player;
    private Vector2 _targetPosition; // 목표 위치

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_canShoot)
        {
            Shoot();
        }

        if (_readyToCheckPosition)
        {
            CheckDistance();
        }

        if (_shouldMove)
        {
            Move();
        }
    }

    private void CheckDistance()
    {
        StartCoroutine(CheckDistanceCoroutine());
    }

    private void Move()
    {
        // 현재 위치에서 타겟 위치로 이동
        transform.position = Vector2.MoveTowards(
            transform.position,
            _targetPosition,
            MoveSpeed * Time.deltaTime
        );

        // 목표 위치에 도달하면 이동 중지
        if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _shouldMove = false;
        }
    }

    private void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator CheckDistanceCoroutine()
    {
        _readyToCheckPosition = false;

        // 플레이어와의 거리 계산
        Vector2 playerPos = (Vector2)_player.transform.position + Position;
        float distance = Vector2.Distance(transform.position, playerPos);

        // 거리가 임계값보다 크면 따라가기 시작
        if (distance > FollowThreshold)
        {
            _targetPosition = playerPos;
            _shouldMove = true;
        }

        // 지정된 간격만큼 대기
        yield return new WaitForSeconds(FollowInterval);
        _readyToCheckPosition = true;
    }

    private IEnumerator ShootCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(ShootCooltime);

        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;
        _canShoot = true;
    }
}
