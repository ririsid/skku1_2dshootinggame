using UnityEngine;

// 스크립터블 오브젝트는 주로 데이터를 저장하는 '데이터 컨테이너'다.
// 게임 오브젝트의 기능이나 트랜스폼 없이 단순히 데이터만 저장할 수 있다.
// 메모리 소모를 줄일 수 있다.
// ㄴ 공유된 데이터 개념이므로 이 방식은 '플라이웨이트' 패턴이라고 할 수 있다.
// 데이터를 모듈화 함으로써 테스트와 관리가 편리해진다.

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public EnemyType EnemyType;
    public float Speed;
    public int MaxHealth;
    public int Damage;
    public int Score;
}
