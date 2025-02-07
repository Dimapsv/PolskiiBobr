using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ThirdPersonController : MonoBehaviour
{
    //input fields
    private ThirpdPesonActionsAsset playerActionsAsset;
    private InputAction move;

    public static UnityEvent<bool> OnBobrHiddenChanged = new UnityEvent<bool>();

    public static UnityEvent OnLesopilkaTreeTaked = new UnityEvent();

    public bool bobrIsHidden;

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
    public bool isPushingButtonPressed = false;

    //camera
    [SerializeField]
    private Camera playerCamera;

    //interact
    [SerializeField] float talkDistance = 10f;
    public bool inConversation; //dialogue movement = 0

    public bool isAxeHas = false;
    public GameObject bobrAxe;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerActionsAsset = new ThirpdPesonActionsAsset();
    }

    private void Start()
    {
        bobrAxe.SetActive(false);
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
        DialogueManager.OnDialogueStart.AddListener(JoinConversation);
        DialogueManager.OnDialogueStop.AddListener(LeaveConversation);

        PlayerManager.OnAbilityTaked.AddListener(UpgradeAbilities);

        PlayerManager.OnAxeHasChanged.AddListener(CheckAxe);


    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Disable();
        DialogueManager.OnDialogueStart.RemoveListener(JoinConversation);
        DialogueManager.OnDialogueStop.RemoveListener(LeaveConversation);

        PlayerManager.OnAbilityTaked.RemoveListener(UpgradeAbilities);

        PlayerManager.OnAxeHasChanged.RemoveListener(CheckAxe);
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            isPushingButtonPressed = true;
        }
        else
        {
            isPushingButtonPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.E)) //interact
        {
            Interact();
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

    //Interact
    public void Interact()
    {
        if (inConversation)
        {
            DialogueManager.instance.SkipLine();
        }
        else
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
            {
                Debug.Log(hitInfo.collider.gameObject.name);
                if (hitInfo.collider.gameObject.TryGetComponent(out NPC npc))
                {
                    Debug.Log("Interact with NPC");
                    if (!npc.isfirstTalked)
                    {
                        DialogueManager.instance.StartDialogue(npc.firstDialogue);
                        npc.isfirstTalked = true;
                        if (npc.quest != null)
                            QuestManager.instance.AddQuest(npc.quest);
                    }
                    else
                    {
                        if (npc.quest == null)
                        {
                            DialogueManager.instance.StartDialogue(npc.postDialogue);
                        }
                        else if (npc.quest != null && QuestManager.instance.CheckQuest(npc.id))
                        {
                            DialogueManager.instance.StartDialogue(npc.questClearedDialogue);
                            QuestManager.instance.SpawnReward(npc.id);
                        }
                        else if (npc.quest != null && !QuestManager.instance.CheckQuest(npc.id))
                        {
                            DialogueManager.instance.StartDialogue(npc.questNotClearedQuest);
                        }

                    }


                }

                if (hitInfo.collider.gameObject.TryGetComponent(out LesopilkaTree tree))
                {
                    if (isAxeHas)
                    {
                        OnLesopilkaTreeTaked?.Invoke();
                        bobrAxe.SetActive(true);
                        tree.gameObject.SetActive(false);
                        Invoke("HideAxe",1.5f);
                    }
                }

            }
        }
    }

    public void HideAxe()
    {
        bobrAxe?.SetActive(false);
    }

    public void CheckAxe(bool isAxeHasChecker)
    {
        isAxeHas = isAxeHasChecker;
    }

    public void UpgradeAbilities(int idOfAbility)
    {
        switch (idOfAbility)
        {
            case 0:
                isPushingUpgraded = true;
                break;
        }
    }
    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
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
            
            Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, dashTimer/dashTime);

            // Check for collisions
            Vector3 direction = (targetPosition - rb.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(rb.position, direction, out hit, dashDistance))
            {
                // If there is a collision, stop the dash
                rb.MovePosition(hit.point - direction * 0.2f); // Move slightly back to avoid sticking
                break;
            }
            else
            {
                rb.MovePosition(targetPosition);
            }

            yield return null;
        }

        isDashing = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HiddenPlace"))
        {
            bobrIsHidden = true;
            OnBobrHiddenChanged.Invoke(bobrIsHidden);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HiddenPlace"))
        {
            bobrIsHidden = false;
            OnBobrHiddenChanged.Invoke(bobrIsHidden);
        }
    }
}
