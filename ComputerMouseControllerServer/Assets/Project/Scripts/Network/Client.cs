using Mirror;
using UnityEngine;

public class Client : NetworkBehaviour
{
    private CursorManager _cursorManager = null;

    private void Start()
    {
        _cursorManager = FindObjectOfType<CursorManager>();

        if (!isLocalPlayer || isServer) return;

        InputController.Instance.OnMousePosChanged += MoveCursorInDirection;
        InputController.Instance.OnLeftMouseButtonClicked += LeftMouseButtonClick;
        InputController.Instance.OnRightMouseButtonClicked += RightMouseButtonClick;
    }

    [Command]
    private void MoveCursorInDirection(Vector2 pos) => _cursorManager.MoveCursor(pos);

    [Command]
    private void LeftMouseButtonClick() => _cursorManager.LeftMouseButtonClick();

    [Command]
    private void RightMouseButtonClick() => _cursorManager.RightMouseButtonClick();
}