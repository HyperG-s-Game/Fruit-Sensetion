using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
namespace GamerWolf.FruitSensetion{

    public class LevelHandler : MonoBehaviour {
        
        
        [SerializeField] private Transform[] spawnPointArray;
        private ObjectPoolingManager objectPoolingManager;
        private void Start(){
            objectPoolingManager = ObjectPoolingManager.i;
        }
        public void SpawnFruit(){
            int randNum = UnityEngine.Random.Range(0,3);
            Debug.Log("RandomNum " + randNum);
            if(randNum == 3){
                randNum = UnityEngine.Random.Range(0,3);
            }
            switch(randNum){
                case 0:
                    objectPoolingManager.SpawnFromPool(PoolObjectTag.Mango,GetRandomSpawnPoint().position,Quaternion.identity);
                break;
                case 1:
                    objectPoolingManager.SpawnFromPool(PoolObjectTag.Apple,GetRandomSpawnPoint().position,Quaternion.identity);
                break;
                case 2:
                    objectPoolingManager.SpawnFromPool(PoolObjectTag.Lemon,GetRandomSpawnPoint().position,Quaternion.identity);
                break;
            }
        }
        private Transform GetRandomSpawnPoint(){
            int randomSpawnPoint = UnityEngine.Random.Range(0,spawnPointArray.Length);
            return spawnPointArray[randomSpawnPoint];

        }
    
    }

}