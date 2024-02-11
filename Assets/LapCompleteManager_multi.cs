using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;
using UnityEngine.Networking;

public class LapCompleteManager_multi : MonoBehaviourPun
{

  public GameObject MinuteBox;

  public GameObject SecondBox;
  public GameObject MilliBox;
  public GameObject laptext;
  public GameObject Winscreen;
  public GameObject Loosescreen;
  public static int lap = 0;
  int ailap = 0;
  float timer = 30.0f;
  bool start = false;

  private void Start()
  {
    StartCoroutine(Deductindcoins());
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



    lap++;

    StartCoroutine(lapreset());


    int i = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);

    this.gameObject.GetComponent<BoxCollider>().enabled = false;
    if (lap == 1 && other.gameObject.GetComponent<PhotonView>().IsMine)
    {
      Winscreen.SetActive(true);
      StartCoroutine(updateindpoint());
    }
    else if (lap == 1)
    {
      Loosescreen.SetActive(true);
    }
  }

  IEnumerator lapreset()
  {
    this.gameObject.GetComponent<BoxCollider>().enabled = false;

    yield return new WaitForSeconds(60f);
    this.gameObject.GetComponent<BoxCollider>().enabled = true;
  }




  public void loadmainscene()
  {
    SceneManager.LoadScene(11);
  }
  IEnumerator updateindpoint()
  {
    WWWForm form = new WWWForm();

    form.AddField("email", PlayerPrefs.GetString("email"));
    form.AddField("value", 600);




    using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/updateindcoins.php", form))
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
  IEnumerator Deductindcoins()
  {
    WWWForm form = new WWWForm();

    form.AddField("email", PlayerPrefs.GetString("email"));
    form.AddField("value", 100);




    using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/deductindcoins.php", form))
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
}

