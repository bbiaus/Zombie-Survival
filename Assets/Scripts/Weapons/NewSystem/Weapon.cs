using UnityEngine;
using Weapons.NewSystem.Data;
using StarterAssets;
using System.Collections;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint; // Punto de aparición de la bala
        [SerializeField] private AudioSource _shootSound; // Sonido del disparo
        [SerializeField] private AudioSource _outOfAmmoAudioSource; // Sonido de gatillo vacío
        [SerializeField] private AudioSource _reloadSound; // Sonido de recarga
        [SerializeField] private ParticleSystem _muzzleFlash; // Efecto de destello al disparar
        [SerializeField] private Animator weaponAnimator; // Animator del arma

        private WeaponData _weaponData;

        


        public Transform FirePoint => _firePoint; //get de firepoint

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

                // Instanciamos una copia del prefab del muzzleflash en la posición del fire point
                ParticleSystem flash = Instantiate(_muzzleFlash, _firePoint.position, _firePoint.rotation, _firePoint);


                // se le aplica una rotación aleatoria en Z para que se vea distinto
                float random = Random.Range(0f, 360f);
                flash.transform.Rotate(0f, 0f, random);

                bullet.Shoot(_firePoint.position, Quaternion.LookRotation(shootDirection));
                
                
                if (weaponAnimator != null)
                {
                    weaponAnimator.SetTrigger("shootTrigger"); //animacion de disparo
                }

                 // Activa el efecto de disparo
                if (_muzzleFlash != null)
                {
                    flash.Play(); // Reproducir el efecto de destello
                    // Se destruye después de un rato (ya que es una copia)
                    Destroy(flash.gameObject, 0.3f); // Destruye el efecto después de poco tiempo
                } 

                //// Activa el sonido de disparo
                if (_weaponData.ShootSound != null && _shootSound != null) _shootSound.PlayOneShot(_weaponData.ShootSound, 0.15f);

                
            }

        private bool _canPlayNoAmmoSound = true; // cooldown para el sonido de gatillo, (arma vacia)

        public void noAmmoSound()
        {
            if (_canPlayNoAmmoSound && _outOfAmmoAudioSource != null && _outOfAmmoAudioSource.clip != null)
            {
                _outOfAmmoAudioSource.PlayOneShot(_outOfAmmoAudioSource.clip, 0.5f);
                StartCoroutine(NoAmmoCooldownCoroutine());
            }
        }

        private IEnumerator NoAmmoCooldownCoroutine()
        {
            _canPlayNoAmmoSound = false;
            yield return new WaitForSeconds(0.2f); // cooldown
            _canPlayNoAmmoSound = true;
        }

        public void reloadAnim()
        {
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("reloadTrigger"); //animacion de recarga
                _reloadSound.PlayOneShot(_reloadSound.clip, 0.9f);
            }
        }

        
        
    }
}