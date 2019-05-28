using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public ReticleScript reticle;
    public GameObject crosshair;

    public WiiMote wiimote;

    public SpriteRenderer checkmark;
    public SpriteRenderer explosion;
    public SpriteRenderer instructions;

    public Sprite[] instruction_sprites;

    public GameObject boot_left;
    public GameObject boot_right;

    public Vector2[] boot_left_positions;
    public Vector2[] boot_right_positions;

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

    // Start is called before the first frame update
    void Start()
    {
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
                    TutorialComplete();
                }
            }
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
    }

    void LeftBootUp()
    {
        reticle.UpdateReticle(ArrowDirection.LEFT, false);
        boot_left.transform.position = boot_left_positions[0];
    }

    void LeftBootDown()
    {
        reticle.UpdateReticle(ArrowDirection.LEFT, true);
        boot_left.transform.position = boot_left_positions[1];
    }

    void RightBootUp()
    {
        reticle.UpdateReticle(ArrowDirection.RIGHT, false);
        boot_right.transform.position = boot_right_positions[0];
    }

    void RightBootDown()
    {
        reticle.UpdateReticle(ArrowDirection.RIGHT, true);
        boot_right.transform.position = boot_right_positions[1];
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

        instructions.sprite = instruction_sprites[1];

        StartCoroutine(ToggleSprite(checkmark, true, 0f));
        StartCoroutine(ToggleSprite(checkmark, false, .3f));
        StartCoroutine(ToggleSprite(checkmark, true, .5f));
        StartCoroutine(ToggleSprite(checkmark, false, 1.2f));
    }

    public void TutorialComplete()
    {
        StartCoroutine(ToggleSprite(explosion, true, 0f));
        StartCoroutine(ToggleSprite(explosion, false, .2f));
        StartCoroutine(ToggleSprite(explosion, true, .3f));
        StartCoroutine(ToggleSprite(explosion, false, .5f));
        StartCoroutine(ToggleSprite(explosion, true, .6f));
        StartCoroutine(ToggleSprite(explosion, false, .8f));
        StartCoroutine(ToggleSprite(explosion, true, .9f));
        StartCoroutine(ToggleSprite(explosion, false, .11f));

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
