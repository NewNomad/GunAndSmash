using naichilab.EasySoundPlayer.Scripts;
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
            BgmPlayer.Instance.Play("bgm");
            respawnController.Initiate();
            countDownTimer.Initiate();
        }

        public void EndGame()
        {
            BgmPlayer.Instance.Stop();
            respawnController.Reset();
            respawnController.Reset();
        }


        private void ConsumptionScore()
        {
        }
    }
}
