using Game.Core;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    Transform target;
    Vector3 homingVelocity;
    Vector3 homingPosition;
    [SerializeField] int damage = 10;
    // 寿命
    [SerializeField] float maxLivingTime = 1f;
    float currentLivingTime = 0f;
    [SerializeField] TrailRenderer trailRenderer;

    private void Awake()
    {
        homingPosition = transform.position;
        currentLivingTime = maxLivingTime;
    }

    public void Setup(Transform target, Vector3 homingVelocity)
    {
        this.target = target;
        this.homingVelocity = homingVelocity;
    }

    private void Update()
    {
        if (target == null)
        {
            DestroyLaser();
            return;
        }
        Vector3 diff = target.transform.position - transform.position;
        var acceleration = (diff - homingVelocity * currentLivingTime) * 2f / (currentLivingTime * currentLivingTime);
        currentLivingTime -= Time.deltaTime;
        if (currentLivingTime < 0f)
        {
            OnHitTarget();
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

    void OnHitTarget()
    {
        if (!target.TryGetComponent(out IDamageable damageable)) { return; }
        damageable.TakeDamage(damage);
    }
}
