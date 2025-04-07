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


        private void Start()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.SetWeaponData(_weaponData); // Asignar los datos del arma al objeto Weapon
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0)) // Si se presiona el botón izquierdo del ratón
            {
                Shoot(); // Llamar a la función de disparo
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
            Vector3 shootDirection = playerController.GetPlayerDirection(_currentWeapon.FirePoint); // Obtener la dirección de disparo desde el weapon
            _currentWeapon.Shoot(shootDirection); // Disparar el proyectil desde el arma actual

            Debug.Log($"Shooting with damage: {_weaponData.Damage}, fire rate: {_weaponData.FireRate}, force: {_weaponData.Force}");

            _canShoot = false; // Desactivar la posibilidad de disparar inmediatamente
            StartCoroutine(WaitForNextShot()); // Iniciar la corrutina para esperar el tiempo entre disparos
        }
    }
}
