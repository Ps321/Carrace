using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControlwaypoint : MonoBehaviour
{

    public GameObject Marker;

    public GameObject mark01;
    public GameObject mark02;
    public GameObject mark03;
     public GameObject mark04;
     public GameObject mark05;
     public GameObject mark06;

      public GameObject mark07;

       public GameObject mark08;

        public GameObject mark09;
         public GameObject mark10;
          public GameObject mark11;
           public GameObject mark12;
        public GameObject mark13;
        public GameObject mark14;
        public GameObject mark15;
        public GameObject mark16;
        public GameObject mark17;
        public GameObject mark18;
        public GameObject mark19;
        public GameObject mark20;
        public GameObject mark21;
        public GameObject mark22;
        public GameObject mark23;
        public GameObject mark24;
        public GameObject mark25;

        public GameObject mark26;
        public GameObject mark27;

        public GameObject mark28;
        public GameObject mark29;
        public GameObject mark30;
        
        public GameObject mark31;
        public GameObject mark32;
        public GameObject mark33;
        public GameObject mark34;
        public GameObject mark35;
        public GameObject mark36;
        public GameObject mark37;
        public GameObject mark38;
        public GameObject mark39;
        public GameObject mark40;
        public GameObject mark41;
        public GameObject mark42;

        public GameObject mark43;
        public GameObject mark44;
        public GameObject mark45;
        public GameObject mark46;



    public int markpoint=0;

    // Start is called before the first frame update
    

    // Update is called once per frame
   IEnumerator OnTriggerEnter(Collider other) {
    Marker.GetComponent<BoxCollider>().enabled=false;
    if(markpoint==0){
        Marker.transform.position=mark02.transform.position;
    }
    if(markpoint==1){
        Marker.transform.position=mark03.transform.position;
    }
    if(markpoint==2){
        Marker.transform.position=mark04.transform.position;
    }
    if(markpoint==3){
        Marker.transform.position=mark05.transform.position;
    }
    if(markpoint==4){
        Marker.transform.position=mark06.transform.position;
    }
    if(markpoint==5){
        Marker.transform.position=mark07.transform.position;
    }
    if(markpoint==6){
        Marker.transform.position=mark08.transform.position;
    }
    if(markpoint==7){
        Marker.transform.position=mark09.transform.position;
    }
    if(markpoint==8){
        Marker.transform.position=mark10.transform.position;
    }
    if(markpoint==9){
        Marker.transform.position=mark11.transform.position;
    }
    if(markpoint==10){
        Marker.transform.position=mark12.transform.position;
    }
    if(markpoint==11){
        Marker.transform.position=mark13.transform.position;
    }
    if(markpoint==12){
        Marker.transform.position=mark14.transform.position;
    }
    if(markpoint==13){
        Marker.transform.position=mark15.transform.position;
    }
    if(markpoint==14){
        Marker.transform.position=mark16.transform.position;
    }
    if(markpoint==15){
        Marker.transform.position=mark17.transform.position;
    }
    if(markpoint==16){
        Marker.transform.position=mark18.transform.position;
    }
    if(markpoint==17){
        Marker.transform.position=mark19.transform.position;
    }
    if(markpoint==18){
        Marker.transform.position=mark20.transform.position;
    }
    if(markpoint==19){
        Marker.transform.position=mark21.transform.position;
    }
    if(markpoint==20){
        Marker.transform.position=mark22.transform.position;
    }
    if(markpoint==21){
        Marker.transform.position=mark23.transform.position;
    }
    if(markpoint==22){
        Marker.transform.position=mark24.transform.position;
    }
    if(markpoint==23){
        Marker.transform.position=mark25.transform.position;
    }
    if(markpoint==24){
        Marker.transform.position=mark26.transform.position;
    }
    if(markpoint==25){
        Marker.transform.position=mark27.transform.position;
    }
    if(markpoint==26){
        Marker.transform.position=mark28.transform.position;
    }
    if(markpoint==27){
        Marker.transform.position=mark29.transform.position;
    }
    if(markpoint==28){
        Marker.transform.position=mark30.transform.position;
    }
    if(markpoint==29){
        Marker.transform.position=mark31.transform.position;
    }
    if(markpoint==30){
        Marker.transform.position=mark32.transform.position;
    }
    if(markpoint==31){
        Marker.transform.position=mark33.transform.position;
    }
    if(markpoint==32){
        Marker.transform.position=mark34.transform.position;
    }
    if(markpoint==33){
        Marker.transform.position=mark35.transform.position;
    }
    if(markpoint==34){
        Marker.transform.position=mark36.transform.position;
    }
    if(markpoint==35){
        Marker.transform.position=mark37.transform.position;
    }
    if(markpoint==36){
        Marker.transform.position=mark38.transform.position;
    }
    if(markpoint==37){
        Marker.transform.position=mark39.transform.position;
    }
    if(markpoint==38){
        Marker.transform.position=mark40.transform.position;
    }
    if(markpoint==39){
        Marker.transform.position=mark41.transform.position;
    }
    if(markpoint==40){
        Marker.transform.position=mark42.transform.position;
    }
    if(markpoint==41){
        Marker.transform.position=mark43.transform.position;
    }
    if(markpoint==42){
        Marker.transform.position=mark44.transform.position;
    }
    if(markpoint==43){
        Marker.transform.position=mark45.transform.position;
    }
    if(markpoint==44){
        Marker.transform.position=mark46.transform.position;
    }
    
   
    markpoint+=1;
    yield return new WaitForSeconds(1.5f);
    Marker.GetComponent<BoxCollider>().enabled=true;
   }
}
