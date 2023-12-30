using System.Collections;
using Cinemachine;
using Game.Control;
using Michsky.MUIP;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        RespawnController respawnController;
        CountDownTimer countDownTimer;
        [SerializeField] ModalWindowManager gameResultWindow;

        bool isGameStarted = false;
        private void Start()
        {
            TryGetComponent(out respawnController);
            TryGetComponent(out countDownTimer);
            respawnController.onKillAllEnemies.AddListener(EndGame);
            countDownTimer.onTimerEnd.AddListener(EndGame);
            PlayerController.instance.GetComponent<Health>().onDeath.AddListener(EndGame);
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
            Debug.Log("EndGame");
            if (!isGameStarted) { return; }
            isGameStarted = false;
            PlayerController.instance.GetComponent<Health>().ForceDeath();
            BgmPlayer.Instance.Stop();
            countDownTimer.StopTimer();
            StartCoroutine(ShowGameResult());
        }

        IEnumerator ShowGameResult()
        {
            HitStopController.Instance.BossHitStop();
            yield return new WaitForSecondsRealtime(2.5f);
            CinemachineVirtualCamera virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = GameObject.Find("GameStartPoint").transform;
            gameResultWindow.OpenWindow();
            gameResultWindow.descriptionText = GetGameResult();
            gameResultWindow.UpdateUI();
            respawnController.Reset();

        }

        private string GetGameResult()
        {
            int remainTime = int.Parse(countDownTimer.CurrentTime.ToString("F0"));
            int score = respawnController.TotalKills * 100 + remainTime * 80;
            string result = "";
            result += "倒した敵の数: " + respawnController.TotalKills + "\n";
            result += "残り時間: " + remainTime + "\n";
            result += "スコア計算: " + respawnController.TotalKills + " × 100 + " + remainTime + " × 80  \n";
            result += "スコア合計: " + score + "\n";
            return result;
        }


        private void ConsumptionScore()
        {
        }
    }
}
