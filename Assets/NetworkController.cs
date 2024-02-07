using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Rendering;


public class NetworkController : MonoBehaviourPunCallbacks
{

    public GameObject loading;
    public Text indpoints;
    // Start is called before the first frame update
    void Start()
    {
        loading.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        StartCoroutine(Signup1());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + "server");
    }

    IEnumerator Signup1()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", PlayerPrefs.GetString("email"));




        using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/score.php", form))
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
                    string[] result = s.Split('-');
                    indpoints.text = result[4];
                }
            }
            loading.SetActive(false);
        }
    }
}
