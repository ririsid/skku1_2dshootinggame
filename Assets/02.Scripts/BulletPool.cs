using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // 오브젝트 풀 (총알 창고)
    // ㄴ 게임 오브젝트(총알 오브젝트)를 필요한 만큼 미리 생성해 두고 풀(창고)에 쌓아두는 기법이다.
    // ㄴ 오브젝트를 매번 생성하고 삭제하는 것보다 메모리 사용량과 성능 저하를 줄일 수 있다.
    // ㄴ 즉, 오버헤드를 줄인다.
    // ㄴ 컴퓨터 과학에 'Pool'은 사용할 때 획득한 메모리와 나중에 해제되는 메모리가 아닌
    //    미리 할당된 메모리 덩어리를 의미한다.
    
    // 목표: 총알 풀에 총알을 풀 사이즈 만큼 미리 생성해서 등록하고 싶다.
    // 속성
    // - 생성할 총알 프리팹
    public List<Bullet> BulletPrefabs;
  
    
    // - 풀 사이즈
    public int PoolSize = 20;
  
    // - 총알을 관리할 풀 리스트
    public List<Bullet> Bullets;

    // 싱글톤
    public static BulletPool Instance;
    
    // 기능
    // 1. 태어날 때
    private void Awake()
    {
        // ToDo: 하나임을 보장하는 코드로 변경
        Instance = this;
        
        // 2. 총알 풀을 총알을 담을 수 있는 크기로 만든다.
        int bulletPrefabCount = BulletPrefabs.Count;
        Bullets = new List<Bullet>(PoolSize * bulletPrefabCount);
        
        // 3. 프리팹 갯수만큼 반복해서
        foreach (Bullet bulletPrefab in BulletPrefabs)
        {
            // 4. 총알 프리팹으로부터 총알을 생성한다.
            for (int i = 0; i < PoolSize; ++i)
            {
                Bullet bullet = Instantiate(bulletPrefab);
                
                // 5. 생성할 총알을 풀에 추가한다.
                Bullets.Add(bullet);
                
                // 총알을 BulletPool 폴더에 집어넣는다.
                // (하위 구조)
                bullet.transform.SetParent(this.transform);
                
                // 6. 비활성화 한다.
                bullet.gameObject.SetActive(false);
            }
        }
        
    }
    
}
