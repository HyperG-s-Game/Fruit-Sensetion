using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.FruitSensetion {
    public class UiHandler : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI timerText;
        // [SerializeField] private Image[] liveCountImages;
        // [SerializeField] private Color deathColor;

        #region Singelton........
        public static UiHandler i;
        private void Awake(){
            if(i == null){
                i = this;
            }else{
                Destroy(i.gameObject);
            }

        }
        #endregion


        public void ShowTimerText(string _time){
            timerText.SetText(_time);
        }
        public void SetLivesLeft(int currentLive){
            // To Do..........
            // for (int i = 0; i < currentLive; i++){
            //     liveCountImages[i].color = deathColor;
            // }
        }
    }

}