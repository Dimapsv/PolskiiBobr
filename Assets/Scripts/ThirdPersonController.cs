using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    //input fields
    private ThirpdPesonActionsAsset playerActionsAsset;
    private InputAction move;

    //movement fields 
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;


    //dash
    public float dashDistance = 12f;
    public float dashTime = 0.2f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    public bool isDashingUpgraded = false;


    //push
    public float radiusPush;
    public float pushTime = 0.2f;
    private float pushTimer = 0f;
    public bool isPushing = false;
    public float forcePush;
    public bool isPushingUpgraded = false;

    //camera
    [SerializeField]
    private Camera playerCamera;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerActionsAsset = new ThirpdPesonActionsAsset();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();

        
    }


    private void Update()
    {
        // Check for input and initiate dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && isDashingUpgraded)
        {
            StartCoroutine(Dash());
        }
        

        // Update dash timer
        if (isDashing)
        {
            dashTimer += Time.deltaTime;

            // Check if dash duration is over
            if (dashTimer >= dashTime)
            {
                isDashing = false;
                dashTimer = 0f;
            }
        }
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }


    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }


    private void DoJump(InputAction.CallbackContext obj)
    {
        if (isGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool isGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            return true;
        else
            return false;
    }


    IEnumerator Dash()
    {
        isDashing = true;

        // Store initial position
        Vector3 startPosition = transform.position;

        // Calculate dash end position
        Vector3 endPosition = startPosition + transform.forward * dashDistance;

        // Perform the dash
        while (isDashing)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, endPosition, dashTimer / dashTime));
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * forcePush, ForceMode.Impulse);
            }
        }

    }

}
