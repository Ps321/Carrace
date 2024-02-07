using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class LapCompleteManager_multi : MonoBehaviourPun
{
    
    public GameObject MinuteBox;

    public GameObject SecondBox;
    public GameObject MilliBox;
    public GameObject laptext;
    public GameObject Winscreen;
    public GameObject Loosescreen;
    public static int lap=0;
    int ailap=0;
    float timer=30.0f;
    bool start=false;

private void Update() {
    
    if(start){
timer=timer-Time.deltaTime;
if (timer<0){
    start=false;
}
    }
}

   private void OnTriggerEnter(Collider other) {
   
  
    
    lap++;
    
      StartCoroutine(lapreset());
   
   
   int i= Array.IndexOf(PhotonNetwork.PlayerList,PhotonNetwork.LocalPlayer);

 this.gameObject.GetComponent<BoxCollider>().enabled=false;
   if(lap==2 && other.gameObject==GamesetupCotroller.players[i]){
    Winscreen.SetActive(true);
   }
   else if(lap==2){
    Loosescreen.SetActive(true);
   }
   }

   IEnumerator lapreset(){
    this.gameObject.GetComponent<BoxCollider>().enabled=false;
   
     yield return new WaitForSeconds(60f);
      this.gameObject.GetComponent<BoxCollider>().enabled=true;
   }

         

 
  public void loadmainscene(){
    SceneManager.LoadScene(0);
   }
}
