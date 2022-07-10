using Lesson_4_7.GameEntityComponents;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson_4_7.GameManagers.Base
{
    public interface IGameManager
    {
        public List<PlayerStats> Players { get; }
        public GameObject Instantiate(GameObject obj, Transform parent, Quaternion rotation, bool setParent);
        public void OnQuit();
    }

    public static class GameManagerHolder
    {
        public static IGameManager GameManagerInstance { get; private set; }

        public static void SetGameManager(IGameManager manager)
        {
            GameManagerInstance = manager;
        }
    }
}
