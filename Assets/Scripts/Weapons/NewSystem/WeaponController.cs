using System.Collections;
using StarterAssets;
using UnityEngine;
using Weapons.NewSystem.Data;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon; // Referencia al arma actual
        [SerializeField] private FirstPersonController playerController; // Referencia al controlador del jugador
        //[SerializeField] private Transform _spawnPosition; equivale a transform.position
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private bool _canShoot = true; // Variable para controlar si se puede disparar o no
        private int _currentAmmo; // Balas actuales en el cargador
        private int _remainingMags; // Cargadores restantes


        private void Start()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.SetWeaponData(_weaponData); // Asignar los datos del arma al objeto Weapon
                _currentAmmo = _weaponData.MaxAmmoPerMag; // Inicializar las balas actuales en el cargador
                _remainingMags = _weaponData.MaxMags; // Inicializar los cargadores restantes
            }
            else
            {
                Debug.LogError("No hay ninguna weapon asignada al WeaponController."); // Mensaje de error si no hay arma asignada
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0)) // Si se presiona el botón izquierdo del mouse
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
            if(!_canShoot) return; // Si no se puede disparar, salir de la función
            if(_currentWeapon == null) return; // Si no hay arma, salir de la función
            
            if (_currentAmmo <= 0)
            {
                Debug.Log("No ammo! Reload needed."); // Mensaje de error si no hay balas
                _currentWeapon.noAmmoSound(); // Reproducir sonido de gatillo vacío
                return; // Salir de la función si no hay balas
            }
            _currentAmmo--; // Disminuir la cantidad de balas actuales

            
            Vector3 shootDirection = playerController.GetPlayerDirection(_currentWeapon.FirePoint); // Obtener la dirección de disparo desde el weapon
            
            _currentWeapon.Shoot(shootDirection); // Disparar el proyectil desde el arma actual

            Debug.Log($"Shooting with damage: {_weaponData.Damage}, fire rate: {_weaponData.FireRate} | Ammo: {_currentAmmo}/{_weaponData.MaxAmmoPerMag} | Mags: {_remainingMags}"); // Mensaje de depuración con información del disparo

            _canShoot = false; // Desactivar la posibilidad de disparar inmediatamente
            StartCoroutine(WaitForNextShot()); // Iniciar la corrutina para esperar el tiempo entre disparos
        }

        public void Reload()
        {
            if (_remainingMags <= 0) return; // No hay más mags disponibles
            _remainingMags--; // Disminuir la cantidad de cargadores restantes
            _currentAmmo = _weaponData.MaxAmmoPerMag; // Recargar el cargador
            Debug.Log("Reloaded. Ammo: " + _currentAmmo + "/" + _weaponData.MaxAmmoPerMag + " | Mags:" + _remainingMags); // Mensaje de depuración al recargar
        }

        public void AddMagazines(int amount) //Esta funcion sirve para poder obtener cargadores extra de power ups
        {
            if (amount <= 0) return; // Si la cantidad es menor o igual a cero, no hacer nada
            if (_remainingMags >= _weaponData.MaxMags) return; // Si ya se tiene el máximo de cargadores, no hacer nada

            // Aumentar la cantidad de cargadores restantes, asegurando que no exceda el máximo
            _remainingMags = Mathf.Min(_remainingMags + amount, _weaponData.MaxMags);
            // Mensaje de depuración con la cantidad de cargadores restantes
            Debug.Log($"Added {amount} magazines. Remaining: {_remainingMags}/{_weaponData.MaxMags}"); 
        }
    }
}
