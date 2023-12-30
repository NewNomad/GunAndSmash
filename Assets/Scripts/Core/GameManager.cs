using UnityEngine;
namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        RespawnController respawnController;
        CountDownTimer countDownTimer;
        private void Awake()
        {
            TryGetComponent(out respawnController);
            TryGetComponent(out countDownTimer);
            respawnController.onKillAllEnemies.AddListener(EndGame);
            countDownTimer.onTimerEnd.AddListener(EndGame);
        }

        public void StartGame()
        {
            respawnController.EnableRespawnController = true;
            countDownTimer.Initiate();
        }

        public void EndGame()
        {
            respawnController.Reset();
            respawnController.EnableRespawnController = false;
        }


        private void ConsumptionScore()
        {
        }
    }
}
