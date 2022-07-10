using UnityEngine;
using UnityEngine.UI;

namespace Lesson_4_7.UIBehaviourours
{
    public class Lifebar : MonoBehaviour
    {
        [SerializeField]
        private Image _lifeBar;

        private float _maxUnitHealh;

        private void Update()
        {
            transform.parent.transform.LookAt(Camera.main.transform);
        }

        public void SetMaxHealth(float maxHealth)
        {
            _maxUnitHealh = maxHealth;
        }

        public void UpdateValue(float currentHealth)
        {
            _lifeBar.fillAmount = currentHealth / _maxUnitHealh;
        }
    }
}
