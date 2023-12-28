using System;
using TMPro;
using UnityEngine;
namespace Game.UI
{
    public class CountDownUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        float currentTime = 0f;
        public float CurrentTime { set => currentTime = value; }
        private void Update()
        {
            Debug.Log(currentTime);
            text.text = String.Format("{0:D2}:{1:D2}:{2:D2}",
            (int)(currentTime / 60),
            (int)(currentTime % 60),
            (int)(currentTime * 100) % 60);
        }

    }
}
