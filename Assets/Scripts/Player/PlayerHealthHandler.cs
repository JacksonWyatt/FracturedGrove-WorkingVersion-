using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    public float MaxHealth = 100;
    private float Health;
    private float PrevRunHealth;

    public float RegenAmount = 2;
    public float RegenTime = 0.2f;
    public float RegenDelay = 5;
    private IEnumerator RegenFunction;

    [Header("fallDamageInfo")]
    private bool falling = false;
    public float FallDamageStartPoint = 10f;
    public float DmgMultiplier = 1.2f;

    [Header("PlayerInfo")]
    public PlayerMovement PlayerMovement;
    public GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        RegenFunction = Regen();
    }

    // Update is called once per frame
    private bool clickDebounce = false;
    void Update()
    {
        //Test hp removal and regen//
        if ( Input.GetKey(KeyCode.X) && !clickDebounce)
        {
            clickDebounce = true;
            Health -= MaxHealth-1;
            print(Health);
            StartCoroutine(ResetDebounce());
        }
        //Check if the user is at 0 HP and kill them
        if (Health <= 0)
        {
            Death();
        }

        //Check if the user is loosing HP and remove regen if so
        if (Health < PrevRunHealth)
        {
            CancelInvoke();
            StopCoroutine(RegenFunction);
        }

        //Check if the HP of the user is below max and start heal function if so/ Otherwise stop heal function and make hp maxHp
        if (Health < MaxHealth)
        {
            Invoke(nameof(RegenDelayFunc), RegenDelay);
        }
        else if (Health >= MaxHealth)
        {
            Health = MaxHealth;
            RegenFunction = Regen();
        }

        PrevRunHealth = Health;

        //FallDamage//
        if (PlayerMovement.state == PlayerMovement.MovementState.air && !falling )
        {
            print("falling");
            falling = true;
            StartCoroutine(FallDamageHelper(PlayerObject.transform.position));
        }
    }

    private IEnumerator ResetDebounce()
    {
        yield return new WaitForSeconds(0.5f);
        clickDebounce = false;
    }
    
    private void RegenDelayFunc()
    {
        StartCoroutine(RegenFunction);
    }
    private IEnumerator Regen()
    {
        while (Health < MaxHealth)
        {
            yield return new WaitForSeconds(RegenTime);
            Health += RegenAmount;
        }
    }

    private void FallDamage(UnityEngine.Vector3 StartPos)
    {
        while(PlayerMovement.state == PlayerMovement.MovementState.air)
        {
            print("Waiting for Ground");
        }
        float ChangeInHeight = Mathf.Floor(StartPos.y - transform.position.y);

        if (ChangeInHeight >= FallDamageStartPoint)
        {
            Health -= 5 - (ChangeInHeight - FallDamageStartPoint) * DmgMultiplier;
        }
        falling = false;
    }

    private IEnumerator FallDamageHelper(UnityEngine.Vector3 startPos)
    {
        while (PlayerMovement.state == PlayerMovement.MovementState.air)
            yield return new WaitForSeconds(0.5f);
        FallDamage(startPos);
    }

    private void Death()
    {
        Health = 0;
    }

    public float GetHealth()
    {
        return Health;
    }
    
    public void SetHealth(float x)
    {
        Health = x;
    }
}
