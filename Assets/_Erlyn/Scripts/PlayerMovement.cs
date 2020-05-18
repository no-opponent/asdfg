using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, Controls.IPlayerActions
{
    GameManagement gm;

    Rigidbody body;
    public Controls control;

    Vector2 moveDir;
    float horizontal;
    float vertical; 

    public float runSpeed = 10.0f;

    [HideInInspector]
    public Animator animator;
    //[HideInInspector]
    public string state = "idle";

    [HideInInspector]
    public GameObject interactable;

    void Awake()
    {
        control = new Controls();
        control.Player.SetCallbacks(this);
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagement>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.K)) {
            animator.SetBool("Dancing", !(animator.GetBool("Dancing")));
        }

    }

    void FixedUpdate()
    {
        body.velocity = new Vector3(-moveDir.x * runSpeed, 0, -moveDir.y * runSpeed);

        if (body.velocity != new Vector3(0,0,0))
        {
            Vector3 lookDir = body.velocity;
            transform.rotation = Quaternion.LookRotation(lookDir);

        }

        if (body.velocity.x != 0 || body.velocity.z != 0)
        {
            if (body.velocity.x > runSpeed / 2 || body.velocity.x < -runSpeed / 2 ||
                body.velocity.z > runSpeed / 2 || body.velocity.z < -runSpeed / 2)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Running", false);
            }
        } else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }
    }

    void OnDisable()
    {
        control.Player.Disable();
        control.Interaction.Enable();
    }

    void OnEnable()
    {
        control.Player.Enable();
        control.Interaction.Disable();
    }

    IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        control.Player.Move.Enable();
        StopCoroutine("WaitFor");
    }

    public void BlockMovement(bool on, float wait = 0f)
    {
        if (on) // Player can't move but can interact
        {
            control.Player.Move.Disable();
        }
        else if (!on && wait == 0) // Player can move right away
        {
            control.Player.Move.Enable();
        }
        else if (!on && wait > 0f) // Player can move after X seconds
            StartCoroutine("WaitFor", wait);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();

        GetComponent<Inventory>().CloseMenu();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if(control.Player.Action.triggered)
            interactable.GetComponent<Interactable>().Interact(); 
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (control.Player.Inventory.triggered)
        {
            gm.inventoryUI.SetActive(!gm.inventoryUI.activeSelf); // UI
            gm.cameraManager.GetComponent<CameraMovement>().InventoryOpen(gm.inventoryUI.activeSelf);
        } 
    }

    public void PlayToolAnimation(string animation, float seconds)
    {
        StartCoroutine(PlayToolAnimationCR(animation, seconds));
    }

    IEnumerator PlayToolAnimationCR(string animation, float seconds)
    {
        state = animation;
        animator.SetBool(animation, true);
        yield return new WaitForSeconds(seconds);
        animator.SetBool(animation, false);
        BlockMovement(false);
        state = "idle";
        //StopCoroutine("PlayToolAnimationCR");
    }
}
