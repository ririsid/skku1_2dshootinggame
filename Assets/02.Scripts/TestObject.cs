using System;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        // Debug.Log(collision.otherCollider.); 
        
        // 트리거에서 콜라이더를 구별하는 방법
        // ㄴ 1. 하위 오브젝트를 만들고 리지드바디를 추가함으로써 따로 구현한다.
        // ㄴ 2. GetComponent<구체 클래스>를 통해서 null 검사
        // ㄴ 3. 피직스 2d / 오버랩 방식
        
        // 
    }

    
}
