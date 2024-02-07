using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LapCompleteManager_timer : MonoBehaviour
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
   
  /*  if(LapTimeManager.SecondCount<=9){
       SecondBox.GetComponent<Text>().text="0"+LapTimeManager.SecondCount+".";
    }
    else{
         SecondBox.GetComponent<Text>().text=""+LapTimeManager.SecondCount+".";
    }

    

    if(LapTimeManager.MinuteCount<=9){
         MinuteBox.GetComponent<Text>().text="0"+LapTimeManager.MinuteCount+":";
    }else{
        MinuteBox.GetComponent<Text>().text=""+LapTimeManager.MinuteCount+":";
    }
        MilliBox.GetComponent<Text>().text=""+LapTimeManager.Millidisplay; 

      lap++;
      if(lap==1){
        MinuteBox.GetComponent<Text>().text="--:";
        SecondBox.GetComponent<Text>().text="--.";
        MilliBox.GetComponent<Text>().text="-";
      }

    //laptext.GetComponent<Text>().text=(PlayerPrefs.GetInt("lap")+1) +"/"+(PlayerPrefs.GetInt("Totallap"));
  
    if(PlayerPrefs.GetInt("Lap")<PlayerPrefs.GetInt("Totallap")){
    laptext.GetComponent<Text>().text=lap +"/2";
*/
    
    lap++;
      StartCoroutine(lapreset());
   
   
   


   if(lap==2 && LaptimeManager_Timer.MinuteCount<3){
    Winscreen.SetActive(true);
   }
   }

   IEnumerator lapreset(){
    this.gameObject.GetComponent<BoxCollider>().enabled=false;
   
     yield return new WaitForSeconds(30f);
      this.gameObject.GetComponent<BoxCollider>().enabled=true;
   }

         

 
  public void loadmainscene(){
    SceneManager.LoadScene(0);
   }
}
