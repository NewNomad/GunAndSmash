using System.Collections;
using Cinemachine;
using UnityEngine;

public class HitStopController : MonoBehaviour
{
    public static HitStopController Instance { get; private set; }
    [SerializeField] private float hitStopTime = 0.1f;
    [SerializeField] private float hitStopPower = 5f;
    readonly float bossHitStopTime = 3f;
    readonly float bossHitStopTimeScale = 0.1f;

    private void Awake()
    {
        Instance = this;
    }

    public void HitStop()
    {
        StartCoroutine(HitStopAction(hitStopTime));
    }

    public void BossHitStop()
    {
        StartCoroutine(BossHitStopAction(hitStopTime));
    }
    private IEnumerator HitStopAction(float fTime)
    {
        Time.timeScale = 0f;
        CinemachineImpulseSource cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        cinemachineImpulse.GenerateImpulse(hitStopPower);
        yield return new WaitForSecondsRealtime(fTime);
        Time.timeScale = 1f;
    }
    private IEnumerator BossHitStopAction(float fTime)
    {
        Time.timeScale = 0f;
        CinemachineImpulseSource cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        cinemachineImpulse.GenerateImpulse(hitStopPower);
        yield return new WaitForSecondsRealtime(fTime);
        Time.timeScale = bossHitStopTimeScale;
        yield return new WaitForSecondsRealtime(bossHitStopTime);
        Time.timeScale = 1f;
    }

}