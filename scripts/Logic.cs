using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{

    private static string groupType = "";

    [SerializeField] private GameObject blueTeam;
    [SerializeField] private GameObject redTeam;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    public void setGroup(string name)
    {
        if (name.Equals(GroupsTypes.Red))
        {
            blueTeam.SetActive(false);
            redTeam.SetActive(true);            
        }
        else if(name.Equals(GroupsTypes.Blue))
        {
            blueTeam.SetActive(true);
            redTeam.SetActive(false);
        }
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
