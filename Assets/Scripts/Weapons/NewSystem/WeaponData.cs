using UnityEngine;

namespace Weapons.NewSystem.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _force;
        [SerializeField] private AudioClip _shootSound;

        //[SerializeField] private GameObject _projectilePrefab;
        //[SerializeField] private GameObject _muzzleFlashPrefab;
        //[SerializeField] private GameObject _impactEffectPrefab;
        //[SerializeField] private GameObject _reloadEffectPrefab;
        //[SerializeField] private GameObject _emptyClipEffectPrefab;

        public GameObject WeaponPrefab => _weaponPrefab;
        public float Damage => _damage;
        public float FireRate => _fireRate;
        public float Force => _force;
        public AudioClip ShootSound => _shootSound;
        //public GameObject ProjectilePrefab => _projectilePrefab;

    }
}