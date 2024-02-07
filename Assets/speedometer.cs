using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class speedometer : MonoBehaviourPun
{

  
   public GameObject speedText;
   public Image needle;

    // Update is called once per frame

    private void Start() {
      needle=GameObject.FindGameObjectWithTag("needle").GetComponent<Image>();
      speedText=GameObject.FindGameObjectWithTag("speed");
    }
    void Update()
    {
      speedText.GetComponent<Text>().text= ((int) gameObject.GetComponent<Rigidbody>().velocity.magnitude).ToString();
       float v=gameObject.GetComponent<Rigidbody>().velocity.magnitude*-5;
       v=Mathf.Clamp(v,-258,0);
        needle.rectTransform.rotation=Quaternion.Euler(0,0,v);
    }
}
