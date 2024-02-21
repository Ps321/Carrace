using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class photonscript_rc : MonoBehaviourPunCallbacks
{
    private int playerNumber = 0;
    public Text[] names;
    public Text[] status;
    public GameObject[] loading;
    public int myplayerNumber;

    public GameObject Loading;
    public GameObject startingGame;
    public Text Rank;
    public Text mapname;

    private TypedLobby sqlLobby = new TypedLobby("customSqlLobby", LobbyType.SqlLobby);
    public const string ELO_PROP_KEY = "C0";
    public const string MAP_PROP_KEY = "C1";

    void Start()
    {

        UpdateRank();
        for (int i = 0; i < 6; i++)
        {
            loading[i].SetActive(true);
        }
        Loading.SetActive(true);
        ConnectToPhoton();
    }
    public void UpdateRank()
    {
        int a = PlayerPrefs.GetInt("rc_balance");
        if (a > 0 && a < 15000)
        {
            Rank.text = "Club racers";
        }
        else if (a > 15000 && a < 40000)
        {
            Rank.text = "Dare Racers";
        }
        else if (a > 40000 && a < 65000)
        {
            Rank.text = "Dangerous Racers";
        }
        else if (a > 65000 && a < 80000)
        {
            Rank.text = "Notorious Racers";
        }
        else if (a > 80000 && a < 100000)
        {
            Rank.text = "Real Racers";
        }

    }

    public void ConnectToPhoton()
    {
        Loading.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {


        Debug.Log("Connected to Photon Master Server");



        ExitGames.Client.Photon.Hashtable roomTournament = new ExitGames.Client.Photon.Hashtable() { { "C0", "racersclub" + Rank.text }, { "C1", "false" } };
        PlayerPrefs.SetInt("gamemode", 2);
        PhotonNetwork.JoinRandomRoom(roomTournament, 0);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No random room available, creating a new room...");
        CreateRoom();
    }

    void CreateRoom()
    {
        string roomName = "Room" + Random.Range(1000, 10000); // Generate a random room name
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6; // Change the value as per your requirement
        string rank = Rank.text; // Assuming Rank.text holds the player's rank

        // Set custom properties based on player's rank

        string c0Value = "racersclub" + Rank.text; // Default value for other ranks


        int randomNumber = Random.Range(0, 2);
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "C0", "racersclub" + Rank.text }, { "C1", "false" }, { "C2", randomNumber } };
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "C0", "C1", "C2" };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Room createdRoom = PhotonNetwork.CurrentRoom;
        LogRoomCustomProperties(createdRoom);
        playerNumber++;
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        ExitGames.Client.Photon.Hashtable customPlayerProperties = new ExitGames.Client.Photon.Hashtable();
        customPlayerProperties["PlayerNumber"] = playerNumber;
        customPlayerProperties["Name"] = PlayerPrefs.GetString("name");
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["C0"]);
        // customPlayerProperties.Add("PlayerType", "Racer"); // Example custom player property
        PhotonNetwork.LocalPlayer.SetCustomProperties(customPlayerProperties);
        StartCoroutine(ReadPlayerInfo());
        if (int.Parse(createdRoom.CustomProperties["C2"].ToString()) == 0)
        {
            mapname.text = "Highway Riders";
        }
        else
        {
            mapname.text = "Desert Storm";
        }

        // SpawnPlayer();
    }
    void LogRoomCustomProperties(Room room)
    {
        Debug.Log("Room Name: " + room.Name);

        // Log each custom property
        foreach (var entry in room.CustomProperties)
        {
            Debug.Log("Custom Property - Key: " + entry.Key + ", Value: " + entry.Value);
        }
    }

    // Example usage:
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        // Get the created room and log its custom properties
        Room createdRoom = PhotonNetwork.CurrentRoom;
        LogRoomCustomProperties(createdRoom);
    }

    void SpawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)); // Adjust spawn position as needed
        PhotonNetwork.Instantiate("PlayerPrefab", spawnPosition, Quaternion.identity);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined the room");
        AssignPlayerNumber(newPlayer);
        StartCoroutine(ReadPlayerInfo());
    }


    void AssignPlayerNumber(Player player)
    {
        // Rank.text = PhotonNetwork.CurrentRoom.CustomProperties["C0"].ToString();
        if (PhotonNetwork.IsMasterClient)
        {
            playerNumber++;
            player.CustomProperties["PlayerNumber"] = PlayerPrefs.GetString("name");
            player.SetCustomProperties(player.CustomProperties);
            if (PhotonNetwork.PlayerList.Length == PhotonNetwork.CurrentRoom.MaxPlayers)
                gameObject.GetPhotonView().RPC("SetPlayerNumber", RpcTarget.AllBuffered, playerNumber);
        }

        // player.NickName = PlayerPrefs.GetString("name");
        // player.CustomProperties["PlayerNumber"] = PlayerPrefs.GetString("name");
        // player.CustomProperties["Name"] = playerNumber;
        // player.SetCustomProperties(player.CustomProperties);
        // 
        Debug.Log(player.NickName + " assigned player number: " + playerNumber);

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Loading.SetActive(true);
        StartCoroutine(ReadPlayerInfo());
    }
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }


    [PunRPC]
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
        Debug.Log("From RPC Player number set to: " + playerNumber);
        // StartCoroutine(ReadPlayerInfo());
        StartCoroutine(startGame());

    }
    IEnumerator startGame()
    {

        yield return new WaitForSeconds(5.0f);
        startingGame.SetActive(true);

        if (PhotonNetwork.InRoom)
        {
            Room currentRoom = PhotonNetwork.CurrentRoom;

            // Create a Hashtable to store the updated custom properties
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

            // Add or update custom properties as needed
            customProperties["C1"] = "True";


            currentRoom.IsOpen = false;

            // Update the room properties to reflect the change
            currentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "IsOpen", false } });
            // Update the room custom properties
            currentRoom.SetCustomProperties(customProperties);
        }
        yield return new WaitForSeconds(3.0f);
        startingGame.SetActive(false);
        if (int.Parse(PhotonNetwork.CurrentRoom.CustomProperties["C2"].ToString()) == 0)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(10);
        }
    }

    IEnumerator ReadPlayerInfo()
    {
        // Rank.text = PhotonNetwork.CurrentRoom.CustomProperties["C0"].ToString();
        yield return new WaitForSeconds(1f); // Wait for some time after joining room to ensure players have synchronized

        Player[] players = PhotonNetwork.PlayerList;
        int index = 0;
        int mypos = 0;
        foreach (Player player in players)
        {
            Debug.Log("Player: " + player.NickName);

            // Read custom properties
            ExitGames.Client.Photon.Hashtable customProps = player.CustomProperties;
            status[index].text = "Ready" + customProps[index];
            if (player == PhotonNetwork.LocalPlayer)
            {
                names[index].text = "Me😎";
                status[index].text = "Ready";
            }
            else
            {
                names[index].text = customProps["Name"].ToString();
            }

            loading[index].SetActive(false);
            index++;

        }
        for (int i = index; i < 6; i++)
        {
            loading[i].SetActive(true);
        }
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     timer.isTimerRunning = true;
        // }
        Loading.SetActive(false);
    }


}
