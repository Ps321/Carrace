using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using System.Linq;
public class indcoin_timer : MonoBehaviourPunCallbacks, IPunObservable
{
    private float timer;
    public float maxTime = 5f;
    public bool isTimerRunning = false;
    public Text timeleft;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Start the timer on the master client
            isTimerRunning = true;
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            // Update the timer
            timer += Time.deltaTime;
            timeleft.text = Mathf.CeilToInt(maxTime - timer).ToString();
            if (timer >= maxTime)
            {
                if (PhotonNetwork.PlayerList.Count() < 4)
                {
                    timer=0;
                }
                else
                {


                    Debug.Log("Timer reached max time!");
                    isTimerRunning = false;
                    if (PhotonNetwork.InRoom)
                    {
                        Room currentRoom = PhotonNetwork.CurrentRoom;

                        // Create a Hashtable to store the updated custom properties
                        Hashtable customProperties = new Hashtable();

                        // Add or update custom properties as needed
                        customProperties["C1"] = "True";


                        currentRoom.IsOpen = false;

                        // Update the room properties to reflect the change
                        currentRoom.SetCustomProperties(new Hashtable() { { "IsOpen", false } });
                        // Update the room custom properties
                        currentRoom.SetCustomProperties(customProperties);
                    }
                    // foreach (var entry in PhotonNetwork.CurrentRoom.CustomProperties)
                    // {
                    //     Debug.Log("Custom Property - Key: " + entry.Key + ", Value: " + entry.Value);
                    // }
                    // SceneManager.LoadScene(3);
                }
            }
        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        // Iterate through the updated properties and log them
        foreach (var entry in propertiesThatChanged)
        {
            if (entry.Key.ToString() == "C1" && propertiesThatChanged["C1"].ToString() == "True")
            {
                if (int.Parse(PhotonNetwork.CurrentRoom.CustomProperties["C2"].ToString()) == 0)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    SceneManager.LoadScene(10);
                }

            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending timer state to other clients
            stream.SendNext(timer);
            stream.SendNext(isTimerRunning);
        }
        else
        {
            // Receiving timer state from master client
            timer = (float)stream.ReceiveNext();
            isTimerRunning = (bool)stream.ReceiveNext();
        }
    }

}
