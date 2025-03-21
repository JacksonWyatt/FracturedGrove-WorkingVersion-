using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyObject : MonoBehaviour
{
    [Header("SpawnInformation")]
    public bool respawnable;
    public float respawntime;
    private Vector3 SpawnLocation;

    [Header("CurrencyInformation")]
    public float Amount;
    public CurrencySystem CurrencySystem;

    [Header("ObjectInfo")]
    public CapsuleCollider Hitbox;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLocation = transform.position;
        CurrencySystem = GameObject.Find("PlayerV2").GetComponent<CurrencySystem>();
        StartCoroutine(animate());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        animate();   
    }

    /*
     void OnCollisionEnter(Collision collision)
    {
        //Check if hit the player
        print(collision.collider.gameObject.name);
        if (collision.collider.gameObject.name == "PlayerV2")
        {
            collect();
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.name == "PlayerObj")
        {
            collect();
        }
    }

    private IEnumerator animate()
    {
        float changeFactor = 1;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y+3, transform.eulerAngles.z);
            if (transform.position.y >= SpawnLocation.y + 0.2f)
            {
                changeFactor = -1;
            }
            else if (transform.position.y <= SpawnLocation.y)
            {
                changeFactor = 1;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + (changeFactor * .01f), transform.position.z);
        }
    }

    private void collect()
    {
        CurrencySystem.AddPoints(Amount);
        Hitbox.enabled = false;
        transform.gameObject.SetActive(false);
        if (respawnable)
        {
            Invoke(nameof(respawn), respawntime);
        }
    }

    private void respawn()
    {
        transform.gameObject.SetActive(true);
        Hitbox.enabled = true;
    }

    public Vector3 getSpawnLocation()
        { return SpawnLocation; }

    public void setSpawnLocation(Vector3 Set)
        { SpawnLocation = Set; }
}
