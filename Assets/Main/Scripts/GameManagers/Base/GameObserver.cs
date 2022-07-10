using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lesson_4_7.GameManagers.Base
{
    public class GameObserver : PunBehaviour
    {
        private static GameObserver _instance;
        private bool _connectedToMaster = false;

        public void OnCreateRoomPressed()
        {
            if (_connectedToMaster)
            {
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 }, null);
            }
        }

        public void OnJoinRoomPressed()
        {
            if (_connectedToMaster)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public void OnQuitGamePressed()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif !UNITY_EDITOR && UNITY_STANDALONE_WIN
            Application.Quit();
#endif
        }

        public override void OnConnectedToMaster()
        {
            DebugAssistant.WriteToLog("Ready for connecting");
            _connectedToMaster = true;
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel("TestNetScene");
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("MenuScene");
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _instance = this;

            if (!PhotonNetwork.connected)
            {
#if UNITY_EDITOR
                PhotonNetwork.playerName = "1";
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
                PhotonNetwork.playerName = "2";
#endif

                PhotonNetwork.automaticallySyncScene = true;
                PhotonNetwork.gameVersion = "0.0.0.1";
                PhotonNetwork.ConnectUsingSettings(PhotonNetwork.gameVersion);
            }
        }
    }
}
