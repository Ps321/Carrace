using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INDpoints : MonoBehaviour
{
    public Text playername;
    public Text Status;
    public GameObject Loading;
    public UIController uIController;
    private string[] indianNames = {
        "Aarav",
        "Aditi",
        "Arjun",
        "Divya",
        "Gaurav",
        "Isha",
        "Kiran",
        "Maya",
        "Neha",
        "Rahul",
        "Sanya",
        "Varun",
        "Zara",
        "Yash",
        "Tanvi"
    };

    void Start()
    {
        StartCoroutine(updatedata());
    }

    IEnumerator updatedata()
    {
        yield return new WaitForSeconds(4.0f);
        playername.text = indianNames[Random.Range(0, indianNames.Length)];
        Status.text = "Ready";
        Loading.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        uIController.loadgame();
    }

    void Update()
    {

    }
}
