using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logincontroller : MonoBehaviour
{
    public InputField name1;
    public InputField email;
    public InputField pno;
    public InputField password;

    public Text t;
    public Text t1;
    public GameObject loading;
    public GameObject Signupscreen;

    // public phoneauth p;


    public InputField Loginemail;
    public InputField Loginpassword;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Login()
    {
        StartCoroutine(checkuser());
    }
    public void Signup()
    {
        StartCoroutine(Signup1());
    }

    IEnumerator Signup1()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name1.text);
        form.AddField("email", email.text);
        form.AddField("pno", pno.text);
        form.AddField("password", password.text);



        using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/signup.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                SceneManager.LoadScene(0);
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string s = www.downloadHandler.text.Trim();
                if (s == "Error")
                {
                    t.text = "User Already Exist";
                    t.color = Color.red;
                }

                else
                {
                    t.text = "Signed Up Successfully Click here to Login";
                    t.color = Color.green;
                    Debug.Log(s); //Output 1
                    // SceneManager.LoadScene(0);
                }
            }
        }
    }

    IEnumerator checkuser()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", Loginemail.text);

        form.AddField("password", Loginpassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://indgamesia.com/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                SceneManager.LoadScene(0);
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string s = www.downloadHandler.text.Trim();
                if (s == "Error")
                {
                    t1.text = "User Doesnt Exist Please Signup";
                    t1.color = Color.red;
                }

                else
                {
                    t1.text = "Logged In Succesfully";
                    t1.color = Color.green;
                    Debug.LogFormat(s);
                    // string[] items = s.Split(',');
                    // PlayerPrefs.SetString("name", items[0]);

                    // PlayerPrefs.SetString("balance", items[2]);
                    PlayerPrefs.SetString("email", Loginemail.text);


                    // p.Login(Loginumber.text);

                    loading.SetActive(true);
                }
            }
        }
    }
}
