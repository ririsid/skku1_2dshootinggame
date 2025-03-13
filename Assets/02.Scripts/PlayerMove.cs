using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // MonoBehaviour: 여러 가지 이벤트 함수를 자동으로 호출해주는 기능
    // Component: 게임 오브젝트에 추가할 수 있는 여러 가지 기능

    // 최종 목표: 키보드 입력에 따라 플레이어를 이동시키고 싶다.

    public Player MyPlayer;

    public float Speed = 3f;

    public float MinX, MaxX;
    public float MinY, MaxY;

    private GameObject _target = null;

    void Update()
    {
        SpeedCheck();

        if (MyPlayer.PlayMode == PlayMode.Auto)
        {
            AutoMove();
        }
        else
        {
            ManualMove();
        }
    }

    private void AutoMove()
    {
        // 가장 가까운 적을 찾아서 _target에 저장
        FindClosestTarget();
        
        // ToDo: _target을 이용해서 이동 코드 작성
    }

    // 가장 가까운 적을 찾는다.
    private void FindClosestTarget()
    {
        // 이미 타겟이 있으면 아무것도 안한다.
        if (_target != null) return;
        
        // 모든 적을 찾는다.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        // 가장 나와 거리가 짧은 적 찾기
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            // 거리가 저장한것보다 짧으면
            float targetDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (targetDistance < distance)
            {
                // 타겟을 갱신한다.
                distance = targetDistance;
                _target = enemy;
            }
        
        }
    }
    
    
    private void ManualMove()
    {
        float h = Input.GetAxisRaw("Horizontal"); // 수평 키 : -1, 0, 1
        float v = Input.GetAxisRaw("Vertical");  // 수직 키: -1, 0, 1

        Vector2 direction = new Vector2(h, v);
        direction = direction.normalized;
        direction.Normalize();



        // 1. 새로운 위치 = 현재 위치 + 방향 * 속력 * 시간
        Vector3 newPosition = transform.position + (Vector3)(direction * Speed) * Time.deltaTime;

        // 2. Math.Clamp(현재값, 최소값, 최대값)
        newPosition.y = Math.Clamp(newPosition.y, MinY, MaxY);

        // 3. 넘어가면 반대로
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        // 4. 위치 갱신
        transform.position = newPosition;
    }

    private void SpeedCheck()
    {
        // 5. 스피드 수정
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 매직넘버로 해도 되는 숫자: -1, 0, 1
            Speed += Math.Min(10, Speed + 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed = Math.Max(1, Speed - 1);
        }
    }
}
