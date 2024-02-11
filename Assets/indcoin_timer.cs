using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
                // Timer has reached max time, do something
                Debug.Log("Timer reached max time!");
                isTimerRunning = false;
                SceneManager.LoadScene(3);
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
