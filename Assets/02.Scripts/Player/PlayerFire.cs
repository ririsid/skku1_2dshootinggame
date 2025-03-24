using System.Collections.Generic;
using UnityEngine;

public enum PlayMode
{
    Auto,
    Manual
}

public class PlayerFire : PlayerComponent
{
    // 목표: 총알을 만들어서 발사하고 싶다.
    // 필요 속성:
    // - 총구들
    public GameObject[] Muzzles;
    public GameObject[] SubMuzzles;

    // - 쿨타임 / 쿨타이머
    public float Cooltimer = 0f;

    private bool _isHold = false;

    public void HoldFire()
    {
        _isHold = true;
    }

    public void ResumeFire()
    {
        _isHold = false;
    }

    // 필요 기능:
    // - 발사하다.
    private void Update()
    {
        if (_isHold) return;

        Cooltimer -= Time.deltaTime;

        // 쿨타임이 아직 안됐으면 종료
        if (Cooltimer > 0)
        {
            return;
        }

        List<Bullet> pool = BulletPool.Instance.Bullets;
        // 자동 모드 이거나 "Fire1" 버튼이 입력되면..
        if (_player.PlayMode == PlayMode.Auto || Input.GetButtonDown("Fire1"))
        {
            foreach (GameObject muzzle in Muzzles)
            {
                // 기존: 새로 생성
                // GameObject bullet = Instantiate(BulletPrefab); // 인스턴스화

                // 개선: 풀 이용
                // 총알 풀에 있는 총알들 중에서
                // 비활성화 되어 있는 총알을
                // 발사 사킨다. (활성화 시킨다)
                foreach (Bullet bullet in pool)
                {
                    // 2. 내가 원하는 타입이고, 비활성화 되어 있다면
                    if (bullet.BulletType == BulletType.Main && bullet.gameObject.activeInHierarchy == false)
                    {
                        bullet.Initialize();

                        // 3. 위치를 총구로 옮기고
                        bullet.transform.position = muzzle.transform.position;

                        // 4. 발사한다. (활성화한다.)
                        bullet.gameObject.SetActive(true);

                        break;
                    }
                }
            }

            foreach (GameObject subMuzzle in SubMuzzles)
            {
                foreach (Bullet bullet in pool)
                {
                    // 2. 내가 원하는 타입이고, 비활성화 되어 있다면
                    if (bullet.BulletType == BulletType.Sub && bullet.gameObject.activeInHierarchy == false)
                    {

                        // 3. 위치를 총구로 옮기고
                        bullet.transform.position = subMuzzle.transform.position;

                        bullet.Initialize();

                        // 4. 발사한다. (활성화한다.)
                        bullet.gameObject.SetActive(true);

                        break;
                    }
                }
            }

            Cooltimer = _player.AttackCooltime;
        }
    }


}
