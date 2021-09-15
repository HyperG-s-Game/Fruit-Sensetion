using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GamerWolf.Utils;
namespace GamerWolf.FruitSensetion {

    public class Menu : MonoBehaviour {


        public void PlayGame(){
            LevelLoader.instance.PlayLevel();
        }    
        
    }

}