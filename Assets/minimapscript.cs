using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class minimapscript : MonoBehaviourPun
{
    private void Awake() {
        if(!photonView.IsMine && PlayerPrefs.GetInt("RaceMode")==4){
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
