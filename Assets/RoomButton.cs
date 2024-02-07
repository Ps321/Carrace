using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour{

[SerializeField] private Text nameText;

[SerializeField] private Text sizeText;

private string roomname;
private int roomSize;
private int playerCount;


public void JoinRoomOnClick(){
    PhotonNetwork.JoinRoom(roomname);

}

public void SetRoom(string nameInput,int sizeInput,int countInput){
    roomname=nameInput;
    roomSize=sizeInput;
    playerCount=countInput;
    nameText.text=nameInput;
    sizeText.text=countInput+"/"+sizeInput;
}

}