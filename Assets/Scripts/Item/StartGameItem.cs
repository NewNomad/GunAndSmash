using System.Collections;
using Game.Control;
using Game.Core;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;

public class StartGameItem : MonoBehaviour, IItem
{
    [SerializeField] Transform playerStartPoint;
    [SerializeField] ParticleSystem startGameParticles;
    [SerializeField] GameStartEffect gameStartEffect;
    [SerializeField] GameManager gameManager;
    bool isUsed = false;
    public void UseItem()
    {
        if (isUsed) { return; }
        isUsed = true;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        Instantiate(startGameParticles, transform.position, Quaternion.identity);
        SePlayer.Instance.Play("etfx_explosion_flash");
        BgmPlayer.Instance.Stop();
        Time.timeScale = .5f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = .2f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.8f);
        GlitchManager.instance.SetGlitch(2.5f);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(2.5f);
        PlayerController.instance.transform.position = playerStartPoint.position;
        PlayerController.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(2f);
        gameStartEffect.Initiate();
        gameManager.StartGame();
        isUsed = false;
    }
}
