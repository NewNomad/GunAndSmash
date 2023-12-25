using Game.Control;
using Game.Core;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;
namespace UI.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] ProgressBar progressBar;
        private void Start()
        {
            AddListeners();
        }

        void AddListeners()
        {
            PlayerController playerController = PlayerController.instance;
            playerController.GetComponent<Health>().OnHealthChanged.AddListener(OnHealthChanged);
        }

        void OnHealthChanged(int health, int maxHealth)
        {
            progressBar.currentPercent = (float)health / (float)maxHealth * 100;
        }
    }
}
