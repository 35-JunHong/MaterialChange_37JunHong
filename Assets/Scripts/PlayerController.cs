using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isOnGround;
    float speed = 10.0f;
    float GamePlane = 10.0f;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        // forward and backward
        if (transform.position.z < -GamePlane)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -GamePlane);
        }
        else if (transform.position.z > GamePlane)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, GamePlane);
        }

        //right and left border 
        if (transform.position.x < -GamePlane)
        {
            transform.position = new Vector3(-GamePlane, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > GamePlane)
        {
            transform.position = new Vector3(GamePlane, transform.position.y, transform.position.z);
        }
        PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = true;

            playerRdr.material.color = playerMtrs[1].color;
        }

    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerRdr.material.color = playerMtrs[2].color;
        }
    }
}
