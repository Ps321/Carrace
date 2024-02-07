using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownMulti : MonoBehaviour
{
     public GameObject CountDown;
    public AudioSource startsound;

   // public GameObject Laptimer;
    public AudioClip gosound;

    public GameObject controlpanel;

    public GameObject laptimer;

    public GameObject aiplayer;
    public GameObject aiplayerclone;
    
    void Start()
    {
        StartCoroutine(Countstart());    
    }

IEnumerator Countstart(){
    yield return new WaitForSeconds(0.5f);
  //  SpawnVehicle.player.GetComponent<Rigidbody>().isKinematic=true;
    CountDown.GetComponent<Text>().text="3";
    startsound.Play();
    CountDown.SetActive(true);
    yield return new WaitForSeconds(1.05f);
    CountDown.SetActive(false);
    CountDown.GetComponent<Text>().text="2";
    startsound.Play();
    CountDown.SetActive(true);
    yield return new WaitForSeconds(1.05f);
    CountDown.SetActive(false);
    CountDown.GetComponent<Text>().text="1";
    startsound.Play();
    CountDown.SetActive(true);
    yield return new WaitForSeconds(1.05f);
    // SpawnVehicle.player.GetComponent<Rigidbody>().isKinematic=false;
    CountDown.GetComponent<Text>().text="";

    
   

    startsound.clip=gosound;
    startsound.Play();
    controlpanel.SetActive(false);
   
    laptimer.SetActive(true);

}
     public void loadmainscene(){
    SceneManager.LoadScene(0);
   }
}
