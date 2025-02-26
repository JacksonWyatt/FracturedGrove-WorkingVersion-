using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class CurrencySystem : MonoBehaviour
{

    public float Points;
    public GameObject CurrencyTxt;
    public Canvas ShopScreen;
    public KeyCode ShopKey = KeyCode.Tab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    //Update the UI for currency//
    private void UpdateUI()
    {
        if (CurrencyTxt.GetComponent<Text>().text != "P: " + Points)
        {
            CurrencyTxt.GetComponent<Text>().text = "P: " + Points;
        }
    }

    // Add points to player//
    public void AddPoints(float p)
    {
        Points += p;
    }

    //Remove points if have enough -- Return false elsewise
    public bool RemovePoints(float p)
    {
        if (Points >= p)
        {
            Points -= p;
            return true;
        }
        return false;
    }
}
