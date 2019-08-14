using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private GameObject power = null;
    [SerializeField] private GameObject skill = null;

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

    public void buildEffect(Vector3 startLocation,string ability)
    {
        if (ability.Equals("attack"))
        {
            if (power == null)
                return;
            GameObject gameObject =
                Instantiate(power, startLocation, power.transform.rotation * this.transform.rotation);
            CustomeNetworkBehavior.CmdEffectToServer(ability);
            Destroy(gameObject, 3);
        }
        else
        {
            if (skill == null)
                return;
            GameObject gameObject =
                Instantiate(skill, startLocation, skill.transform.rotation * this.transform.rotation);
            CustomeNetworkBehavior.CmdEffectToServer(ability);
            Destroy(gameObject, 3);
        }

        //   CustomeNetworkBehavior.CmdEffectToServer(power,startLocation);//TODO
    }
    public void updateEffect(Vector3 startLocation,string ability)
    {
        
                if (ability.Equals("attack"))
                {
                    GameObject gameObject =
                        Instantiate(power, startLocation, power.transform.rotation * this.transform.rotation);
                    Destroy(gameObject, 3);
                }
                else
                {
      
                    GameObject gameObject =
                        Instantiate(skill, startLocation, skill.transform.rotation * this.transform.rotation);
                    Destroy(gameObject, 3);
                }
    }
}
