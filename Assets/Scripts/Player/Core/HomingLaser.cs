using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    Transform target;
    Vector3 homingVelocity;
    Vector3 homingPosition;
    // 寿命
    [SerializeField] float maxLivingTime = 1f;
    float currentLivingTime = 0f;
    [SerializeField] TrailRenderer trailRenderer;

    private void Awake()
    {
        homingPosition = transform.position;
        homingVelocity = new Vector3(0, -50, 0);
        currentLivingTime = maxLivingTime;
    }

    public void Setup(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        Debug.Log(target);
        // if (target == null)
        // {
        //     DestroyLaser();
        //     return;
        // }
        Vector3 diff = target.transform.position - transform.position;
        var acceleration = (diff - homingVelocity * currentLivingTime) * 2f / (currentLivingTime * currentLivingTime);
        currentLivingTime -= Time.deltaTime;
        if (currentLivingTime < 0f)
        {
            DestroyLaser();
        }
        homingVelocity += acceleration * Time.deltaTime;
        homingPosition += homingVelocity * Time.deltaTime;
        transform.position = homingPosition;
    }

    void DestroyLaser()
    {
        if (trailRenderer != null)
        {
            trailRenderer.transform.parent = null;
            // Destroy(trailRenderer.gameObject, trailRenderer.time);
        }
        Destroy(gameObject);
    }
}
