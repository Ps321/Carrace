using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LapCompleteTimeMagaer : MonoBehaviour
{

  public GameObject MinuteBox;

  public GameObject SecondBox;
  public GameObject MilliBox;
  public GameObject laptext;
  public GameObject Winscreen;
  public GameObject Loosescreen;
  int lap = 0;
  int ailap = 0;
  float timer = 30.0f;
  bool start = false;

  private void Start()
  {
    StartCoroutine(updateindpoint());
  }

  private void Update()
  {

    if (start)
    {
      timer = timer - Time.deltaTime;
      if (timer < 0)
      {
        start = false;
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag != "AIcar01")
    {
      if (LapTimeManager.SecondCount <= 9)
      {
        SecondBox.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
      }
      else
      {
        SecondBox.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
      }



      if (LapTimeManager.MinuteCount <= 9)
      {
        MinuteBox.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";
      }
      else
      {
        MinuteBox.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";
      }
      MilliBox.GetComponent<Text>().text = "" + LapTimeManager.Millidisplay;

      lap++;
      if (lap == 1)
      {
        MinuteBox.GetComponent<Text>().text = "--:";
        SecondBox.GetComponent<Text>().text = "--.";
        MilliBox.GetComponent<Text>().text = "-";
      }

      //laptext.GetComponent<Text>().text=(PlayerPrefs.GetInt("lap")+1) +"/"+(PlayerPrefs.GetInt("Totallap"));

      if (PlayerPrefs.GetInt("Lap") < PlayerPrefs.GetInt("Totallap"))
      {
        laptext.GetComponent<Text>().text = lap + "/2";


      }
      StartCoroutine(lapreset());
    }
    else
    {
      if (!start)
      {
        ailap++;
        start = true;
      }
    }


    if (ailap > 2 && lap <= 2)
    {
      Loosescreen.SetActive(true);
    }
    if (lap > 2 && ailap <= 2)
    {
      Winscreen.SetActive(true);
      StartCoroutine(updateindpoint());
    }
  }
  IEnumerator updateindpoint()
  {
    WWWForm form = new WWWForm();

    form.AddField("email", PlayerPrefs.GetString("email"));




    using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/updateindpoints.php", form))
    {
      yield return www.SendWebRequest();

      if (www.result != UnityWebRequest.Result.Success)
      {
        // SceneManager.LoadScene(0);
        Debug.Log(www.error);
      }
      else
      {
        //Debug.Log(www.downloadHandler.text);
        string s = www.downloadHandler.text.Trim();
        if (s == "Error")
        {
          // t.text = "User Already Exist";
          // t.color = Color.red;
        }

        else
        {

          Debug.Log(s); //Output 1

        }
      }

    }
  }

  IEnumerator lapreset()
  {
    this.gameObject.GetComponent<BoxCollider>().enabled = false;
    yield return new WaitForSeconds(0.2f);
    LapTimeManager.MinuteCount = 0;
    LapTimeManager.SecondCount = 0;
    LapTimeManager.MilliCount = 0;
    yield return new WaitForSeconds(30f);
    this.gameObject.GetComponent<BoxCollider>().enabled = true;
  }




  public void loadmainscene()
  {
    SceneManager.LoadScene(11);
  }
}
