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
    public float Duration;
    private float DurationLeft;
    public KeyCode PhaseKey = KeyCode.E;
    public float SwitchBurnoutCD;
    private bool SwitchBurnedOut;
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

    private IEnumerator DurationTimer;

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
        DurationLeft = Duration;
        DurationTimer = DimensionDuration();

        if (IsIn4D())
        {
            ui.Negate();
        }
        //else 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PhaseKey) && canSwitchModes && Unlocked && !SwitchBurnedOut) 
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
            //If transferring to 4D//
            RenderSettings.fogColor = fogColor;
            RenderSettings.ambientLight = AmbienceColor;
            DynamicGI.UpdateEnvironment();
            print("ChangeVisual");
            for (int i = 0;i < trailRenderers.Length;i++)
            {
                trailRenderers[i].colorGradient = gradientChange[i];
            }

            print("RunningCoroutine");
            StartCoroutine(DurationTimer);
        }
        else
        {
            //if transferring to 3D//
            RenderSettings.fogColor = prevFogColor;
            RenderSettings.ambientLight = prevambienceColor;
            DynamicGI.UpdateEnvironment();

            print("ChangedVisualOFF");
            for (int i = 0; i < trailRenderers.Length; i++)
            {
                trailRenderers[i].colorGradient = trailGradients[i];
            }

            print("ChangedDurationOFF");
            StopCoroutine(DurationTimer);
            DurationTimer = DimensionDuration();
            DurationLeft = Duration;
        }
        //-----------------------//


        canSwitchModes = false;
        in4D = !in4D;
        ui.Negate();
        print("switched modes");
        Invoke(nameof(ResetSwitchCooldown), switchCooldown);
        
    }

    private IEnumerator DimensionDuration()
    {
        print("running");
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(Duration / 20);
            print(DurationLeft);
            DurationLeft -= Duration/20;
        }

        if (in4D)
        {
            print("Burnedout");
            SwitchBurnedOut = true;
            SwitchMode();
            Invoke(nameof(UnBurnoutTravel), SwitchBurnoutCD);

        }
        

    }

    private void UnBurnoutTravel()
    {
        SwitchBurnedOut = false;
        DurationLeft = Duration;
    }

    public void ResetSwitchCooldown()
    {
        canSwitchModes = true;
    }

    public bool IsIn4D()
    {
        return in4D;
    }


    public float GetDurationLeft()
    {
        return DurationLeft;
    }

}
