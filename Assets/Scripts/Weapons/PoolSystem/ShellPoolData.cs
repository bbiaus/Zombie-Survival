using UnityEngine;

[CreateAssetMenu(fileName = "NewShellPoolData", menuName = "Pool/ShellPoolData")]
public class ShellPoolData : ScriptableObject
{
    [SerializeField] private GameObject casingPrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private float deactivateTime = 4f;

    public GameObject CasingPrefab => casingPrefab;
    public int PoolSize => poolSize;
    public float DeactivateTime => deactivateTime;
}
