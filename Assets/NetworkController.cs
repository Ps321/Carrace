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
    public Text indCoins;
    public Text racerspoints;
    public Text indpoints_gm;
    public Text indCoins_gm;
    public Text racerspoints_gm;
    // Start is called before the first frame update
    void Start()
    {
        loading.SetActive(false);

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
                    PlayerPrefs.SetString("name", result[0]);
                    indpoints.text = result[4];
                    indCoins.text = result[5];
                    racerspoints.text = result[6];
                    indpoints_gm.text = result[4];
                    indCoins_gm.text = result[5];
                    racerspoints_gm.text = result[6];
                    PlayerPrefs.SetInt("ip_balance", int.Parse(result[4]));
                    PlayerPrefs.SetInt("ic_balance", int.Parse(result[5]));
                    PlayerPrefs.SetInt("rc_balance", int.Parse(result[6]));
                }
            }
            loading.SetActive(false);
        }
    }
}
