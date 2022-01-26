using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    Vector3 direction;

    [SerializeField]
    float speed;

    [SerializeField]
    float maxSpeed;

    int desiredLane = 1; //0:left, 1:middle, 2:right

    float laneDistance = 2.0f;

    Vector3 targetPosition;

    [SerializeField]
    float lerpValue;

    public float jumpForce;

    public float gravity = -20;

    public Animator animator;

    bool isSliding = false;

    AudioManager audioManager;

    void Start()
    {
        direction.z = speed;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        if(speed < maxSpeed)
            speed += 0.1f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);
        animator.SetBool("isJumped", !controller.isGrounded);

        PlayerMovement();
    }


    void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            audioManager.PlaySound("GameOver");
            audioManager.StopSound("MainTheme");
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);

        controller.center = new Vector3(0, -0.9f, 0);
        controller.height = 0.3f;
        controller.radius = 0.05f;

        yield return new WaitForSeconds(0.8f);

        isSliding = false;
        controller.center = new Vector3(0, -0.54f, 0);
        controller.height = 0.79f;
        controller.radius = 0.48f;
        animator.SetBool("isSliding", false);
    }

    void PlayerMovement()
    {
        if (SwipeManager.swipeDown && !isSliding)
        {
            Debug.Log("down");
            StartCoroutine(Slide());
        }

        if (SwipeManager.swipeUp && controller.isGrounded)
        {
            Jump();
        }
        else if (!controller.isGrounded)
        {
            direction.y += gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeRight && desiredLane < 2)
        {
            desiredLane++;
        }

        if (SwipeManager.swipeLeft && desiredLane > 0)
        {
            desiredLane--;
        }

        targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up; // goal: taking position as vector3

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }


        controller.Move(direction * Time.deltaTime);
    }
}
