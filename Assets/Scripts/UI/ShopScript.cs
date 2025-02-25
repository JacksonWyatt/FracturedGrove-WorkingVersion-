using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
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

    private Dictionary<string, int[]> UpgValues;

    public Button[] UpgradeButtons; 
    // Start is called before the first frame update
    void Start()
    {
        //Set Values in upgradeDictionary
        UpgValues = new Dictionary<string, int[]>()
        {
            ["runspeed"] = new int[2] { 1, 5 },
            ["jumpheight"] = new int[2] { 1, 5 },
            ["cling"] = new int[2] { 1, 5 },
            ["climb"] = new int[2] { 1, 5 },
            ["WallRun"] = new int[2] { 1, 5 },
            ["dimensionaltravel"] = new int[2] { 1, 5 },
        };
        

    }

    private bool OpenShopCD = false;
    // Update is called once per frame
    void Update()
    {
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
            PlayerCamScript.sensX = 0;
            PlayerCamScript.sensY = 0;
            settingsScript.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //Enable the ShopCanvas//
            ShopCanvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
