using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace GamerWolf.Utils{

    public class SavingAndLoadingManager : MonoBehaviour{
        public static SavingAndLoadingManager instance {get;private set;}
        
        public SaveData saveData;
        
        private void Awake(){
            if(instance == null){
                instance = this;
            }else{
                Destroy(instance);
            }
            DontDestroyOnLoad(instance);
            
            LoadGame();
            

        }
        [ContextMenu("SAVE GAME")]
        public void SaveGame(){
            // saveData.settingsData.Save();
            // for (int i = 0; i < saveData.levelData.Length; i++){
            //     saveData.levelData[i].Save();
            // }
            // saveData.coins.Save();
        }
        [ContextMenu("LOAD GAME")]
        public void LoadGame(){
            // saveData.settingsData.Load();
            // for (int i = 0; i < saveData.levelData.Length; i++){
            //     saveData.levelData[i].Load();
            // }
            // saveData.coins.Load();
        }      
        private void OnApplicationQuit(){
            SaveGame();
        }  
    }
    [System.Serializable]
    public struct SaveData{
        public SavingDataSO[] savedLevelData;
        public SavingDataSO[] settingsData;
    }

}