using Game.Control;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        RespawnController respawnController;
        CountDownTimer countDownTimer;

        bool isGameStarted = false;
        private void Awake()
        {
            TryGetComponent(out respawnController);
            TryGetComponent(out countDownTimer);
            respawnController.onKillAllEnemies.AddListener(EndGame);
            countDownTimer.onTimerEnd.AddListener(EndGame);
        }

        public void StartGame()
        {
            if (isGameStarted) { return; }
            isGameStarted = true;
            BgmPlayer.Instance.Play("bgm");
            respawnController.Initiate();
            countDownTimer.Initiate();
        }

        public void EndGame()
        {
            if (!isGameStarted) { return; }
            isGameStarted = false;
            PlayerController.instance.GetComponent<Health>().ForceDeath();
            BgmPlayer.Instance.Stop();
            respawnController.Reset();
        }


        private void ConsumptionScore()
        {
        }
    }
}
