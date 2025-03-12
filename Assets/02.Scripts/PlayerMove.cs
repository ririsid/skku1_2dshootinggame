using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // MonoBehaviour: 여러 가지 이벤트 함수를 자동으로 호출해주는 기능
    // Component: 게임 오브젝트에 추가할 수 있는 여러 가지 기능

    // 최종 목표: 키보드 입력에 따라 플레이어를 이동시키고 싶다.

    public float Speed = 3f;

    public float MinX, MaxX;
    public float MinY, MaxY;


    void Update()
    {

        // 게임 오브젝트에 transform이라는 컴포넌트는 무조건 있으므로
        // transform에 쉽게 접근할 수 있도록 만들어놨다.

        // transform.Translate -> 이동하다라는 뜻으로 매개 변수로 '속도'을 받는다.
        // 속도: 방향 * 속력



        // 벡터   : 크기와 방향 
        // 왼쪽으로 100


        // 초당 3M(unit)만큼 위로 움직여라!
        // transform.Translate(Vector2.up * 3f * Time.deltaTime);
        // Vector2 d = Vector2.up;
        // Time.deltaTime: 프레임 간 시간 간격을 의미한다.
        // 4프레임은 Time.deltaTime: 1/4 = 0.25초     
        // 3*0.25 + 3 * 0.25 + 3 * 0.25 + 3 * 0.25 = 3


        // 2프레임은 Time.deltaTime: 1/2 = 0.5초
        // 3*0.5 + 3*0.5 = 3

        // 30프레임은 Time.deltaTime: 1/30 = 0.03초
        // 60프레임은 Time.deltaTime: 1/60 = 0.016초


        SpeedCheck();

        Move();
    }


    private void Move()
    {
        // 키보드, 마우스, 터치, 조이스틱 등 외부에서 들어오는
        // 입력 소스는 모오오두 'Input' 클래스를 통해 관리할 수 있다.
        //float h = Input.GetAxis("Horizontal"); // 수평 키 : -1f ~ 1f
        float h = Input.GetAxisRaw("Horizontal"); // 수평 키 : -1, 0, 1

        //float v = Input.GetAxis("Vertical");  // 수직 키: -1f ~ 1f
        float v = Input.GetAxisRaw("Vertical");  // 수직 키: -1, 0, 1

        // 방향 만들기
        Vector2 direction = new Vector2(h, v);
        // 벡터로부터 방향만 가져오는 것을: 정규화
        direction = direction.normalized;
        direction.Normalize();


        // transform.Translate(direction * Speed * Time.deltaTime);

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
