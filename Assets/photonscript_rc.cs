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
        if (a > 0 && a < 3000)
        {
            Rank.text = "Bronze";
        }
        else if (a > 2999 && a < 3999)
        {
            Rank.text = "Silver";
        }
        else if (a > 3999 && a < 4999)
        {
            Rank.text = "Gold";
        }
        else if (a > 4999 && a < 5999)
        {
            Rank.text = "Platinum";
        }
        else if (a > 5999 && a < 6999)
        {
            Rank.text = "Immortal";
        }
        else if (a > 6999)
        {
            Rank.text = "Conqueror";
        }
    }

    public void ConnectToPhoton()
    {
        Loading.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        string sqlLobbyFilter = "C0='kjjj' AND C1='false'";

        Debug.Log("Connected to Photon Master Server");


        Debug.Log(sqlLobbyFilter);
        PhotonNetwork.JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, null, sqlLobbyFilter);
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
        roomOptions.MaxPlayers = 2; // Change the value as per your requirement
        string rank = Rank.text; // Assuming Rank.text holds the player's rank

        // Set custom properties based on player's rank
        string c0Value;
        if (rank == "Conqueror")
        {
            c0Value = "racersclubConqueror";
        }
        else
        {
            c0Value = "racersclub"; // Default value for other ranks
        }

        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "C0", c0Value }, { "C1", "false" } };

        roomOptions.CustomRoomPropertiesForLobby = new string[] { "C0", "C1" };
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
    }


    void AssignPlayerNumber(Player player)
    {
        Rank.text = PhotonNetwork.CurrentRoom.CustomProperties["C0"].ToString();
        if (PhotonNetwork.IsMasterClient)
        {
            playerNumber++;
            player.CustomProperties["PlayerNumber"] = PlayerPrefs.GetString("name");
            player.SetCustomProperties(player.CustomProperties);
            if (PhotonNetwork.PlayerList.Length == 2)
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
        yield return new WaitForSeconds(3.0f);
        startingGame.SetActive(false);
        SceneManager.LoadScene(3);
    }

    IEnumerator ReadPlayerInfo()
    {
        Rank.text = PhotonNetwork.CurrentRoom.CustomProperties["C0"].ToString();
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
