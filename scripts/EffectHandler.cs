using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private GameObject vikingPower;

    private CustomeNetworkBehavior CustomeNetworkBehavior;
    // Start is called before the first frame update
    void Start()
    {
        CustomeNetworkBehavior = this.GetComponent<CustomeNetworkBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildEffect(string power,Vector3 startLocation)
    {
        switch (power)
        {
            case "vikingPower":
                GameObject gameObject = Instantiate(vikingPower, startLocation, vikingPower.transform.rotation);
                Destroy(gameObject,3);
                break;
        }
     //   CustomeNetworkBehavior.CmdEffectToServer(power,startLocation);//TODO
     CustomeNetworkBehavior.CmdEffectToServer(power);
    }
    public void updateEffect(string power,Vector3 startLocation)
    {
        switch (power)
        {
            case "vikingPower":
                GameObject gameObject = Instantiate(vikingPower, startLocation, vikingPower.transform.rotation);
                Destroy(gameObject,3);
                break;
        }
    }
}
