using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{

    public GameObject[] cars;
    public Text carname;
    public Scene s1;

    public GameObject lockimage;
    public GameObject selectbtn;
    public GameObject menu1;
    public GameObject garage;

    public Text selectbtntext;


    public int val1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("RaceMode", 2);
        PlayerPrefs.SetInt("Mapmode", 1);
        if (PlayerPrefs.GetInt("carnumber") == 0)
        {
            selectbtntext.text = "Selected";
        }
        else
        {
            selectbtntext.text = "Select";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void setracemode(int val)
    {
        PlayerPrefs.SetInt("RaceMode", val);
        if (val == 1)
        {

        }
        else
        {

        }
    }

    public void setmapmode(int val)
    {
        PlayerPrefs.SetInt("Mapmode", val);
    }

    public void next()
    {
        if (val1 == 14)
        {
            cars[0].SetActive(true);
            cars[14].SetActive(false);

            carname.text = cars[0].name;
            val1 = 0;
        }
        else
        {
            cars[val1 + 1].SetActive(true);
            cars[val1].SetActive(false);
            Debug.Log(cars[val1].name);
            carname.text = cars[val1 + 1].name;
            val1 = val1 + 1;
        }
        if (val1 == 0)
        {
            lockimage.SetActive(false);
            selectbtn.SetActive(true);

        }
        else
        {
            lockimage.SetActive(true);
            selectbtn.SetActive(false);
        }

        if (PlayerPrefs.GetInt("carnumber") == val1)
        {
            selectbtntext.text = "Selected";
        }
        else
        {
            selectbtntext.text = "Select";
        }

    }

    public void prev()
    {
        if (val1 == 0)
        {
            cars[5].SetActive(true);
            cars[0].SetActive(false);
            carname.text = "Car 6";
            val1 = 5;
        }
        else
        {
            cars[val1 - 1].SetActive(true);
            cars[val1].SetActive(false);
            carname.text = "Car" + (val1);
            val1 = val1 - 1;
        }
        if (val1 == 0 || val1 == 1 || val1 == 2)
        {
            lockimage.SetActive(false);
            selectbtn.SetActive(true);

        }
        else
        {
            lockimage.SetActive(true);
            selectbtn.SetActive(false);
        }
        if (PlayerPrefs.GetInt("carnumber") == val1)
        {
            selectbtntext.text = "Selected";
        }
        else
        {
            selectbtntext.text = "Select";
        }
    }

    public void selectvehicle()
    {
        PlayerPrefs.SetInt("carnumber", val1);
        SceneManager.LoadScene(11);
    }

    public void loadgame()
    {

        if (PlayerPrefs.GetInt("RaceMode") == 2)
        {
            if (PlayerPrefs.GetInt("Mapmode") == 1)
            {
                SceneManager.LoadScene(4);
            }
            if (PlayerPrefs.GetInt("Mapmode") == 2)
            {
                SceneManager.LoadScene(5);
            }

        }

        if (PlayerPrefs.GetInt("RaceMode") == 1)
        {
            if (PlayerPrefs.GetInt("Mapmode") == 1)
            {
                SceneManager.LoadScene(6);
            }
            if (PlayerPrefs.GetInt("Mapmode") == 2)
            {
                SceneManager.LoadScene(7);
            }

        }


        if (PlayerPrefs.GetInt("RaceMode") == 3)
        {
            if (PlayerPrefs.GetInt("Mapmode") == 1)
            {
                SceneManager.LoadScene(8);
            }
            if (PlayerPrefs.GetInt("Mapmode") == 2)
            {
                SceneManager.LoadScene(9);
            }

        }
        if (PlayerPrefs.GetInt("RaceMode") == 4)
        {
            menu1.SetActive(true);
            garage.SetActive(false);
        }
        //SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void refresh()
    {
        SceneManager.LoadScene(11);
    }
}
