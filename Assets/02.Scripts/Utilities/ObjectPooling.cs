using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab; // 사용할 프리팹
    [SerializeField] private int poolSize; // 오브젝트 풀 사이즈
    private Queue<GameObject> objectPool = new Queue<GameObject>(); // 오브젝트를 담을 큐

    public static ObjectPooling Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePool(poolSize);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        GameObject newObj = Instantiate(objectPrefab, transform);
        newObj.SetActive(false);
        objectPool.Enqueue(newObj);
        return newObj;
    }

    public virtual GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else
        {
            return CreateObject();
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        objectPool.Enqueue(obj);
    }
}