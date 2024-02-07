using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown_training : MonoBehaviour
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
     SpawnVehicle.player.GetComponent<Rigidbody>().isKinematic=true;
    yield return new WaitForSeconds(0.5f);

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
     SpawnVehicle.player.GetComponent<Rigidbody>().isKinematic=false;
    CountDown.GetComponent<Text>().text="";
   

    startsound.clip=gosound;
    startsound.Play();
    controlpanel.SetActive(false);
    //aiplayer.SetActive(true);
   // aiplayerclone.SetActive(false);
   // laptimer.SetActive(true);

}
     public void loadmainscene(){
    SceneManager.LoadScene(0);
   }
}
