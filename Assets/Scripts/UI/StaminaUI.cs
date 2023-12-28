using Michsky.MUIP;
using UnityEngine;
namespace Game.UI
{
    public class StaminaUI : MonoBehaviour
    {
        [SerializeField] private ProgressBar progressBar;

        public void SetStaminaPercentage(float percentage)
        {
            progressBar.currentPercent = percentage * 100;
        }

    }
}
