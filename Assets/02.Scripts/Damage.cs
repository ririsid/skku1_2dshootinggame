using UnityEngine;


public enum DamageType
{
    Bullet,
    Boom
}

// 대미지를 추상화..
public struct Damage       // 값 타입, 구조체: 다양한 데이터를 담기 위한 자료 구조 
{
    public int Value;
    public DamageType Type;
    public GameObject From; 
}