using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Core
{
    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] float maxTime = 100f;
        [SerializeField] float addTimeOnKill = 0.5f;
        [SerializeField] CountDownUI countDownUI;
        [SerializeField] AddTime addTimeUI;
        [SerializeField] Vector3 addTimeOffset = new Vector3(0, 50f, 0);
        float currentTime = 0f;
        public static CountDownTimer instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }


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

        public void AddTime(float addTime)
        {
            currentTime += addTime;
            // if (currentTime > maxTime) currentTime = maxTime;
            AddTime instance = Instantiate(addTimeUI, Vector3.zero, Quaternion.identity);
            instance.transform.SetParent(countDownUI.transform, false);


            instance.Initiate(addTime);
        }
    }
}
