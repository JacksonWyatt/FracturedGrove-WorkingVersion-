using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DimensionNavigation : MonoBehaviour
{
    [Header("Used Classes")]
    public UIClass ui;
    public PlayerMovement mover;
    public DimensionalObj dim;

    [Header("Info")]
    public bool Unlocked;
    public KeyCode PhaseKey = KeyCode.E;
    public float switchCooldown;
    public bool canSwitchModes;
    public bool in4D;
    public float sphereRadius;
    public float minSpeed;

    [Header("Visuals")]
    public Color fogColor;
    private Color prevFogColor;
    public Color AmbienceColor;
    private Color prevambienceColor;
    public TrailRenderer[] trailRenderers;
    public Gradient[] gradientChange;
    private Gradient[] trailGradients;

    [Header("Layers")]
    public LayerMask whatIsPassThrough;
    public LayerMask whatIs4D;


    // Start is called before the first frame update
    void Start()
    {
        Unlocked = false;
        prevFogColor = RenderSettings.fogColor;
        prevambienceColor = RenderSettings.ambientLight;
        trailGradients = new Gradient[trailRenderers.Length];
        for(int i = 0; i < trailGradients.Length; i++)
        {
            trailGradients[i] = trailRenderers[i].colorGradient;
        }


        mover = GetComponent<PlayerMovement>();
        if (IsIn4D())
        {
            ui.Negate();
        }
        //else 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PhaseKey) && canSwitchModes && Unlocked) 
        {
            print("trying to switch modes");
            SwitchMode();
        }
    }

    public void SwitchMode()
    {
        //Change the world visuals//
        if (!IsIn4D())
        {
            RenderSettings.fogColor = fogColor;
            RenderSettings.ambientLight = AmbienceColor;
            DynamicGI.UpdateEnvironment();
            for (int i = 0;i < trailRenderers.Length;i++)
            {
                trailRenderers[i].colorGradient = gradientChange[i];
            }
        }
        else
        {
            RenderSettings.fogColor = prevFogColor;
            RenderSettings.ambientLight = prevambienceColor;
            DynamicGI.UpdateEnvironment();
            for (int i = 0; i < trailRenderers.Length; i++)
            {
                trailRenderers[i].colorGradient = trailGradients[i];
            }
        }
        //-----------------------//

        canSwitchModes = false;
        in4D = !in4D;
        ui.Negate();
        print("switched modes");
        Invoke(nameof(ResetSwitchCooldown), switchCooldown);
        
    }

    public void ResetSwitchCooldown()
    {
        canSwitchModes = true;
    }

    public bool IsIn4D()
    {
        return in4D;
    }


}
