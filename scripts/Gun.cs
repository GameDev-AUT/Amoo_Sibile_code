using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float damage = 15;
    private GameObject player;
    private bool waitTilEndOfAnimation = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("projectile"))
            return;
        if(!this.gameObject.transform.IsChildOf(other.gameObject.transform))
           if (player.GetComponent<Player>().Attack)
          { 
            other.gameObject.GetComponent<Player>().healthAdd(-damage);
            Debug.Log("damage");
          }
    }

    private void waitForAnimationReset()
    {
        waitTilEndOfAnimation = !waitTilEndOfAnimation;
    }

    public GameObject Player
    {
        get => player;
        set => player = value;
    }
}
