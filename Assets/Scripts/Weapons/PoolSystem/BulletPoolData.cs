using UnityEngine;

[CreateAssetMenu(fileName = "BulletPoolData", menuName = "Weapon/Bullet Pool Data")]
public class BulletPoolData : ScriptableObject
{
    public GameObject bulletPrefab;
    public int poolSize = 20;
}