using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 총알을 만들어서 발사하고 싶다.

    // 필요 속성:
    // - 총알 프리팹
    public GameObject BulletPrefab;
    // - 총구
    public GameObject Muzzle;

    
    // 필요 기능:
    // - 발사하다.
    private void Update()
    {
        // 1. 발사 버튼을 누르면
        if(Input.GetButtonDown("Fire1"))
        {
            // 2. 프리팹으로부터 총알을 만든다.
            GameObject bullet = Instantiate(BulletPrefab); // 인스턴스화
            // Instantiate는 게임오브젝트를 '복제'해서 씬에 새로 만들어 넣는다.

            // 3. 총알 위치를 총구의 위치로 지정해준다.
            bullet.transform.position = Muzzle.transform.position;
        }
    }


}
