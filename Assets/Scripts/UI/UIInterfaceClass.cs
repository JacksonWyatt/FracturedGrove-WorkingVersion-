//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInterface : UIClass
{

    public PlayerMovement PlayerMovement;

    private GameObject SpeedBackground;
    private GameObject SpeedBar;
    private GameObject HealthBackground;
    private GameObject HealthBar;
    private GameObject SprintBarBackground;
    private GameObject SprintBar;
    // Start is called before the first frame update
    void Start()
    {
        SpeedBackground = GameObject.Find("VelocityBarBackground");
        SpeedBar = GameObject.Find("VelocityBar");
        HealthBackground = GameObject.Find("HealthBarBackground");
        HealthBar = GameObject.Find("HealthBar");
        SprintBarBackground = GameObject.Find("SprintCooldownBarBackground");
        SprintBar = GameObject.Find("SprintCooldownBar");

        //Start corutines//
        StartCoroutine(SpeedFadeHelper());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedUI();
        UpdateHealthUI();
        UpdateSprintUI();
    }


    public void UpdateSpeedUI()
    {
        //Scale the speed UI to match the speed value of the player//
        if (PlayerMovement.GetMoveSpeed() <= PlayerMovement.GetMaxSpeed()){ // player is moving at normal speeds
            scaleByVal(
                //Get the decimal percentage current speed is at//
                PlayerMovement.GetMoveSpeed() / PlayerMovement.GetMaxSpeed(),
                SpeedBar, //--Object being affected
                'x' //--Dimension being scaled
                );
            //fix outline to normal outline
            SpeedBar.transform.parent.gameObject.GetComponent<Outline>().effectColor = Color.black;
            SpeedBar.transform.parent.gameObject.GetComponent<Outline>().effectDistance = new Vector2(2, -2);
        }
        else // Player is moving faster than walk speed
        {
            scaleByVal(
                // set bar to full // 
                1.05f,
                SpeedBar, //--Object being affected
                'x' //--Dimension being scaled
                );
            //change how the outline looks//
            SpeedBar.transform.parent.gameObject.GetComponent<Outline>().effectColor = new Color(1, 0, 0, 0.5f);
            SpeedBar.transform.parent.gameObject.GetComponent<Outline>().effectDistance = new Vector2(5, -5);
        }
        //--------------------------------------------------------//

        //Change the Bar to match the transparency of the Background//
        setColor(SpeedBar,//--object
                SpeedBar.GetComponent<UnityEngine.UI.Image>().color.r, //-- Object red value
                SpeedBar.GetComponent<UnityEngine.UI.Image>().color.g, //-- Object green value
                SpeedBar.GetComponent<UnityEngine.UI.Image>().color.b, //-- Object blue value
                SpeedBackground.GetComponent<UnityEngine.UI.Image>().color.a //-- Background transparency value
                );
        //---------------------------------------------------------//

    }

    private IEnumerator SpeedFadeHelper()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //Fade out UI//
            if (PlayerMovement.GetMoveSpeed() == 1 && SpeedBackground.GetComponent<UnityEngine.UI.Image>().color.a == 1)
            {
                StartCoroutine(fadeUI(SpeedBackground, 1, 30, 0));

                //Fade in UI//
            }
            else if (PlayerMovement.GetMoveSpeed() > 1 && SpeedBackground.GetComponent<UnityEngine.UI.Image>().color.a == 0)
            {
                StartCoroutine(fadeUI(SpeedBackground, 1, 30, 1));
            }
        }
    }
    public void UpdateHealthUI()
    {
        //Scale the health UI to the health value of the player//
        //Add in when playerClass is added//
        //----------------------------------------------------//

        //Change the Bar to match the transparency of the Background//
        setColor(HealthBar,//--object
                HealthBar.GetComponent<UnityEngine.UI.Image>().color.r, //-- Object red value
                HealthBar.GetComponent<UnityEngine.UI.Image>().color.g, //-- Object green value
                HealthBar.GetComponent<UnityEngine.UI.Image>().color.b, //-- Object blue value
                HealthBackground.GetComponent<UnityEngine.UI.Image>().color.a //-- Background transparency value
                );
        //---------------------------------------------------------//

        // Determine if the UI should be visible or not out due to lack of use //
        //Add in when playerClass is added//
        //--------------------------------------------------------------------//
    }

    private void UpdateSprintUI()
    {
        if (PlayerMovement.state == PlayerMovement.MovementState.sprinting)
        {
            SprintBarBackground.GetComponent<UnityEngine.UI.Image>().enabled = true;
            SprintBar.GetComponent<UnityEngine.UI.Image>().enabled = true;

            scaleByVal(
                PlayerMovement.GetSprintDurationLeft() / PlayerMovement.sprintduration,
                SprintBar,
                'x'
            );

        }
        else
        {
            SprintBarBackground.GetComponent<UnityEngine.UI.Image>().enabled = false;
            SprintBar.GetComponent<UnityEngine.UI.Image>().enabled = false;
            scaleByVal(
                1,
                SprintBar,
                'x'
                );
        }
    }

}
