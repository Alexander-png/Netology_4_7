using TMPro;
using UnityEngine;

namespace Lesson_4_7
{
    public class DebugAssistant : MonoBehaviour
    {
        private const bool C_LogEnabled = true;

        public static void WriteToLog(string message)
        {
#if UNITY_EDITOR
            if (C_LogEnabled)
            {
                Debug.Log(message);
            }
#endif
        }
    }
}
