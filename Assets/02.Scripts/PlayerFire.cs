using UnityEngine;

public enum PlayMode
{
    Auto,
    Mannual
}

public class PlayerFire : MonoBehaviour
{
    // 목표: 총알을 만들어서 발사하고 싶다.

    public Player MyPlayer;
    
    // 필요 속성:
    // - 총알 프리팹
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    // - 총구들
    public GameObject[] Muzzles;
    public GameObject[] SubMuzzles;

    // - 쿨타임 / 쿨타이머
    public float Cooltimer = 0f;




    
    // 필요 기능:
    // - 발사하다.
    private void Update()
    {
        Cooltimer -= Time.deltaTime;
        
        // 쿨타임이 아직 안됐으면 종료
        if(Cooltimer > 0 )
        {
            return;
        }

        // 자동 모드 이거나 "Fire1" 버튼이 입력되면..
        if (MyPlayer.PlayMode == PlayMode.Auto || Input.GetButtonDown("Fire1"))
        {
            foreach (GameObject muzzle in Muzzles)
            {
                GameObject bullet = Instantiate(BulletPrefab); // 인스턴스화

                bullet.transform.position = muzzle.transform.position;
            }

            foreach (GameObject subMuzzle in SubMuzzles)
            {
                GameObject subBullet = Instantiate(SubBulletPrefab); // 인스턴스화

                subBullet.transform.position = subMuzzle.transform.position;
            }

            Cooltimer = MyPlayer.AttackCooltime;
        }
    }


}
