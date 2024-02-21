using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class database : MonoBehaviour
{
    public InputField bankname;
    public InputField aadhar;
    public InputField pan;
    public InputField accno;
    public InputField ifsc;
    public GameObject kycerror;



    void Update()
    {

    }
    public void Updatekyc()
    {
        Debug.Log("aaya");
        if (bankname.text != "" && aadhar.text != "" && pan.text != "" && accno.text != "" && ifsc.text != "")
        {
            StartCoroutine(updatekyc());
        }
        else
        {
            kycerror.SetActive(true);
            kycerror.GetComponent<Text>().text = "All fields Required*";
            kycerror.GetComponent<Text>().color = Color.red;
        }
    }
    IEnumerator updatekyc()
    {
        Debug.Log("aaya1");
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("name"));
        form.AddField("email", PlayerPrefs.GetString("email"));
        form.AddField("bankname", bankname.text);
        form.AddField("aadhar", aadhar.text);
        form.AddField("pan", pan.text);
        form.AddField("account", accno.text);
        form.AddField("ifsc", ifsc.text);




        using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/updatekyc.php", form))
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
                    kycerror.GetComponent<Text>().text = "Error Updating Kyc";
                    kycerror.GetComponent<Text>().color = Color.red;
                    // t.text = "User Already Exist";
                    // t.color = Color.red;
                }

                else
                {
                    kycerror.GetComponent<Text>().text = "Updated Successfully";
                    kycerror.GetComponent<Text>().color = Color.green;
                    Debug.Log(s); //Output 1

                }
            }

        }
    }
}
