using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float health = 100;
    private float energy = 0;
    private int gem = 300;
    private bool alive = true;
    private bool attack;
    private int ability = 0;
    private UIManager UiManager;
    private CustomeNetworkBehavior customeNetworkBehavior;
    [SerializeField] private int gun;
    void Start()
    {
        
        customeNetworkBehavior = GetComponent<CustomeNetworkBehavior>();
        selectGun();
        gun = 0;
        if (customeNetworkBehavior.isLocalPlayer)
        {
            UiManager = GameObject.FindWithTag("Manager").GetComponent<UIManager>();
            StartCoroutine(update());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void selectGun()
    {
        GameObject shop = GameObject.Find("shop");
        shop.GetComponent<equeipment_Controller>().buyGun(this.gameObject, gun);
    }
    
    
    public void healthAdd(float health)
    {
        this.health += health;
        if(this.health < 0)
        {
            AnimationState animationState =  new AnimationState("death", 1, false);
            animationState.AsABool = true;
            GetComponent<AnimationHandler>().setAnimation(animationState);
        }
        if (customeNetworkBehavior.isLocalPlayer)
        {
            UiManager.onHealthChange(this.health);
        }
    }
    
    public void updateHealth(float health)
    {
        this.health = health;
        print(this.name + " "+health);
        if(this.health < 0){
            GetComponent<AnimationHandler>().setAnimation(new AnimationState("death",1,true));
        }
        if (customeNetworkBehavior.isLocalPlayer)
              UiManager.onHealthChange(health);
    }
    

    
    IEnumerator update()
    {
        int i = 0;
        UiManager.onabilityChange(ability);
        UiManager.onHealthChange(health);
        while (alive)
        {
            i++;
            energy+=10;
            UiManager.onEnergyChange(energy);

                if (i == 300)
            {
                ability++;
                UiManager.onabilityChange(ability);
                i = 0;
            }
            yield return new WaitForSeconds(5f);
        }
        
    }

    public bool Attack
    {
        get => attack;
        set => attack = value;
    }

    public float Health
    {
        get => health;
        set => health = value;
    }
}
