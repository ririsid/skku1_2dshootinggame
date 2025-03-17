using UnityEngine;

public class Background : MonoBehaviour
{
    // 배경 스크롤링: 배경 이미지를 일정한 속도로 움직여 캐리턱타 몬스터 등의 움직임을 더 동적으로 만들어주는 기술
    //           ㄴ 캐릭터는 그대로 두고 배경만 움직이는 '눈속임'
    
    // 필요 속성
    // - 머터리얼
    public SpriteRenderer MySpriteRenderer;
    private Material _material;
    
    // - 스크롤 속도
    public float ScrollSpeed = 1f;

    private void Awake()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        _material = MySpriteRenderer.material; // 원본 머터리얼의 복사본 (인스턴스)를 생성해서 반환
                                               // vs sharedMaterial
        MySpriteRenderer.material = _material;
    }
    
    void Update()
    {
        // 방향을 구하고,
        Vector2 direction = Vector2.up;

        // 방향으로 스크롤링 한다.
        _material.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
    }
}
