using System.Collections;
using Game.Core;
using UnityEngine;

public class StartGameItem : MonoBehaviour, IItem
{
    public void UseItem()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        Time.timeScale = .5f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        GlitchManager.instance.SetGlitch(1f);
    }
}
