using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    // カーソル位置取得用のAction
    private readonly List<RaycastResult> _results = new();


    // Selectable以外の対象UIのタグ
    [SerializeField] private string _uiTag = "InputExclusiveUI";
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
        if (IsPointerOverSelectable()) return;

        onStop.Invoke();
        clickDuration = 0f;
        startClickPos = GetMousePos();
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        if (IsPointerOverSelectable()) return;

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

    private bool IsPointerOverSelectable()
    {
        // レイキャスト用の情報を作成
        PointerEventData _pointer = new PointerEventData(EventSystem.current)
        {
            position = GetMousePos()
        };

        // カーソル上にあるUIをレイキャストで取得
        _results.Clear();
        EventSystem.current.RaycastAll(_pointer, _results);

        // SelectableなUI上にカーソルがあったら、入力を受け付けない
        for (var i = 0; i < _results.Count; i++)
        {
            GameObject uiObj = _results[i].gameObject;
            Debug.Log(uiObj.name);

            if (uiObj.CompareTag(_uiTag))
                return true;

            if (uiObj.GetComponent<Selectable>() != null)
                return true;
        }
        return false;
    }
}
