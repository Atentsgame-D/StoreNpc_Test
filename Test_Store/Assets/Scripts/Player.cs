using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    PlayerInputActions actions = null;
    Animator anim = null;
    TextMeshProUGUI useText = null;
    CharacterController controller;

    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    enum MoveMode
    {
        Walk = 0,
        Run
    }

    MoveMode moveMode = MoveMode.Run;

    public float runSpeed = 6.0f;
    public float turnSpeed = 10.0f;
    public float walkSpeed = 3.0f;

    public bool tryUse = false;
    public bool isTrigger = false;

    private void Awake()
    {
        actions = new PlayerInputActions();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        useText = GameObject.Find("UseText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        useText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMoveInput;
        actions.Player.Move.canceled += OnMoveInput;
        actions.Player.Use.performed += OnUseInput;
        actions.Player.MoveModeChange.performed += OnMoveModeChange;
    }


    private void OnDisable()
    {
        actions.Player.MoveModeChange.performed -= OnMoveModeChange;
        actions.Player.Use.performed -= OnUseInput;
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
        //inputDir.Normalize();

        if (inputDir.sqrMagnitude > 0.0f)
        {
            inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
            targetRotation = Quaternion.LookRotation(inputDir);
        }
    }

    private void OnUseInput(InputAction.CallbackContext context)
    {
        Use();
    }

    public void Use()
    {
        if (isTrigger)
        {
            if (tryUse)
            {
                tryUse = false;
            }
            else
            {
                tryUse = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        useText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        tryUse = false;
        useText.gameObject.SetActive(false);
    }

    private void OnMoveModeChange(InputAction.CallbackContext context)
    {
        if (moveMode == MoveMode.Walk)
        {
            moveMode = MoveMode.Run;
        }
        else
        {
            moveMode = MoveMode.Walk;
        }
    }

    private void Update()
    {
        float speed = 1.0f;
        if (inputDir.sqrMagnitude > 0.0f)
        {
            if (moveMode == MoveMode.Run)
            {
                anim.SetFloat("Speed", 1.0f);
                speed = runSpeed;
            }
            else if (moveMode == MoveMode.Walk)
            {
                anim.SetFloat("Speed", 0.3f);
                speed = walkSpeed;
            }
            controller.Move(speed * Time.deltaTime * inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);
        }
    }
}
