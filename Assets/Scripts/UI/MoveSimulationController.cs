using System.Collections.Generic;
using Game.Control;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [SerializeField] int maxStep;
    [SerializeField] LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();

    private void Start()
    {
        // lineRenderer.transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        DrawPredictionLine(PlayerController.instance.transform.position, transform.right * 1);
    }

    private void DrawPredictionLine(Vector2 basePosition, Vector2 fireVector)
    {
        lineRenderer.transform.position = Vector3.zero;
        Vector2 currentPosition = PlayerController.instance.transform.GetComponent<Rigidbody2D>().position;
        Vector2 currentVelocity = PlayerController.instance.transform.GetComponent<Rigidbody2D>().velocity;

        lineRenderer.positionCount = maxStep;

        for (int i = 0; i < maxStep; i++)
        {
            float time = i * Time.fixedDeltaTime;
            Vector2 predictedPosition = currentPosition + currentVelocity * time + 0.5f * Physics2D.gravity * Mathf.Pow(time, 2);
            lineRenderer.SetPosition(i, predictedPosition);
        }

    }

}
