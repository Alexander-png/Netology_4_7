using UnityEngine;

namespace Lesson_4_7.UIBehaviourours
{
    public class PanelContainer : MonoBehaviour
    {
        [SerializeField]
        private VictoryPanelBehaviour _victoryPanel;
        [SerializeField]
        private DefeatPanelBehaviour _defeatPanel;

        public VictoryPanelBehaviour VictoryPanel => _victoryPanel;
        public DefeatPanelBehaviour DefeatPanel => _defeatPanel;
    }
}
