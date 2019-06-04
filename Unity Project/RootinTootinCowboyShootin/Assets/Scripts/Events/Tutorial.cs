using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public ReticleScript reticle;
    public GameObject crosshair;

    public WiiMote wiimote;

    public SpriteRenderer explosion;
    public SpriteRenderer instructions;
    public SpriteRenderer circle;

    public Sprite[] instruction_sprites;

    public Transform logo;
    public Transform boot_dad;
    public GameObject boot_left;
    public GameObject boot_right;

    public Vector2[] logo_positions;
    public Vector2[] boot_dad_positions;
    public Vector2[] boot_left_positions;
    public Vector2[] boot_right_positions;

    Color color;

    float state_swap_time = 1f;
    float state_swap = 0f;

    public bool colliding;
    public float step_time;
    float next_change;
    bool up;

    bool left_pressed;
    bool right_pressed;
    bool left_pressed_last_frame;
    bool right_pressed_last_frame;

    bool dancemat_done;

    bool shoot;
    bool shoot_last_frame;

    public int state = 0;

    bool once;

    AudioSource AS;
    public AudioClip step;
    public AudioClip gunshot;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        color = Color.white;
        color.a = 0;

        next_change = Time.time + step_time;

        crosshair.SetActive(false);
        reticle.InstantiateArrows(ArrowDirection.LEFT, ArrowDirection.RIGHT);
    }

    void FixedUpdate()
    {
        left_pressed_last_frame = left_pressed;
        right_pressed_last_frame = right_pressed;
        left_pressed = Input.GetButton("DanceMatLeft");
        right_pressed = Input.GetButton("DanceMatRight");

        if (left_pressed && right_pressed)
        {
            if (!dancemat_done)
                DirectionsCorrect();

            if ((!shoot_last_frame) && shoot)
            {
                if (crosshair.GetComponent<CrosshairScript>().current_reticle != null)
                {
                    if (!once)
                    {
                        once = true;
                        AS.PlayOneShot(gunshot);
                        TutorialComplete();
                    }
                }
            }
        }
        else if (dancemat_done)
        {
            dancemat_done = false;
            state = 0;
            instructions.sprite = instruction_sprites[0];
        }

        if (!left_pressed_last_frame && left_pressed)
            LeftBootDown();

        if (left_pressed_last_frame && !left_pressed)
            LeftBootUp();

        if (!right_pressed_last_frame && right_pressed)
            RightBootDown();

        if (right_pressed_last_frame && !right_pressed)
            RightBootUp();

        if (Time.time > next_change && !dancemat_done)
        {
            IndicateBoots();
        }

        //change dending on state
        if (state == 0)
        {
            //use (state_swap / state_swap_time) for a 0-1 value
            if (state_swap > 0)
            {
                state_swap -= Time.fixedDeltaTime;
            }
            else
            {
                state_swap = 0;
            }
            boot_dad.position = Vector2.Lerp(boot_dad.position, boot_dad_positions[0], 0.05f);
            logo.position = Vector2.Lerp(logo.position, logo_positions[0], 0.05f);
            color.a = Mathf.Lerp(color.a, 0, 0.08f);
        }
        else
        {
            if (state_swap < state_swap_time)
            {
                state_swap += Time.fixedDeltaTime;
            }
            else
            {
                state_swap = state_swap_time;
            }

            boot_dad.position = Vector2.Lerp(boot_dad.position, boot_dad_positions[1], 0.05f);
            logo.position = Vector2.Lerp(logo.position, logo_positions[1], 0.05f);
            color.a = Mathf.Lerp(color.a, 1, 0.08f);
        }

        //move logo and boot_dad
        //boot_dad.position = Vector2.Lerp(boot_dad_positions[0], boot_dad_positions[1], state_swap / state_swap_time);
        //logo.position = Vector2.Lerp(logo_positions[0], logo_positions[1], state_swap / state_swap_time);
        //color.a = state_swap / state_swap_time;
        circle.color = color;
        //Debug.Log("Circle color: " + circle.color);
        //Debug.Log("Other color: " + color);

    }

    void LeftBootUp()
    {
        reticle.UpdateReticle(ArrowDirection.LEFT, false);
        boot_left.transform.localPosition = boot_left_positions[0];
    }

    void LeftBootDown()
    {
        AS.PlayOneShot(step);
        reticle.UpdateReticle(ArrowDirection.LEFT, true);
        boot_left.transform.localPosition = boot_left_positions[1];
    }

    void RightBootUp()
    {
        reticle.UpdateReticle(ArrowDirection.RIGHT, false);
        boot_right.transform.localPosition = boot_right_positions[0];
    }

    void RightBootDown()
    {
        AS.PlayOneShot(step);
        reticle.UpdateReticle(ArrowDirection.RIGHT, true);
        boot_right.transform.localPosition = boot_right_positions[1];
    }

    void IndicateBoots() //Boots move up and down to show where to place feet
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
        }

        next_change += step_time;

        if (up)
            up = false;
        else if (!up)
            up = true;
    }

    void DirectionsCorrect() //Flashes checkmark and enables crosshair for shooting section
    {
        dancemat_done = true;
        reticle.directions_correct = true;
        crosshair.gameObject.SetActive(true);
        reticle.FindCrosshair();

        if (colliding)
        {
            Debug.Log("aim correct when directions turned correct!!");
            state = 2;
            instructions.sprite = instruction_sprites[2];
        }
        else
        {
            state = 1;
            instructions.sprite = instruction_sprites[1];
        }

        
    }

    public void TutorialComplete()
    {
        Debug.Log("Tutorial complete!");
        AS.PlayOneShot(gunshot);

        StartCoroutine(StartGame(1.5f));
    }
    IEnumerator ToggleSprite(SpriteRenderer spriterenderer, bool enabled, float time) //Toggle a sprite on or off after given time
    {
        yield return new WaitForSeconds(time);
        spriterenderer.enabled = enabled;
    }

    IEnumerator StartGame(float time)
    {
        yield return new WaitForSeconds(time);
        //SceneManager.LoadScene("CutsceneScene");
        FindObjectOfType<LevelFadeScript>().SwapLevel(Stages.CUTSCENE);
    }
}
