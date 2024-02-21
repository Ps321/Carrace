using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointcontrol_multi : MonoBehaviour
{


    public LapCompleteManager_multi multi;
    public int value;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        /* if(other.gameObject.tag=="AIcar01"){
         Marker.transform.position=nextTrigger.transform.position;
         }*/
        if (other.transform.parent.transform.parent.gameObject.layer == 6)
        {

            multi.arr[value] = 1;
        }

    }

    IEnumerator collideroff()
    {
        yield return new WaitForSeconds(0.5f);
        var b = PlayerPrefs.GetInt("playercheckpoints");

        PlayerPrefs.SetInt("playercheckpoints", b + 1);
        Debug.Log(b + "checkpoiny");
    }
}
