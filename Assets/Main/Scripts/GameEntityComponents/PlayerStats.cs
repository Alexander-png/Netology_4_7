using Lesson_4_7.Assistance.Markers;
using Lesson_4_7.GameManagers.Base;
using Lesson_4_7.UIBehaviourours;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lesson_4_7.GameEntityComponents
{
    public class PlayerStats : MonoBehaviour
    {
        private IGameManager _gameManager;
        private bool _onVictory = false;
        private bool _onDefeat = false;

        [SerializeField]
        private float _maxVelocity;
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private PhotonView _photonView;
        [SerializeField]
        private PlayerMovement _thisMovement;
        [SerializeField]
        private Lifebar _thisLifebar;

        [SerializeField]
        private Canvas _victoryCanvasPrefab;
        [SerializeField]
        private Canvas _defeatCanvasPrefab;

        [SerializeField, Space(15)]
        private float _health;
        [SerializeField]
        private float _fireRate;
        [SerializeField, Space(15)]
        private GameObject _target;

        public float MaxVelocity => _maxVelocity;
        public float Acceleration => _acceleration;
        public float Health
        { 
            get => _health;
            private set
            {
                _health = value;
                UpdateHP();
            }
        }

        public float FireRate => _fireRate;
        public IGameManager GameManager => _gameManager;

        public PhotonView PhotonView => _photonView;

        public GameObject Target
        { 
            get => _target; 
            set 
            {
                _target = value; 
            }  
        }

        public bool InputEnabled { get; private set; } = true;

        public void SetGameManager(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            _thisLifebar.SetMaxHealth(Health);
        }

        private void Update()
        {
            _thisLifebar.UpdateValue(Health);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ProjectileMovement proj))
            {
                Health -= proj.Damage;
                if (_health <= 0)
                {
                    OnDeathActions();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out KillZone _))
            {
                Health = 0;
                OnDeathActions();
            }
        }

        private void FixedUpdate()
        {
            if (!_onDefeat && !_onVictory && AreAllEnemiesDead())
            {
                _onVictory = true;
                if (_photonView.isMine)
                {
                    InputEnabled = false;
                    Instantiate(_victoryCanvasPrefab);
                }
            }
        }

        private void OnDeathActions()
        {
            DebugAssistant.WriteToLog($"{gameObject.name} is dead");
            if (_photonView.isMine && !_onDefeat && !_onVictory)
            {
                _onDefeat = true;
                InputEnabled = false;
                Instantiate(_defeatCanvasPrefab);
            }
        }

        public void OnQuit(InputValue input)
        {
            if (_photonView.isMine)
            {
                _gameManager.OnQuit();
            }
        }

        private bool AreAllEnemiesDead()
        {
            int aliveCounter = 0;
            if (_gameManager.Players.Count < 2)
            {
                return false;
            }

            foreach(PlayerStats enemy in _gameManager.Players)
            {
                if (enemy.Equals(this))
                {
                    continue;
                }
                if (enemy.Health > 0)
                {
                    aliveCounter++;
                }
            }
            return aliveCounter == 0;
        }

        private void UpdateHP()
        {
            _photonView.RPC("UpdateHealth", PhotonTargets.All, gameObject.name, Health);
        }

        [PunRPC]
        public void UpdateHealth(string targetPlayerName, float health)
        {
            if (gameObject.name == targetPlayerName)
            {
                _health = health;
                if (_health <= 0)
                {
                    OnDeathActions();
                }
            }
        }
    }
}
