using Game.Core;
using TMPro;
using UnityEngine;
namespace Game.UI
{
    public class KillAmountUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] RespawnController respawnController;
        private void Awake()
        {
            respawnController.onKillCountChanged.AddListener(UpdateKillCount);
        }

        private void UpdateKillCount(int totalKills, int maxKills)
        {
            text.text = "Kills: " + totalKills + "/" + maxKills + "";
        }
    }
}
