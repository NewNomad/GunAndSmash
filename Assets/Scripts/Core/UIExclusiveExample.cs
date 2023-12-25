using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIExclusiveExample : MonoBehaviour
{
    // 対象のAction
    [SerializeField] private InputActionProperty _buttonAction;

    private void OnEnable()
    {
        // Input ActionのperformedコールバックにOnButtonActionを登録
        _buttonAction.action.performed += OnButtonAction;
        _buttonAction.action.Enable();
    }

    private void OnDisable()
    {
        // Input ActionのperformedコールバックからOnButtonActionを削除
        _buttonAction.action.performed -= OnButtonAction;
        _buttonAction.action.Disable();
    }

    // Input Actionのperformedコールバック
    private void OnButtonAction(InputAction.CallbackContext context)
    {
        // UIの上にカーソルがあったら、入力を受け付けない
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        print("ボタンが押された！");
    }
}
