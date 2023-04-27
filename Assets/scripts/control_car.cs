using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class control_car : MonoBehaviour
{
    private float movespeed = 0.7f;
    private float rotation_speed = 0.001f;
    private float drag = 0.0005f;
    private Rigidbody rb;
    public control_joystick control;
    public Slider throtle;
    private float throtle_val = 0.001f;
    private Vector3 pos_ball;
    private float min_ball_pos;
    private float max_ball_pos;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rb.maxAngularVelocity = rotation_speed;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        dir.x = control.joystick_LR();
        dir.z = control.joystick_TB();
        rb.velocity = dir * movespeed;
    }
    private void ValueChangeCheck()
    {
        if (spawned == false)
        {
            pos_ball = transform.position;
            min_ball_pos = pos_ball.y;
            max_ball_pos = pos_ball.y + 3.0f;
            spawned = true;
        }
        throtle_val = throtle.value;
        if (throtle_val < 0.115)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
            update_y_value();
        }
    }

    private void update_y_value()
    {
        pos_ball.x = transform.position.x;
        pos_ball.z = transform.position.z;
        pos_ball.y = map(throtle_val);

        transform.position = pos_ball;
    }
    private float map(float val)
    {
        float min_slider_val = 0.15f;
        float max_slider_val = 1.0f;
        return (val - min_slider_val) / (max_slider_val - min_slider_val) * (max_ball_pos - min_ball_pos) + min_ball_pos;
    }
}