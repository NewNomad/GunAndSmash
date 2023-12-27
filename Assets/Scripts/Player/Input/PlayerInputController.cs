using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 入力を受け取ってイベントを発行する
/// </summary>
/// 
public class PlayerInputController : MonoBehaviour
{
    [Header("Input Settings")]
    [Header("移動を有効にするのに必要なマウス移動距離")]
    [SerializeField] private float enableMoveDistance = 0.1f;
    [Header("クリックと判定する最大の時間")]
    [SerializeField] private float maxClickDuration = 0.2f;
    private float clickDuration = 0f;
    private Vector2 startClickPos;
    public UnityEvent onFireEvent;
    public UnityEvent<Vector2> onMoveEvent;
    public UnityEvent onEnhancedFireEvent;
    public UnityEvent<Vector2> onEnhancedMoveEvent;
    public UnityEvent onStop;

    PlayerInput input;
    private void Awake()
    {
        TryGetComponent(out input);
    }

    private void OnEnable()
    {
        input.actions["Fire"].started += OnClick;
        input.actions["Fire"].canceled += OnClickCanceled;
    }

    private void OnDisable()
    {
        input.actions["Fire"].started -= OnClick;
    }

    private void Update()
    {
        // if (clickDuration < 0) return;
        clickDuration += Time.deltaTime;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        // UIの上にカーソルがあったら、入力を受け付けない
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        onStop.Invoke();
        clickDuration = 0f;
        startClickPos = GetMousePos();
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        // UIの上にカーソルがあったら、入力を受け付けない
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // FIXME: 実装時間がないため一次的に長押し系は無効化
        // if (clickDuration < maxClickDuration)
        // {
        handleClickDurationLessThanMax();
        // }
        // else
        // {
        //     handleClickDurationMoreThanMax();
        // }
        clickDuration = -1f;
    }

    private void handleClickDurationLessThanMax()
    {
        Vector2 endClickPos = GetMousePos();
        float ClickedDistance = Vector2.Distance(startClickPos, endClickPos);
        if (ClickedDistance > enableMoveDistance)
        {
            Vector2 direction = (startClickPos - endClickPos).normalized;
            onMoveEvent.Invoke(direction);
        }
        else
        {
            onFireEvent.Invoke();
        }
    }

    private void handleClickDurationMoreThanMax()
    {
        Vector2 endClickPos = GetMousePos();
        float ClickedDistance = Vector2.Distance(startClickPos, endClickPos);
        if (ClickedDistance > enableMoveDistance)
        {
            Vector2 direction = (startClickPos - endClickPos).normalized;
            onEnhancedMoveEvent.Invoke(direction);
        }
        else
        {
            onEnhancedFireEvent.Invoke();
        }
    }

    // マウスの位置を取得する
    private Vector2 GetMousePos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }
}
