using UnityEngine;
using WeaponSystem;

namespace Weapons.NewSystem.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _fireRate;
        //[SerializeField] private float _force;
        [SerializeField] private AudioClip _shootSound;
        [SerializeField] private int _maxAmmoPerMag;
        [SerializeField] private int _maxMags;
        [SerializeField] private float reloadTime;



        //[SerializeField] private GameObject _projectilePrefab;
        //[SerializeField] private GameObject _muzzleFlashPrefab;
        //[SerializeField] private GameObject _impactEffectPrefab;
        //[SerializeField] private GameObject _reloadEffectPrefab;
        //[SerializeField] private GameObject _emptyClipEffectPrefab;

        public Weapon WeaponPrefab => _weaponPrefab;
        public float Damage => _damage;
        public float FireRate => _fireRate;
        //public float Force => _force;
        public AudioClip ShootSound => _shootSound;
        public int MaxAmmoPerMag => _maxAmmoPerMag;
        public int MaxMags => _maxMags;
        public float ReloadTime => reloadTime;
        //public GameObject ProjectilePrefab => _projectilePrefab;

    }
}