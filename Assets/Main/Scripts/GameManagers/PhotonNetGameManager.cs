using Lesson_4_7.Assistance.Markers;
using Lesson_4_7.GameEntityComponents;
using Lesson_4_7.GameManagers.Base;
using Photon;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson_4_7
{
    public class PhotonNetGameManager : PunBehaviour, IGameManager//, IPunObservable
    {
        private PlayerPool _playerPool;
        private PlayerStats _minePlayer;

        [SerializeField, Space(15)]
        private string _playerPrefabName;
        [SerializeField, Range(0, 15)]
        private float _spawnAreaRadius;

        [SerializeField]
        private Camera _camera;

        private List<PlayerStats> _players;
        public List<PlayerStats> Players
        {
            get
            {
                List<PlayerStats> toReturn = new List<PlayerStats>(_players);
                return toReturn;
            }
        }

        private void Awake()
        {
            GameManagerHolder.SetGameManager(this);
            _players = new List<PlayerStats>();
        }

        private void FixedUpdate()
        {
            // Doing this in fixed update is bad, i know.
            if (PhotonNetwork.playerList.Length != _players.Count)
            {
                PlayerStats[] playersOnMap = FindObjectsOfType<PlayerStats>();
                _players.Clear();
                foreach (PlayerStats player in playersOnMap)
                {
                    if (!_players.Contains(player))
                    {
                        AddPlayer(player.gameObject);
                    }
                }
                Debug.Log($"Players on map: {_players.Count}");
            }
        }

        private void Start()
        {
            _playerPool = FindObjectOfType<PlayerPool>();

            Vector2 randomPos = Random.insideUnitCircle;
            Vector3 spawnArea = new Vector3(randomPos.x * _spawnAreaRadius, 1.5f, randomPos.y * _spawnAreaRadius);

            GameObject player = Instantiate($"{_playerPrefabName}{PhotonNetwork.playerName}", _playerPool.transform, new Quaternion(), true);
            _camera.transform.parent = player.transform;
            _camera.transform.localPosition = new Vector3(0, 3, -5);
            _camera.transform.localRotation = Quaternion.Euler(10, 0, 0);
            player.transform.position += spawnArea;
            AddPlayer(player, true);
        }

        private void AddPlayer(GameObject toAdd, bool isMine = false)
        {
            PlayerStats stats = toAdd.GetComponent<PlayerStats>();
            if (isMine)
            {
                _minePlayer = stats;
            }
            stats.SetGameManager(this);
            _players.Add(stats);

            Debug.Log($"Players on map: {_players.Count}");
        }

        public GameObject Instantiate(GameObject prefab, Transform parent, Quaternion rotation, bool setParent = true)
        {
            GameObject obj = Instantiate(prefab.name, parent.position, rotation);
            if (setParent)
            {
                obj.transform.SetParent(parent);
            }
            return obj;
        }

        private GameObject Instantiate(string prefabName, Transform parent, Quaternion rotation, bool setParent = true)
        {
            GameObject obj = Instantiate(prefabName, parent.position, rotation);
            if (setParent)
            {
                obj.transform.SetParent(parent);
            }
            return obj;
        }

        private GameObject Instantiate(string objectName, Vector3 position, Quaternion rotation)
        {
            return PhotonNetwork.Instantiate(objectName, position, rotation, 0);
        }

        public void OnQuit()
        {
            _players.Remove(_minePlayer);
            Destroy(_minePlayer.gameObject);
            PhotonNetwork.LeaveRoom();
        }

        // It doesn't works
        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    DebugAssistant.WriteToLog("OnPhotonSerializeView");
        //    if (stream.isReading)
        //    {
        //        //stream.SendNext()
        //    }
        //    else
        //    {

        //    }

        //    // Todo:
        //    // livebar
        //    // effects
        //}
    }
}
