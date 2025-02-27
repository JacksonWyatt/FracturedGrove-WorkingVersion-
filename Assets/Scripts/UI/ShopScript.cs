using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Canvases")]
    public Canvas Interface;
    public Canvas Settings;
    public Canvas ShopCanvas;

    [Header("Scripts")]
    public settingsScript settingsScript;
    public PlayerCam PlayerCamScript;
    public PlayerMovement PlayerMovementScript;
    public CurrencySystem CurrencySystemScript;
    public ClimbAndCling WallSystemScript;
    public DimensionNavigation DimensionNavigationScript;

    private Dictionary<string, float[]> UpgValues;

    public Button[] UpgradeButtons; 
    // Start is called before the first frame update
    void Start()
    {
                //Set Values in upgradeDictionary//
        // Values go [String:StatName] = {int:Level,int:Cost}//
        UpgValues = new Dictionary<string, float[]>()
        {
            ["runspeed"] = new float[2] { 1, 5 },
            ["jumpheight"] = new float[2] { 1, 5 },
            ["cling"] = new float[2] { 1, 5 },
            ["climb"] = new float[2] { 1, 5 },
            ["wallrun"] = new float[2] { 1, 5 },
            ["dimensionaltravel"] = new float[2] { 1, 100 },
        };

        //ActivateShop();

        //add listeners for the button clicks//
        foreach (Button b in UpgradeButtons)
        {
            b.onClick.AddListener(() => ButtonClicked(b.gameObject.transform.parent.gameObject.name.ToLower()));
        }

    }

    private bool OpenShopCD = false;
    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.Tab) && !OpenShopCD)
        {
            OpenShopCD = true;
            ActivateShop();
            Invoke(nameof(ShopOpenCooldown),1);
        }
    }

    private void ShopOpenCooldown()
    {
        OpenShopCD = false;
    }

    private void ActivateShop()
    {
        //If shopCanvas is visible
        if (ShopCanvas.GetComponent<Canvas>().enabled)
        {
            if (Settings.enabled)
                Settings.enabled = false;

            //Disable Shop Canvas//
            ShopCanvas.GetComponent<Canvas>().enabled = false;

            //Set player to moveable and uncamera locked//
            PlayerMovementScript.enabled = true;
            PlayerCamScript.sensX = settingsScript.scroll.value;
            PlayerCamScript.sensY = settingsScript.scroll.value;
            settingsScript.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        //If shopCanvas is not visible
        else
        {
            //Set player to unmoveable and camera locked//
            PlayerMovementScript.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            PlayerMovementScript.SetMoveSpeed(0);
            PlayerMovementScript.enabled = false;
            PlayerCamScript.sensX = 0;
            PlayerCamScript.sensY = 0;
            settingsScript.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //Enable the ShopCanvas//
            ShopCanvas.GetComponent<Canvas>().enabled = true;
        }
    }

    //OnButtonClickFunction//
    private void ButtonClicked(string valName)
    {
        UpgValues.TryGetValue(valName, out float[] Info);
        if (Info[1] <= CurrencySystemScript.Points)
        {
            ShopCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 200, 0, 0.2f);
            Invoke(nameof(FixTintColor), 0.25f);

            //Update UpgradeInfo//
            CurrencySystemScript.Points -= Info[1];
            Info[0] += 1;
            Info[1] = MathF.Round(Info[1] + (Info[1]/2));

            //Upgrade Stat//
            if (valName == "runspeed")
            {
                PlayerMovementScript.setMaxSpeed(PlayerMovementScript.GetMaxSpeed() + 1);
            }
            else if (valName == "jumpheight")
            {
                PlayerMovementScript.jumpForce += 0.25f;
            }
            else if (valName == "cling")
            {
                if (WallSystemScript.climbTime != 0)
                {
                    WallSystemScript.climbTime /= 1.25f; 
                }
            }
            else if (valName == "climb")
            {
                WallSystemScript.climbTime += 0.5f;
            }
            else if (valName == "Wallrun")
            {
                WallSystemScript.minWallRunSpeed -= 0.5f;
                WallSystemScript.WallRunTime += 0.5f;
            }
            else if (valName == "dimensionaltravel")
            {
                DimensionNavigationScript.Unlocked = true;
                UpgradeButtons[5].enabled = false;
                ShopCanvas.transform.GetChild(7).gameObject.GetComponent<Image>().enabled = true;
                Info[1] = 0;
            }
        }
        else
        {
            ShopCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(150, 0, 0, 0.2f);
            Invoke(nameof(FixTintColor), 0.25f);
        }
    }

    private void FixTintColor()
    {
        ShopCanvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }

    private void UpdateUI()
    {
        if (ShopCanvas.enabled)
        {

            ShopCanvas.gameObject.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "(LVL: " + UpgValues["runspeed"][0].ToString() + ")"; // Update Runspeed level text //
            ShopCanvas.gameObject.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "COST: " + UpgValues["runspeed"][1].ToString() + "P"; // Update Runspeed Cost text  //
            ShopCanvas.gameObject.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "(LVL: " + UpgValues["jumpheight"][0].ToString() + ")"; // Update jumpheight level text //
            ShopCanvas.gameObject.transform.GetChild(2).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "COST: " + UpgValues["jumpheight"][1].ToString() + "P"; // Update jumpheight Cost text  //
            ShopCanvas.gameObject.transform.GetChild(3).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "(LVL: " + UpgValues["cling"][0].ToString() + ")"; // Update cling level text //
            ShopCanvas.gameObject.transform.GetChild(3).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "COST: " + UpgValues["cling"][1].ToString() + "P"; // Update cling Cost text  //
            ShopCanvas.gameObject.transform.GetChild(4).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "(LVL: " + UpgValues["climb"][0].ToString() + ")"; // Update climb level text //
            ShopCanvas.gameObject.transform.GetChild(4).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "COST: " + UpgValues["climb"][1].ToString() + "P"; // Update climb Cost text  //
            ShopCanvas.gameObject.transform.GetChild(5).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "(LVL: " + UpgValues["wallrun"][0].ToString() + ")"; // Update wallrun level text //
            ShopCanvas.gameObject.transform.GetChild(5).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "COST: " + UpgValues["wallrun"][1].ToString() + "P"; // Update wallrun Cost text  //
            ShopCanvas.gameObject.transform.GetChild(6).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "COST: " + UpgValues["dimensionaltravel"][1].ToString() + "P"; // Update dimensionaltravel Cost text  //
        }
    }
}
