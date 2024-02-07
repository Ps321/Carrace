using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WaitingRoomController : MonoBehaviourPunCallbacks 
{
    // Start is called before the first frame update
    private PhotonView myphotonview;
[SerializeField] private int multiplayerSceneIndex;
    [SerializeField] private int menuRoomSceneIndex;

    [SerializeField] Text Countdown;
    [SerializeField] Text PlayerCountText;
    private int playercount;

    private int roomsize;
    private bool readytoCountdown;
    private bool readytostart;
    private bool startinggame;
    [SerializeField] private int minplayer;

    private float timertostartGame;

    private float notfullGameTimer;

    private float fullGametimer;

    [SerializeField] float maxWaitTime;

    [SerializeField] float maxfullwaitTime;



    void Start()
    {
        myphotonview=GetComponent<PhotonView>();
        fullGametimer=maxfullwaitTime;
        notfullGameTimer=maxWaitTime;
        timertostartGame=maxWaitTime;
        PlayerCountUpdate();

    }
    void PlayerCountUpdate(){
        playercount=PhotonNetwork.PlayerList.Length;
        roomsize=4;
        PlayerCountText.text=playercount+":"+roomsize;

        if(playercount==roomsize){
            readytostart=true;
        }
        else if(playercount>=minplayer){
            readytoCountdown=true;
        }
        else{
            readytoCountdown=false;
            readytostart=false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        if(PhotonNetwork.IsMasterClient){
            myphotonview.RPC("RPC_SendTimer",RpcTarget.Others,timertostartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float time_in){
        timertostartGame=time_in;
        notfullGameTimer=time_in;

        if(time_in<fullGametimer){
            fullGametimer=time_in;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
        //base.OnPlayerLeftRoom(otherPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        WaitingforMorePlayers();
    }

    void WaitingforMorePlayers(){
        if(playercount<=1){
            resetTimer();
        }
        if(readytostart){
            fullGametimer-=Time.deltaTime;
            timertostartGame=fullGametimer;

        }
        else if(readytoCountdown){
            notfullGameTimer-=Time.deltaTime;
            timertostartGame=notfullGameTimer;
        }

        string temptimer= string.Format("{0:00}",timertostartGame);
        Countdown.text=temptimer;

        if(timertostartGame<=0f){
            if(startinggame){
                return;

            
            }
            StartGame();
        }
    }

    void resetTimer(){
        timertostartGame=maxWaitTime;
        notfullGameTimer=maxWaitTime;
        fullGametimer=maxfullwaitTime;
    }

    public void StartGame(){
        startinggame=true;
        if(!PhotonNetwork.IsMasterClient)
        return;

        PhotonNetwork.CurrentRoom.IsOpen=false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void cancel1(){
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuRoomSceneIndex);
    }

}
