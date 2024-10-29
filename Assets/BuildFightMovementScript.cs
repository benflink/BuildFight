using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f; 
    public Animator animator;

    private Rigidbody rb;
    private Vector3 moveInput;
    private float currentSpeed;
    private bool canDash = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed; 
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveInput = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetButton("Sprint")) 
        {
            currentSpeed = sprintSpeed;
            animator.SetBool("isWalk", true); 
        }
        else
        {
            currentSpeed = walkSpeed;
        }


        if (moveInput.magnitude > 0)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalk", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = moveInput * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        if(Input.GetButton("Fire1"))
        {
            
            if (canDash)
            {
                Debug.Log("Fire1");
                rb.MovePosition((rb.position + movement));
                canDash = false;
                StartCoroutine(Dash());
            }
        }
        
        if (moveInput.magnitude > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("CanDash");
        canDash = true;
    }
}
