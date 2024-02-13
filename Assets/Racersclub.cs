using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racersclub : MonoBehaviour
{
    public GameObject popup;
    public GameObject rcgame;
    public GameObject Loading;
    void Start()
    {
        
        Debug.Log("rcrace" + PlayerPrefs.GetInt("rc_balance"));
        if (PlayerPrefs.GetInt("rc_balance") > 0)
        {
            rcgame.SetActive(true);
        }
        else
        {
            popup.SetActive(true);
        }
        Loading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
