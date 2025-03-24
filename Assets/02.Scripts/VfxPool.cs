using System.Collections.Generic;
using UnityEngine;

public class VfxPool : MonoBehaviour
{
    public List<ParticleSystem> VfxPrefabs;

    // - 풀 사이즈
    public int PoolSize = 50;

    public List<ParticleSystem> Vfx;

    // 싱글톤
    public static VfxPool Instance;

    // 기능
    // 1. 태어날 때
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 2. 총알 풀을 총알을 담을 수 있는 크기로 만든다.
        int vfxPrefabCount = VfxPrefabs.Count;
        Vfx = new List<ParticleSystem>(PoolSize * vfxPrefabCount);

        // 3. 풀 사이즈만큼 반복해서
        foreach (var vfxPrefab in VfxPrefabs)
        {
            for (int i = 0; i < PoolSize; i++)
            {
                ParticleSystem vfx = Instantiate(vfxPrefab);
                Vfx.Add(vfx);
                vfx.transform.SetParent(transform);
                vfx.gameObject.SetActive(false);
            }
        }
    }

    public ParticleSystem Create(string name, Vector3 position)
    {
        foreach (ParticleSystem vfx in Vfx)
        {
            if (vfx.name.StartsWith(name) && vfx.gameObject.activeInHierarchy == false)
            {
                vfx.transform.position = position;
                vfx.gameObject.SetActive(true);
                vfx.Clear();
                vfx.Play();
                return vfx;
            }
        }

        return null;
    }
}
