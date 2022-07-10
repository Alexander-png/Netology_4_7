using Lesson_4_7.GameManagers.Base;
using UnityEngine;

namespace Lesson_4_7.GameEntityComponents
{
    public class AimController : MonoBehaviour
    {
        private PlayerStats _playerStats;
        
        private void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
        }
        
        private void FixedUpdate()
        {
            if (!_playerStats.InputEnabled)
            {
                return;
            }
            AimLogic();
        }

        private void AimLogic()
        {
            FindNearestEnemy();
            if (_playerStats.Target != null)
            {
                transform.LookAt(_playerStats.Target.transform);
                transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
            }
        }

        private void FindNearestEnemy()
        {
            var players = GameManagerHolder.GameManagerInstance.Players;
            if (players.Count == 1)
            {
                return;
            }   

            GameObject nearestEnemy = null;
            float minimalDistance = -1f;
            foreach (PlayerStats player in players)
            {
                if (player.Equals(_playerStats))
                {
                    continue;
                }
                float sqrDistance = (player.transform.position - _playerStats.transform.position).sqrMagnitude;
                if (minimalDistance == -1f || minimalDistance > sqrDistance)
                {
                    nearestEnemy = player.gameObject;
                    minimalDistance = sqrDistance;
                }
            }
            _playerStats.Target = nearestEnemy;
        }
    }
}
