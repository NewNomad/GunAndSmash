using Game.Control;
using Game.Move;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [SerializeField] int maxStep;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LineRenderer OnClickPositionLineRenderer;
    bool isPlayerMayMove = false;
    Vector2 moveVector = Vector2.zero;
    Vector2 mouseClickPosition = Vector2.zero;

    private void Start()
    {
        var a = PlayerController.instance.GetComponent<PlayerInputController>().GetIsPlayerMayMoveAndDirection();
    }
    private void FixedUpdate()
    {
        (isPlayerMayMove, moveVector) = PlayerController.instance.GetComponent<PlayerInputController>().GetIsPlayerMayMoveAndDirection();
        handlePredictionLine();
    }

    private void handlePredictionLine()
    {
        if (!isPlayerMayMove)
        {
            lineRenderer.enabled = false;
            OnClickPositionLineRenderer.enabled = false;
            mouseClickPosition = Vector2.zero;
            return;
        }
        lineRenderer.enabled = true;
        OnClickPositionLineRenderer.enabled = true;
        print(isPlayerMayMove);
        DrawPredictionLine();
        DrawOnClickPositionLineRenderer();
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

    // 最初にクリックした場所から今のマウス位置までlineRendererを引く
    private void DrawOnClickPositionLineRenderer()
    {
        if (mouseClickPosition == Vector2.zero)
        {
            mouseClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        OnClickPositionLineRenderer.transform.position = Vector3.zero;
        OnClickPositionLineRenderer.positionCount = 2;
        OnClickPositionLineRenderer.SetPosition(0, mouseClickPosition);
        var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OnClickPositionLineRenderer.SetPosition(1, currentMousePosition);
    }

}
