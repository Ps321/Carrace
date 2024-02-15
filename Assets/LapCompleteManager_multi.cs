using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Realtime;

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
  float timer = 60.0f;
  bool start = true;
  public int winner = 0;

  public Text winpos;
  public Text amount;
  int oncedone = 0;

  private void Start()
  {

    if (PlayerPrefs.GetInt("gamemode") == 2)
    {
      StartCoroutine(Deductindcoins());
    }
    else if (PlayerPrefs.GetInt("gamemode") == 3)
    {
      // StartCoroutine(Deductracercoins());
    }

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





    // StartCoroutine(lapreset());

    Debug.Log("aaya" + lap);
    Debug.Log("aaya" + other.gameObject.transform.parent.transform.parent.name);
    int i = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);


    if (lap == 0 && other.gameObject.transform.parent.transform.parent.GetComponent<PhotonView>().IsMine && oncedone == 0 && start == false)
    {
      lap++;
      winner = winner + 1;
      winpos.text = (winner).ToString();
      if (winner == 1)
      {
        amount.text = "300";
      }
      else if (winner == 2)
      {
        amount.text = "200";
      }
      else if (winner == 3)
      {
        amount.text = "100";
      }
      else
      {
        amount.text = "0";
      }
      Winscreen.SetActive(true);
      Updatecoins();
      gameObject.GetPhotonView().RPC("SetwinnerNumber", RpcTarget.AllBuffered, winner);
      oncedone = 1;
    }
    // else if (lap == 1)
    // {
    //   Loosescreen.SetActive(true);
    // }
  }

  IEnumerator lapreset()
  {
    this.gameObject.GetComponent<BoxCollider>().enabled = false;

    yield return new WaitForSeconds(60f);
    this.gameObject.GetComponent<BoxCollider>().enabled = true;
  }

  [PunRPC]
  public void SetwinnerNumber(int number)
  {
    winner++;
    // StartCoroutine(ReadPlayerInfo());



  }
  public void Updatecoins()
  {
    if (PlayerPrefs.GetInt("gamemode") == 2 && int.Parse(amount.text) > 0)
    {
      StartCoroutine(updateindpoint(int.Parse(amount.text)));
    }
    else if (PlayerPrefs.GetInt("gamemode") == 3 && int.Parse(amount.text) > 0)
    {
      StartCoroutine(updateracerpoint(int.Parse(amount.text)));
    }
  }

  public void loadmainscene()
  {
    SceneManager.LoadScene(11);
  }
  IEnumerator updateindpoint(int points)
  {
    WWWForm form = new WWWForm();

    form.AddField("email", PlayerPrefs.GetString("email"));
    form.AddField("value", points);




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
  IEnumerator updateracerpoint(int points)
  {
    WWWForm form = new WWWForm();

    form.AddField("email", PlayerPrefs.GetString("email"));
    form.AddField("value", points);




    using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/updateracerpoints.php", form))
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
  IEnumerator Deductracercoins()
  {
    WWWForm form = new WWWForm();

    form.AddField("email", PlayerPrefs.GetString("email"));
    form.AddField("value", 100);




    using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/deductracerpoints.php", form))
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

