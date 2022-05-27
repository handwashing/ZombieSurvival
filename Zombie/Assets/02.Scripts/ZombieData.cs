using UnityEngine;

//좀비 생성시 사용할 셋업 데이터
[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName = "Zombie Data")]
public class ZombieData : ScriptableObject
{
    public float health = 100f; //health
    public float damage = 20f; //공격력
    public float speed = 2f; //speed
    public Color skinColor = Color.white; //skin color
}
