using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lesson_4_7.UIBehaviourours
{
    public class DefeatPanelBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(CheckoutToMainMenu());
        }

        private IEnumerator CheckoutToMainMenu()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("MenuScene");
        }
    }
}
