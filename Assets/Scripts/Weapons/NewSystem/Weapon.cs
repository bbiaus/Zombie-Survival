using UnityEngine;
using Weapons.NewSystem.Data;
using StarterAssets;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _firepoint; // Punto de aparición de la bala
        private WeaponData _weaponData;

        public Transform FirePoint => _firepoint; //get de firepoint

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SetWeaponData(WeaponData data)
        {
            //_firepoint = weaponData.WeaponPrefab.transform.Find("FirePoint"); // Encuentra el punto de disparo en el prefab del arma
            _weaponData = data; // Asigna los datos del arma
        }
        public void Shoot(Vector3 shootDirection)
            {
                Bullet bullet = BulletPool.Instance.GetBullet(); //saco una bala del pool
                if (bullet == null) return;

                

                bullet.Shoot(_firepoint.position, Quaternion.LookRotation(shootDirection));

                Debug.Log($"Shooting with damage: {_weaponData.Damage}, fire rate: {_weaponData.FireRate}, force: {_weaponData.Force}");
            }
    }
}