using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equeipment_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    
    //be care about if you delete the data in public field of engin , you aproximately has been done :)

    public GameObject[] allEqueipments;
    public Vector3[] locationInHand;
    public Vector3[] RotationInHand;
    
    void Start()
    {
        
    }

    public void buyGun(GameObject  body ,int gunNumber)
    {
        Transform[] childs = body.GetComponentsInChildren<Transform>();
        Transform hand = null;
        Debug.Log(childs.Length);
        for(int j=0;j<childs.Length;j++)
        {
            if (childs[j].GetComponent<Transform>().name == "hand_r")
            {
                Debug.Log("we achive right hand => have fun => :)");
                hand = childs[j].GetComponent<Transform>();
            }   
        }
        Debug.Log(hand.name);
        if (!hand)
        {
            //this will be finished if cant find hand_r in your body
            Debug.Log("not found hand_r");
            return;
        }
        Quaternion test = Quaternion.Euler(RotationInHand[gunNumber]);
        GameObject gun = Instantiate( allEqueipments[gunNumber],hand);
        gun.GetComponent<Gun>().Player = body;
        gun.transform.localPosition = locationInHand[gunNumber];
        gun.transform.localRotation =Quaternion.Euler( RotationInHand[gunNumber]);
        Debug.Log("sold");


    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
