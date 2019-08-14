
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectorPLayer : MonoBehaviour
{
    [SerializeField] private Text charName;
    [SerializeField] private Text num;
    [SerializeField] private GameObject selectionGui;
    [SerializeField] private GameObject playerUI;
    private bool updating = false;
    private bool selected = false;
    public void OnSelect()
    {
        if (charName.IsActive())
        {
            GetComponent<SelectionPlayerGui>().@select(charName.text);
            selectionGui.SetActive(false);
            selected = true;
            selectionGui.SetActive(false);
            playerUI.SetActive(true);
        }
        print("dsdsa");
    }
    void StartCounting()
    {
        StartCoroutine(chooseDefault());
    }

    IEnumerator chooseDefault()
    {
        int i = 60;
        while (i>=0 && !selected)
        {
            num.text = "" + i;
            yield return new WaitForSeconds(1);
            i--;
        }
        
        selectionGui.SetActive(false);
        playerUI.SetActive(true);
        yield return new WaitForSeconds(1);
        GameObject.FindWithTag("local").GetComponent<movment>().enabled = true;
        GameObject.FindWithTag("local").GetComponent<Player>().enabled = true;

    }
    public void setUISelcetion(bool enable)
    {
        playerUI.SetActive(!enable);
        selectionGui.SetActive(enable);
        Invoke("setPlayerMovmonet",1);
        StartCounting();
    }

    public void show()
    {
        selected = false;
        setUISelcetion(true);
    }
    private void setPlayerMovmonet()
    {
       GetComponent<SelectionPlayerGui>().initial();

    }
}
