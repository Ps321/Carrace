using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private int MultiplayerRoomSceneIndex;
    [SerializeField] private GameObject lobbypanel;
    [SerializeField] private GameObject roompanel;

    [SerializeField] private GameObject startbtn;

    [SerializeField] private Transform playerContainer;
    [SerializeField] private GameObject playerListingprefab;
    [SerializeField] private Text roomNameDisplay;


    void ClearPlayerListing(){
        for(int i=playerContainer.childCount-1;i>0;i--){
            Destroy(playerContainer.GetChild(1).gameObject);
        }
    }

    void ListPlayers(){
        foreach(Player player in PhotonNetwork.PlayerList){
            GameObject templisting=Instantiate(playerListingprefab,playerContainer);
            Text temptext=templisting.transform.GetChild(0).GetComponent<Text>();
            temptext.text=player.NickName;
        }
    }

    public override void OnJoinedRoom()
    {
       roompanel.SetActive(true);
       lobbypanel.SetActive(false);
       roomNameDisplay.text=PhotonNetwork.CurrentRoom.Name;

       if(PhotonNetwork.IsMasterClient){
        startbtn.SetActive(true);
       }
       else{
        startbtn.SetActive(false);
       }

       ClearPlayerListing();
       ListPlayers();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearPlayerListing();
       ListPlayers();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerListing();
       ListPlayers();

       if(PhotonNetwork.IsMasterClient){
        startbtn.SetActive(true);
       }
    }

    public void StartGame(){
        if(PhotonNetwork.IsMasterClient){
        //PhotonNetwork.CurrentRoom.IsOpen=false;
       if(PlayerPrefs.GetInt("Mapmode")==1){
            SceneManager.LoadScene(3);
           }else{
            SceneManager.LoadScene(10);
           }
       
       }
    }

    IEnumerator rejoinlobby(){
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }


    public void Backonclick(){
        lobbypanel.SetActive(true);
        roompanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinlobby());
    }





/*

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
*/
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
