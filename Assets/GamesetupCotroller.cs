using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System;
using UnityStandardAssets.Vehicles.Car;
using System.Linq;

public class GamesetupCotroller : MonoBehaviourPunCallbacks
{

  int a1 = 1;

  public Transform[] spawnPoints;
  public static GameObject[] players1;
  private GameObject playerCar;
  int enable = 0;
  bool enn = false;
  // Start is called before the first frame update
  void Start()
  {
    PlayerPrefs.SetInt("carnumber", 0);

    CreatePlayer();


  }

  void CreatePlayer()
  {

    Player[] players = PhotonNetwork.PlayerList;
    for (int i = 0; i < players.Length; i++)
    {
      int index = Array.IndexOf(PhotonNetwork.PlayerList, players[i]);
      if (index != -1 && index < spawnPoints.Length && players[i] == PhotonNetwork.LocalPlayer)
      {
        Vector3 spawnPosition = spawnPoints[index].position;
        Quaternion spawnRotation = Quaternion.identity;
        GameObject playerCarPrefab = null;

        // Instantiate the player's car based on some condition (e.g., PlayerPrefs.GetInt("carnumber"))
        // Example:
        if (PlayerPrefs.GetInt("carnumber") == 0)
        {
          playerCarPrefab = Resources.Load<GameObject>("Player");
        }
        else if (PlayerPrefs.GetInt("carnumber") == 1)
        {
          playerCarPrefab = Resources.Load<GameObject>("Player1");
        }
        else if (PlayerPrefs.GetInt("carnumber") == 2)
        {
          playerCarPrefab = Resources.Load<GameObject>("Player2");
        }

        if (playerCarPrefab != null)
        {
          GameObject playerCar = PhotonNetwork.Instantiate(playerCarPrefab.name, spawnPosition, spawnRotation);
          playerCar.transform.GetChild(2).GetComponent<CarCam>().enabled = true;
          // players1[0] = playerCar;


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
    }
       Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["C0"]);
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
          if (car.transform.GetChild(2).GetComponent<CarCam>())
            car.transform.GetChild(2).gameObject.SetActive(true);


        }
        else
        {
          // Disable control script on other players' cars
          // Example:
          car.GetComponent<CarController>().enabled = false;
          car.GetComponent<CarUserControl>().enabled = false;
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
  // Update is called once per frame
  [PunRPC]
  void enableit(int i)
  {
    StartCoroutine(en());

  }

  [PunRPC]
  void RPCStartGame(Vector3 spawnposition, Quaternion spawnrotaion)
  {
    PhotonNetwork.Instantiate("Player", spawnposition, spawnrotaion, 0);
  }
}
