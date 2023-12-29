using System.Collections;
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
    }
}
