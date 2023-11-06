using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    private bool jumpHasPeaked = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            StartCoroutine(JumpPeakDelay());

        }
    

        if (Input.GetButtonDown("Crouch"))
        {
            
            crouch = true;
            animator.SetBool("IsCrouching", true);

        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouching", false);
        }
    }

    IEnumerator JumpPeakDelay()
    {
        // Wait for the expected peak time of the jump
        yield return new WaitForSeconds(0.5f); // Adjust the time to the peak of your jump
        jumpHasPeaked = true;
    }

    public void OnLanding()
    {
        Debug.Log("LAAAAANDING");
        if (jumpHasPeaked)
        {
            animator.SetBool("IsJumping", false);
            jumpHasPeaked = false;
        }
    }

    private void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;



    }
}
