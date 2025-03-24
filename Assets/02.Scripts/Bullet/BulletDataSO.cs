using UnityEngine;

[CreateAssetMenu(fileName = "BulletDataSO", menuName = "Scriptable Objects/BulletDataSO")]
public class BulletDataSO : ScriptableObject
{
    public BulletType BulletType = BulletType.Main;
    public float Speed = 6;
    public int Damage = 100;
}
