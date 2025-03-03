using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    public InputActionAsset actions;
    private InputAction xAxis;
    private InputAction zAxis;
    private InputAction jump;

    private const string ACTION_MAP = "Player";
    private const string MOVE = "Move";
    private const string JUMP = "Jump";

    private PlayerController playerController;
    void Awake()
    {
        xAxis = actions.FindActionMap(ACTION_MAP).FindAction(MOVE);
        jump = actions.FindActionMap(ACTION_MAP).FindAction(JUMP);
        playerController = GetComponent<PlayerController>();
    }

    void OnEnable()
    {
        actions.FindActionMap(ACTION_MAP).Enable();
        jump.performed += OnJump;
    }

    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        jump.performed -= OnJump;
    }

    public float GetMoveValue()
    {
        return xAxis.ReadValue<float>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!playerController.IsGrounded()) return;

        playerController.Jump();
    }
}
