
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/GunData",fileName ="GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip; //발사 소리,
    public AudioClip relaodClip; //재장전 소리
                          
    public float damage = 25; //공격력

    public int startAmoRemain = 100; //처음에 주언진 전체 탄알
    public int magCapacity = 25;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;
       
        
}
