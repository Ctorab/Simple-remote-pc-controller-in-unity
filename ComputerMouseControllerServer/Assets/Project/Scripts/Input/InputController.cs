using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; } = null;

    public event Action<Vector2> OnMousePosChanged = null;
    public event Action OnLeftMouseButtonClicked = null;
    public event Action OnRightMouseButtonClicked = null;

    [SerializeField] private Joystick _joystick = null;

    private void Awake() => Instance = this;

    private void Update() => OnMousePosChanged?.Invoke(new Vector2(_joystick.Horizontal, -_joystick.Vertical));

    public void LeftMouseButtonClick() => OnLeftMouseButtonClicked?.Invoke();
    public void RightMouseButtonClick() => OnRightMouseButtonClicked?.Invoke();
}