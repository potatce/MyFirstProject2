using UnityEngine;

public class InputActions : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public float Horizontal;
    public bool Jump;
    public bool Sprint;
    public bool attack;
    public bool interact;

    public void Update()
    {
        Horizontal = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        Jump = _inputSystem.Player.Jump.WasPressedThisFrame();
        Sprint = _inputSystem.Player.Sprint.IsPressed();
    }
    private void Awake() {_inputSystem = new InputSystem_Actions();}

    private void OnEnable() {_inputSystem.Enable();}

    private void OnDisable() {_inputSystem.Disable();}
}