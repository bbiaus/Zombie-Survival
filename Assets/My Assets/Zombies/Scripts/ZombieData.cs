using UnityEngine;

[CreateAssetMenu(fileName = "NewZombieData", menuName = "ScriptableObjects/ZombieData", order = 1)]
public class ZombieData : ScriptableObject
{
    public string zombieName;
    public enum ZombieType { Normal, Rápido, Tanque }
    public ZombieType type;
    public int maxHealth = 100;
    public int damage = 10;
    public float speed = 3.5f;
    public float detectionRange = 15f;
    public float soundInterval = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    public AudioClip idleSound;
    public AudioClip chaseSound;
    public AudioClip attackSound;
    public AudioClip[] randomSounds;
}
