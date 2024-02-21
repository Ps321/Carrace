using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System;
using UnityStandardAssets.Vehicles.Car;
using System.Linq;
using UnityEngine.SceneManagement;

public class GamesetupCotroller : MonoBehaviourPunCallbacks
{

  int a1 = 1;

  public Transform[] spawnPoints;
  public static GameObject[] players1;
  private GameObject playerCar;
  public GameObject[] cars;
  int enable = 0;
  bool enn = false;
  // Start is called before the first frame update
  void Start()
  {
    if (SceneManager.GetActiveScene().name == "Training_map2" || SceneManager.GetActiveScene().name == "Training_map1")
    {
      enable = 1;
    }

    CreatePlayer();


  }

  void CreatePlayer()
  {

    Player[] players = PhotonNetwork.PlayerList;

    int index = Array.IndexOf(players, PhotonNetwork.LocalPlayer);
    if (index != -1 && index < spawnPoints.Length)
    {
      Vector3 spawnPosition = spawnPoints[index].position;
      Quaternion spawnRotation = Quaternion.identity;
      GameObject playerCarPrefab = null;


      playerCarPrefab = cars[PlayerPrefs.GetInt("carnumber")];

      if (playerCarPrefab != null)
      {
        GameObject playerCar = PhotonNetwork.Instantiate(playerCarPrefab.name, spawnPosition, spawnRotation);
        playerCar.transform.GetChild(2).GetComponent<CarCam>().enabled = true;

        // players1[0] = playerCar;
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
          playerCar.transform.rotation = Quaternion.EulerAngles(0, -90, 0);
        }
        // if (SceneManager.GetActiveScene().buildIndex == 3)
        // {
        //   playerCar.transform.rotation = Quaternion.EulerAngles(0, 45, 0);
        // }


        // Set the owner of the car to the respective player
        // playerCar.GetComponent<PhotonView>().TransferOwnership(players[i]);
      }
      else
      {
        Debug.LogError("Failed to load player car prefab.");
      }
    }
    else
    {
      Debug.Log("Local player index out of bounds or spawn points not assigned.");
    }

    // Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["C0"]);
    StartCoroutine(en());
    // Iterate through the found GameObjects

  }


  void Update()
  {


    if (enable == 1)
    {
      GameObject[] cars = GameObject.FindGameObjectsWithTag("Player");

      foreach (GameObject car in cars)
      {
        ExitGames.Client.Photon.Hashtable customProps = car.GetComponent<PhotonView>().Owner.CustomProperties;
        if (car.GetComponent<PhotonView>().IsMine)
        {
          // Enable control script on the local player's car
          // Example:
          car.GetComponent<CarController>().enabled = true;
          car.GetComponent<CarUserControl>().enabled = true;
          car.layer = 6;
          if (car.transform.GetChild(2).GetComponent<CarCam>())
          {
            car.transform.GetChild(2).gameObject.SetActive(true);

          }



        }
        else
        {
          // Disable control script on other players' cars
          // Example:
          car.GetComponent<CarController>().enabled = false;
          car.GetComponent<CarUserControl>().enabled = false;
          car.GetComponent<speedometer>().enabled = false;
          if (car.transform.GetChild(0).GetComponent<minimapscript>())
          {
            Destroy(car.transform.GetChild(0));
          }
        }
      }
    }
  }
  IEnumerator en()
  {
    yield return new WaitForSeconds(0.5f);
    enable = 1;
  }
  public void Mainlobby()
  {
    PhotonNetwork.LeaveRoom();
    PhotonNetwork.Disconnect();
    SceneManager.LoadScene(11);
  }
  // Update is called once per frame
  [PunRPC]
  void enableit(int i)
  {
    StartCoroutine(en());

  }

  // [PunRPC]
  // void RPCStartGame(Vector3 spawnposition, Quaternion spawnrotaion)
  // {
  //   PhotonNetwork.Instantiate("Player", spawnposition, spawnrotaion, 0);
  // }
}
