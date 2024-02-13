using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beforephoton : MonoBehaviour
{
    public GameObject indcoinscreen;
    public GameObject insufficentbalance;
    void Start()
    {
        PlayerPrefs.SetInt("ic_balance",1000);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void indcoincheck()
    {
        //   indcoinscreen.SetActive(true);
        //     indcoinscreen.GetComponent<Photonscript>().ConnectToPhoton();
        if (PlayerPrefs.GetInt("ic_balance") < 100)
        {
            insufficentbalance.SetActive(true);
        }
        else
        {
            indcoinscreen.SetActive(true);
            indcoinscreen.GetComponent<Photonscript>().ConnectToPhoton();
        }
    }
}
