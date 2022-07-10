using Lesson_4_7.GameManagers.Base;
using UnityEngine;

namespace Lesson_4_7.UIBehaviourours
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        private GameObserver _observer;

        private void Start()
        {
            _observer = FindObjectOfType<GameObserver>();
        }

        public void OnCreateRoomClicked()
        {
            _observer.OnCreateRoomPressed();
        }

        public void OnJoinRoomClicked()
        {
            _observer.OnJoinRoomPressed();
        }

        public void OnExitClick() 
        {
            _observer.OnQuitGamePressed();
        }
    }
}
