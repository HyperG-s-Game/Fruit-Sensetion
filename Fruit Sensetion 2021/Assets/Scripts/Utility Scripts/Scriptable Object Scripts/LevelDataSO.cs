using System.IO;
using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace GamerWolf.FruitSensetion{

    [CreateAssetMenu(fileName = "Level Data",menuName = "ScriptableObject/New Level Data")]
    public class LevelDataSO : SavingDataSO {
        
        public SceneIndex sceneIndex;
        public List<BasketTypes> basketTypesList;
        public bool isCompleted = false;

        public void SetLevelComplete(bool isComplete){
            isCompleted = isComplete;
        }


        [ContextMenu("Save")]
        public void Save(){
            string data = JsonUtility.ToJson(this,true);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath,"/","Level Data",sceneIndex));
            formatter.Serialize(file,data);
            file.Close();
        }

        [ContextMenu("Load")]
        public void Load(){
            if(File.Exists((string.Concat(Application.persistentDataPath,"/","Level Data",sceneIndex)))){
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream Stream = File.Open(string.Concat(Application.persistentDataPath,"/","Level Data",sceneIndex),FileMode.Open);
                JsonUtility.FromJsonOverwrite(formatter.Deserialize(Stream).ToString(),this);
                Stream.Close();
            }
        }
    }
    

}
