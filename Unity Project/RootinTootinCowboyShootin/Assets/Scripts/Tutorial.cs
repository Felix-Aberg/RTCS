using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject boot_left;
    public GameObject boot_right;

    public GameObject arrow_left_pressed;
    public GameObject arrow_right_pressed;

    public Vector2[] boot_left_positions;
    public Vector2[] boot_right_positions;

    public float step_time;
    float next_change;
    bool up;

    bool left_pressed;
    bool right_pressed;
    bool left_pressed_last_frame;
    bool right_pressed_last_frame;

    // Start is called before the first frame update
    void Start()
    {
        next_change = Time.time + step_time;
    }

    // Update is called once per frame
    void Update()
    {
        left_pressed_last_frame = left_pressed;
        right_pressed_last_frame = right_pressed;
        left_pressed = Input.GetButton("DanceMatLeft");
        right_pressed = Input.GetButton("DanceMatRight");
    }

    void FixedUpdate()
    {
        if (!left_pressed_last_frame && left_pressed)
            LeftBootDown();

        if (!right_pressed_last_frame && right_pressed)
            RightBootDown();

        if (Time.time > next_change)
        {
            if (up)
            {
                if (!left_pressed)
                {
                    LeftBootUp();
                }

                if (!right_pressed)
                {
                    RightBootUp();
                }

                up = false;
            }

            else if (!up)
            {
                if (!left_pressed)
                {
                    LeftBootDown();
                }

                if (!right_pressed)
                {
                    RightBootDown();
                }

                up = true;
            }

            next_change += step_time;
        }
    }

    void LeftBootUp()
    {
        arrow_left_pressed.SetActive(false);
        boot_left.transform.position = boot_left_positions[0];
    }

    void LeftBootDown()
    {
        arrow_left_pressed.SetActive(true);
        boot_left.transform.position = boot_left_positions[1];
    }

    void RightBootUp()
    {
        arrow_right_pressed.SetActive(false);
        boot_right.transform.position = boot_right_positions[0];
    }

    void RightBootDown()
    {
        arrow_right_pressed.SetActive(true);
        boot_right.transform.position = boot_right_positions[1];
    }
}
