using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    public float MaxHealth = 100;
    private float Health;

    public float RegenAmount = 2;
    public float RegenTime = 0.2f;
    public float RegenDelay = 5;

    [Header("fallDamageInfo")]
    public float FallDamageStartPoint = 10f;
    public float DmgMultiplier = 1.2f;

    [Header("PlayerInfo")]
    public PlayerMovement PlayerMovement;
    public GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Death();
        }
    }

    private void FallDamage(Vector3 StartPos)
    {
        float ChangeInHeight = Mathf.Floor(StartPos.y - transform.position.y);

        if (ChangeInHeight >= FallDamageStartPoint)
        {
            Health -= 5 - (ChangeInHeight - FallDamageStartPoint) * DmgMultiplier;
        }

    }

    private void Death()
    {
        
    }
}
