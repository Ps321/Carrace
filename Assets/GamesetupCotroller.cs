using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System;

public class GamesetupCotroller : MonoBehaviourPunCallbacks
{

    int a1=1;
   
    public Transform[] spawnPoints;
    public static GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        
        CreatePlayer();
             }

    void CreatePlayer(){
         if(PlayerPrefs.GetInt("carnumber")==0){
       int i= Array.IndexOf(PhotonNetwork.PlayerList,PhotonNetwork.LocalPlayer);
       players[i]= PhotonNetwork.Instantiate("Player",spawnPoints[i].position,Quaternion.identity);
         }
         if(PlayerPrefs.GetInt("carnumber")==1){
       int i= Array.IndexOf(PhotonNetwork.PlayerList,PhotonNetwork.LocalPlayer);
        players[i]=PhotonNetwork.Instantiate("Player 1",spawnPoints[i].position,Quaternion.identity);
         }
         if(PlayerPrefs.GetInt("carnumber")==2){
       int i= Array.IndexOf(PhotonNetwork.PlayerList,PhotonNetwork.LocalPlayer);
       players[i]= PhotonNetwork.Instantiate("Player 2",spawnPoints[i].position,Quaternion.identity);
         }
    }

    // Update is called once per frame
  

    [PunRPC]
    void RPCStartGame(Vector3 spawnposition,Quaternion spawnrotaion){
        PhotonNetwork.Instantiate("Player",spawnposition,spawnrotaion,0);
    }
}
