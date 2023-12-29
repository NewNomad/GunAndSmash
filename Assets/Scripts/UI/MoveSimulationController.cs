using System.Collections.Generic;
using Game.Control;
using Game.Move;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [SerializeField] int maxStep;
    [SerializeField] LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();
    bool isPlayerMayMove = false;
    Vector2 moveVector = Vector2.zero;

    private void Start()
    {
        var a = PlayerController.instance.GetComponent<PlayerInputController>().GetIsPlayerMayMoveAndDirection();
    }
    private void FixedUpdate()
    {
        (isPlayerMayMove, moveVector) = PlayerController.instance.GetComponent<PlayerInputController>().GetIsPlayerMayMoveAndDirection();
        if (!isPlayerMayMove)
        {
            lineRenderer.enabled = false;
            return;
        }
        lineRenderer.enabled = true;
        DrawPredictionLine();
    }

    private void DrawPredictionLine()
    {
        lineRenderer.transform.position = Vector3.zero;
        Rigidbody2D playerRigidbody = PlayerController.instance.GetComponent<Rigidbody2D>();
        Vector2 currentPosition = playerRigidbody.position;
        Vector2 currentVelocity = moveVector * PlayerController.instance.GetComponent<Mover>().GetMoveSpeed();
        Vector2 gravity = Physics2D.gravity;

        lineRenderer.positionCount = maxStep;

        for (int i = 0; i < maxStep; i++)
        {
            float time = i * Time.fixedDeltaTime;
            Vector2 gravityEffect = 0.5f * gravity * time * time;
            Vector2 nextPosition = currentPosition + currentVelocity * time + gravityEffect;
            lineRenderer.SetPosition(i, nextPosition);
        }

    }

}
