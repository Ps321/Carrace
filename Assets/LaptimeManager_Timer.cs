using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaptimeManager_Timer : MonoBehaviour
{
   
 public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string Millidisplay;

    public GameObject MinuteBox;

    public GameObject SecondBox;
    public GameObject MilliBox;
     public GameObject laptext;

     public GameObject Winscreen;

     public GameObject Loosescreen;


   
    private void Start() {
        PlayerPrefs.SetInt("lap",0);
        PlayerPrefs.SetInt("Totallap",2);
        PlayerPrefs.SetInt("playercheckpoints",0);
     //    laptext.GetComponent<Text>().text=(PlayerPrefs.GetInt("lap")) +"/"+(PlayerPrefs.GetInt("Totallap"));
    }
    void Update()
    {
        MilliCount+=Time.deltaTime*10;
        Millidisplay=MilliCount.ToString("F0");

        MilliBox.GetComponent<Text>().text=""+Millidisplay;

    if(MilliCount>10){
        MilliCount=0;
        SecondCount+=1;
    }
    if(SecondCount<=9){
       SecondBox.GetComponent<Text>().text="0"+SecondCount+".";
    }
    else{
         SecondBox.GetComponent<Text>().text=""+SecondCount+".";
    }

    if(SecondCount>=60){
        SecondCount=0;
        MinuteCount+=1;
        if(MinuteCount==3){
            if(LapCompleteManager_timer.lap>=2){
                Winscreen.SetActive(true);
            }
            else{
                Loosescreen.SetActive(true);
            }
        }
    }

    if(MinuteCount<=9){
         MinuteBox.GetComponent<Text>().text="0"+MinuteCount+":";
    }else{
        MinuteBox.GetComponent<Text>().text=""+MinuteCount+":";
    }


    }
}
