using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class StartLobby : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Startbtn;
    [SerializeField] private GameObject LobbyPanel;
    [SerializeField] private GameObject MainPanel;

    public InputField playernameInput;
    [SerializeField] private int roomSize;
    [SerializeField] private string RoomName;

    private List<RoomInfo> roomListing;

    [SerializeField] private Transform roomsContainer;
    [SerializeField] GameObject roomListingprefab;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Startbtn.SetActive(true);
        roomListing = new List<RoomInfo>();
        //PhotonNetwork.JoinRandomRoom();

        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("Nickname") == "")
            {
                PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        }

        playernameInput.text = PhotonNetwork.NickName;

    }

    public void updateName(string name)
    {
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString("NickName", name);

    }

    public void JoinLobbyOnClick()
    {
        MainPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {


        foreach (RoomInfo room in roomList)
        {
            if (room.PlayerCount > 0)
            {
                roomListing.Add(room);
                ListRoom(room);
            }
        }
    }


    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingprefab, roomsContainer);
            RoomButton tempbtn = tempListing.GetComponent<RoomButton>();


            tempbtn.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }


    public void OnRoomNameChanged(string nameIn)
    {
        RoomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }

    public void CreateRoom()
    {
        Debug.Log("Creating Room");

        RoomOptions roomops = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(RoomName, roomops);
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create Room ............. try Again with diffrent name");
        // CreateRoom();
    }


    public void Cancel1()
    {
        MainPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }

    public void Exit()
    {
        Application.Quit();
    }















    /*

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("failed to join Random Lobby");

            CreateRoom();
        }

        void CreateRoom(){
            Debug.Log("Creating Room now");

            int randomno=Random.Range(0,1000);
            RoomOptions roomOps=new RoomOptions(){ IsVisible=true , IsOpen =true , MaxPlayers=(byte)roomSize};
            PhotonNetwork.CreateRoom("Room No:-"+randomno,roomOps);
            Debug.Log(randomno);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to create Room ............. trying Again");
            CreateRoom();
        }





        public void DelayStart(){
            Startbtn.SetActive(false);

        }

        public void DelayCancel(){
            Startbtn.SetActive(true);

            PhotonNetwork.LeaveRoom();
        }    */
}
