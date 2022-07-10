using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lesson_4_7.GameEntityComponents
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform _projectileSpawnPoint;
        [SerializeField]
        private ProjectileMovement _projectilePrefab;

        private PlayerStats _playerStats;

        private bool _isShooting;
        private Coroutine _shootingCoroutine;

        private void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
        }

        private void OnShoot(InputValue input)
        {
            if (!_playerStats.PhotonView.isMine)
            {
                return;
            }

            _isShooting = input.isPressed;
            if (_shootingCoroutine == null && _playerStats.Target != null)
            {
                _shootingCoroutine = StartCoroutine(ShootCoroutine());
            }
        }

        private IEnumerator ShootCoroutine()
        {
            while (_isShooting && _playerStats.InputEnabled)
            {
                if (_playerStats.GameManager == null)
                {
                    DebugAssistant.WriteToLog("No game manager is attached to current game object!");
                    break;
                }

                _projectilePrefab.SetData(gameObject.name);

                _playerStats.GameManager.Instantiate(_projectilePrefab.gameObject, _projectileSpawnPoint, _projectileSpawnPoint.rotation, false);

                yield return new WaitForSeconds(60 / _playerStats.FireRate);
            }
            _shootingCoroutine = null;
        }
    }
}
