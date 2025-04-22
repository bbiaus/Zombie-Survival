using System.Collections;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using Weapons.NewSystem.Data;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        private Weapon _currentWeapon; // Referencia al arma actual
        [SerializeField] private FirstPersonController playerController; // Referencia al controlador del jugador
        [SerializeField] private Transform _spawnPosition; //equivale a transform.position
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private bool _canShoot = true; // Variable para controlar si se puede disparar o no
        private int _currentAmmo; // Balas actuales en el cargador
        private int _remainingMags; // Cargadores restantes
        private bool _isReloading = false;
        public LayerMask hitLayers;
        public Transform shootOrigin; // El punto desde donde sale el disparo (ej: el cañón del arma)
        public float range = 100f;


        private void Start()
        {


            if (_currentWeapon != null)
            {
                //Invoke("EnableShooting", 2.0f); // Espera 2 segundos
                EquipWeapon(_weaponData);   // Asignar el arma al controlador de armas
                _currentAmmo = _weaponData.MaxAmmoPerMag;
                _remainingMags = _weaponData.MaxMags;
            }
            else
            {
                Debug.Log("No hay ninguna weapon asignada al WeaponController."); // Mensaje de error si no hay arma asignada
            }
        }
        private void Update()
        {
            Debug.DrawRay(shootOrigin.position, shootOrigin.forward * 100f, Color.green);
            if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot) // Si se presiona el botón izquierdo del mouse
            {
                Shoot(); // Llamar a la función de disparo
            }
            if (Input.GetKeyDown(KeyCode.R)) //Tecla R para recargar
            {
                
                Reload();
                
            }
        }
        private IEnumerator WaitForNextShot() //waitrof
        {
            yield return new WaitForSeconds(_weaponData.FireRate);  // Esperar el tiempo entre disparos
            _canShoot = true; // Permitir disparar nuevamente
        }
        public void Shoot()
        {
            if (!_canShoot || _isReloading) return; // Si no se puede disparar, salir de la función
            if(_currentWeapon == null) return; // Si no hay arma, salir de la función
            
            if (_currentAmmo <= 0)
            {
                Debug.Log("No ammo! Reload needed."); // Mensaje de error si no hay balas
                _currentWeapon.noAmmoSound(); // Reproducir sonido de gatillo vacío
                return; // Salir de la función si no hay balas
            }

            _currentAmmo--; // Disminuir la cantidad de balas actuales

            Ray ray = new Ray(shootOrigin.position, shootOrigin.forward);

            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);

            if (Physics.Raycast(ray, out hit, range, ~0))
            {
                if (hit.collider.CompareTag("Head"))
                {
                    Debug.Log("¡Disparo en la cabeza!");
                    hit.collider.GetComponent<ZombiePart>().TakeDamage(3); // o más daño
                }
                else if (hit.collider.CompareTag("Body"))
                {
                    Debug.Log("Disparo en el cuerpo.");
                    hit.collider.GetComponent<ZombiePart>().TakeDamage(1);
                }
                // Acá podrías instanciar el efecto de impacto también

            }

            Vector3 shootDirection = playerController.GetPlayerDirection(_currentWeapon.FirePoint); // Obtener la dirección de disparo desde el weapon

            _currentWeapon.Shoot(shootDirection); // Disparar el proyectil desde el arma actual

            Debug.Log($"Shooting with damage: {_weaponData.Damage}, fire rate: {_weaponData.FireRate} | Ammo: {_currentAmmo}/{_weaponData.MaxAmmoPerMag} | Mags: {_remainingMags}"); // Mensaje de depuración con información del disparo

            _canShoot = false; // Desactivar la posibilidad de disparar inmediatamente
            StartCoroutine(WaitForNextShot()); // Iniciar la corrutina para esperar el tiempo entre disparos
        }

        public void Reload()
        {
            if (_remainingMags <= 0 || _isReloading) return; // No hay más mags disponibles

            _isReloading = true; // Activar el estado de recarga
            _remainingMags--; // Disminuir la cantidad de cargadores restantes
            _currentAmmo = _weaponData.MaxAmmoPerMag; // Recargar el cargador

            _currentWeapon.reloadAnim(); // Reproducir la animación de recarga del arma
            _currentWeapon.noAmmoSound(); // Reproducir sonido de gatillo vacío (temporalmente)

            Debug.Log("Reloaded. Ammo: " + _currentAmmo + "/" + _weaponData.MaxAmmoPerMag + " | Mags:" + _remainingMags); // Mensaje de depuración al recargar
            
            StartCoroutine(FinishReloadAnimation()); // Esperar a que termine la animación
        }

        public void AddMagazines(int amount) //Esta funcion sirve para poder obtener cargadores extra de power ups
        {
            if (_currentWeapon == null) return; // Si no hay arma, salir de la función
            if (amount <= 0) return; // Si la cantidad es menor o igual a cero, no hacer nada
            if (_remainingMags >= _weaponData.MaxMags) return; // Si ya se tiene el máximo de cargadores, no hacer nada

            // Aumentar la cantidad de cargadores restantes, asegurando que no exceda el máximo
            _remainingMags = Mathf.Min(_remainingMags + amount, _weaponData.MaxMags);
            // Mensaje de depuración con la cantidad de cargadores restantes
            Debug.Log($"Added {amount} magazines. Remaining: {_remainingMags}/{_weaponData.MaxMags}");
        }

        private IEnumerator FinishReloadAnimation()
        {
            yield return new WaitForSeconds(_weaponData.ReloadTime); // Esto lo tomás del ScriptableObject

            _isReloading = false; // Ya se puede disparar
        }

        public void EquipWeapon(WeaponData weapon)
        {
            _weaponData = weapon;
            //Limpiar cualquier arma previa, manual o instanciada
            foreach (Transform child in _spawnPosition)
            {
                Destroy(child.gameObject);
                Debug.Log("ARMA VIEJA DESTRUIDA"); // Mensaje de depuración al destruir el arma anterior
            }

            if(weapon.WeaponPrefab != null)
            {
                _currentWeapon = Instantiate(weapon.WeaponPrefab, _spawnPosition.position, _spawnPosition.rotation, _spawnPosition);
                _currentWeapon.SetWeaponData(_weaponData);
                _currentAmmo = _weaponData.MaxAmmoPerMag;
                _remainingMags = _weaponData.MaxMags;

            }
            else
            {
                Debug.LogError("No hay ningun weapon asignada al WeaponController."); // Mensaje de error si no hay prefab de arma asignado
            }
            
        }

    }
}
