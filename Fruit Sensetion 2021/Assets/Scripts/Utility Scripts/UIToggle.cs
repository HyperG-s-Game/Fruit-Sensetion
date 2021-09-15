using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIToggle : MonoBehaviour {
    
    
    
    public GameObject onImage,offImage;

    
    public void SetToggle(bool on){
        if(on){
            
            onImage.SetActive(true);
            offImage.SetActive(false);
        }else{
            
            onImage.SetActive(false);
            offImage.SetActive(true);
        }
    }
}
