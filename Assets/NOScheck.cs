using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class NOScheck : MonoBehaviour
{
    public AxisTouchButton axisTouchButton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a GameObject with a PhotonView
        PhotonView otherPhotonView = other.gameObject.transform.parent.transform.parent.GetComponent<PhotonView>();
        Debug.Log(other.gameObject.transform.parent.name);
        // Check if the GameObject has a PhotonView component
        if (otherPhotonView != null)
        {
            // Check if the collider belongs to the local player's object
            if (otherPhotonView.IsMine)
            {
                // Perform actions specific to the local player's object collision
                Debug.Log("Local player's object collided with something!");
                otherPhotonView.GetComponent<CarController>().m_Topspeed=400;
                otherPhotonView.GetComponent<CarController>().boost=true;

            }
            else
            {
                // Perform actions for collisions with objects not owned by the local player
                Debug.Log("Other player's object collided with something!");
            }
        }
    }
}
