using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float rayLength = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    private  Animator animator;


    private Rigidbody rb;
    private float moveX = 0f;
    private float currentSpeed;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 lv = rb.linearVelocity;
        lv.x = moveX * moveSpeed;
        rb.linearVelocity = new Vector3(lv.x, lv.y, 0f);

        if (Mathf.Abs(moveX) > 0.01f)
        {
            float targetAngle = moveX > 0 ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
        CheckGround();

        currentSpeed = new Vector3(rb.linearVelocity.x, 0f, 0f).magnitude;
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("Grounded", isGrounded);
        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("FreeFall", true);
        }
        else
        {
            animator.SetBool("FreeFall", false);
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveX = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (isGrounded)
            {
                animator.SetTrigger("StartJump");
                Vector3 lv = rb.linearVelocity;
                lv.y = jumpForce;
                rb.linearVelocity = lv;
            }
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);
    }
}
