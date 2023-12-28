using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] TextMeshPro text;
        [SerializeField] float maxTime = 100f;
        float currentTime = 0f;
        private void Update()
        {
            currentTime += Time.deltaTime;

        }
    }
}
