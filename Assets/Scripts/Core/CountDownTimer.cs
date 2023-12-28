using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] float maxTime = 100f;
        [SerializeField] CountDownUI countDownUI;
        float currentTime = 0f;

        private void Awake()
        {
            currentTime = maxTime;
        }
        private void Update()
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0f)
            {
                currentTime = 0f;
            }
            countDownUI.CurrentTime = currentTime;
        }
    }
}
