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
        PlayerController.instance.GetComponent<PlayerInputController>().onPlayerMayMove.AddListener(OnPlayerMayMove);

    }
    private void FixedUpdate()
    {
        Debug.Log(isPlayerMayMove);
        // if (!isPlayerMayMove)
        // {
        //     lineRenderer.enabled = false;
        //     return;
        // }
        // lineRenderer.enabled = true;
        DrawPredictionLine();
    }

    void OnPlayerMayMove(bool isPlayerMayMove, Vector2 direction)
    {
        this.isPlayerMayMove = isPlayerMayMove;
        moveVector = direction;
    }

    private void DrawPredictionLine()
    {
        lineRenderer.transform.position = Vector3.zero;
        Rigidbody2D playerRigidbody = PlayerController.instance.GetComponent<Rigidbody2D>();
        Vector2 currentPosition = playerRigidbody.position;
        Vector2 gravity = playerRigidbody.gravityScale * Physics2D.gravity;
        float moveSpeed = PlayerController.instance.GetComponent<Mover>().GetMoveSpeed();

        lineRenderer.positionCount = maxStep;

        for (int i = 0; i < maxStep; i++)
        {
            float time = i * Time.fixedDeltaTime;
            Vector2 predictedPosition = currentPosition + moveVector * time + moveSpeed * gravity * time * time;
            lineRenderer.SetPosition(i, predictedPosition);
        }

    }

}
