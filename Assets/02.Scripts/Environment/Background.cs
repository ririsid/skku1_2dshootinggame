using UnityEngine;

public class Background : MonoBehaviour
{
    // 배경 스크롤링: 배경 이미지를 일정한 속도로 움직여 캐리턱타 몬스터 등의 움직임을 더 동적으로 만들어주는 기술
    //           ㄴ 캐릭터는 그대로 두고 배경만 움직이는 '눈속임'
    
    // 필요 속성
    // - 머터리얼
    public SpriteRenderer MySpriteRenderer;
    private Material _material;
    private MaterialPropertyBlock _materialPropertyBlock;
    private Vector2 _currentOffset = Vector2.zero;
    
    public float ScrollSpeed = 1f;

    private void Awake()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    
    void Update()
    {
        // 방향을 구하고,
        Vector2 direction = Vector2.up;

        // 방향으로 스크롤링 한다.
        _currentOffset += new Vector2(0, ScrollSpeed * Time.deltaTime);
        MySpriteRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetVector("_MainTex_ST", new Vector4(1, 1, _currentOffset.x, _currentOffset.y));
        MySpriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
