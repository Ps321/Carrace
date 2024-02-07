using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTriggers : MonoBehaviour
{


    public GameObject Marker;
    public GameObject previousTrigger;
    public GameObject nextTrigger;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
  /* if(other.gameObject.tag=="AIcar01"){
   Marker.transform.position=nextTrigger.transform.position;
   }*/
  if(other.gameObject.tag!="AIcar01"){
   
   StartCoroutine(collideroff());
  }
    
    }

    IEnumerator collideroff(){
        yield return new WaitForSeconds(0.5f);
         var b =PlayerPrefs.GetInt("playercheckpoints");

  PlayerPrefs.SetInt("playercheckpoints",b+1);
  Debug.Log(b+"checkpoiny");
    }
}
