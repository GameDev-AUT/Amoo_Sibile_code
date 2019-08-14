using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text health;
    [SerializeField] private Text energy;
    [SerializeField] private Text gem;
    [SerializeField] private Text ability;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHealthChange(float health)
    {
        this.health.text = ""+health;
        StartCoroutine(textChange(this.health));
    }
    public void onEnergyChange(float input)
    {
        this.energy.text = "" + input;
        StartCoroutine(textChange(energy));
    }
    public void onabilityChange(float input)
    {
        this.ability.text = "" + input;
        StartCoroutine(textChange(ability));
    }

    IEnumerator textChange(Text text)
    {
        
        int i = 0;
        while (i<=27)
        {
            text.fontSize += 1;
            i++;
            yield return new WaitForSeconds(0.00001f);
        }
        //print(text.fontSize);
        while (i>0)
        {
            text.fontSize -= 1;
            i--;
            yield return new WaitForSeconds(0.00001f);
        }
        yield return null;
    }
}
