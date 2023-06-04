using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control_car : MonoBehaviour
{

    private float movespeed = 0.7f;
    private float rotation_speed = 100f;
    private float drag = 0.0005f;
    private Rigidbody rb;
    public control_joystick control;
    private int points = 0;
    public Text pointsText;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rb.maxAngularVelocity = rotation_speed;
    }

    private void Update()
    {
        float horizontalInput = control.joystick_LR();
        float verticalInput = control.joystick_TB();
        Vector3 dir = Vector3.zero;
        dir.x = control.joystick_LR();
        dir.z = control.joystick_TB();

        // Adjust the forward direction based on the joystick input
        Vector3 forwardDirection = transform.forward * dir.z;

        // Apply the movement to the rigidbody
        rb.velocity = forwardDirection * movespeed;

        // Move the car forward/backward
        Vector3 movement = transform.forward * verticalInput * movespeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Rotate the car
        float rotation = horizontalInput * rotation_speed * Time.deltaTime;
        Quaternion rotationDelta = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * rotationDelta);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Destroy(collision.gameObject);
            points++; // Increment points value
            UpdatePointsText(); // Update UI text
        }
    }
    private void UpdatePointsText()
    {
        pointsText.text = "Points: " + points.ToString();
    }
}