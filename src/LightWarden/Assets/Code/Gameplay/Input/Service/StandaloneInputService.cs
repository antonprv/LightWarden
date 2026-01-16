using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;

namespace Code.Gameplay.Input.Service
{
  public class StandaloneInputService : IInputService, System.IDisposable
  {
    private Camera _mainCamera;
    private Vector3 _screenPosition;
    private InputActions _inputActions;

    public StandaloneInputService()
    {
      // Initialize the input actions (the generated C# class)
      _inputActions = new InputActions();
      _inputActions.PCActionMap.Enable(); // Enable the PC platform-specific action map
    }

    public void Dispose()
    {
      // Clean up when service is destroyed
      _inputActions?.Dispose();
    }

    public Camera CameraMain
    {
      get
      {
        if (_mainCamera == null && Camera.main != null)
          _mainCamera = Camera.main;

        return _mainCamera;
      }
    }

    public Vector2 GetScreenMousePosition() =>
        Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;

    public Vector2 GetWorldMousePosition()
    {
      if (CameraMain == null)
        return Vector2.zero;

      _screenPosition = Mouse.current != null ?
          Mouse.current.position.ReadValue() :
          InputSystem.GetDevice<Mouse>()?.position.ReadValue() ?? Vector3.zero;

      return CameraMain.ScreenToWorldPoint(_screenPosition);
    }

    public bool HasAxisInput() => GetHorizontalAxis() != 0 || GetVerticalAxis() != 0;

    public float GetVerticalAxis() => _inputActions.PCActionMap.Move.ReadValue<Vector2>().y;
    public float GetHorizontalAxis() => _inputActions.PCActionMap.Move.ReadValue<Vector2>().x;

    public bool GetLeftMouseButton() =>
        _inputActions.PCActionMap.Fire.IsPressed() &&
        !IsPointerOverUI();

    public bool GetLeftMouseButtonDown() =>
        _inputActions.PCActionMap.Fire.WasPressedThisFrame() &&
        !IsPointerOverUI();

    public bool GetLeftMouseButtonUp() =>
        _inputActions.PCActionMap.Fire.WasReleasedThisFrame() &&
        !IsPointerOverUI();

    private bool IsPointerOverUI()
    {
      return EventSystem.current != null &&
             EventSystem.current.IsPointerOverGameObject();
    }
  }
}
