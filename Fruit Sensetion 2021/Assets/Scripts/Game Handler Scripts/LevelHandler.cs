using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
namespace GamerWolf.FruitSensetion{

    public class LevelHandler : MonoBehaviour {

        [Header("Level Data")]
        [SerializeField] private LevelDataSO levelDataSO;
        [SerializeField] private List<Basket> basketsList;
        [SerializeField] private List<Transform> basketPositionList;
        private List<Vector3> spawnPointList;
        private ObjectPoolingManager objectPoolingManager;

        #region Singleton......

        public static LevelHandler i;

        private void Awake(){
            if(i == null){
                i = this;
            }else{
                Destroy(i.gameObject);
            }
            objectPoolingManager = GetComponent<ObjectPoolingManager>();
        }

        #endregion

        private void Start(){
            spawnPointList = new List<Vector3>();
            SpawnBaskets();
        }

        private void SpawnBaskets(){
            foreach(BasketTypes basketTypes in levelDataSO.basketTypesList){
                for (int i = 0; i < basketsList.Count; i++){
                    Basket spawnbasket = basketsList[i];
                    if(basketTypes == spawnbasket.GetBasketTypes()){
                        Transform spawnPoint = GetRandomBasketPosition();
                        Basket basket = Instantiate(spawnbasket,spawnPoint.position,Quaternion.identity);
                        SetSpawnPoint(basket.GetFruitSpawnPoint());
                        basketsList.Remove(spawnbasket);
                        basketPositionList.Remove(spawnPoint);
                    }
                    
                }
            }
            
            
        }
        public void SpawnFruit(){
            GameObject pooledObject = objectPoolingManager.SpawnRandomFromPool(GetRandomFruitSpawnPoint(),Quaternion.identity);
            Fruit fruit =pooledObject.GetComponent<Fruit>();
            if(fruit != null){
                if(!levelDataSO.basketTypesList.Contains(fruit.GetFruitBasket())){
                    fruit.DestroyMySelf();
                }
            }
        }
        private void SetSpawnPoint(Vector3 spawnPoint){
            spawnPointList.Add(spawnPoint);
        }
        private Vector3 GetRandomFruitSpawnPoint(){
            int randomSpawnPoint = UnityEngine.Random.Range(0,spawnPointList.Count);
            return spawnPointList[randomSpawnPoint];

        }
        
        private Transform GetRandomBasketPosition(){
            int randomPoint = UnityEngine.Random.Range(0,basketPositionList.Count);
            return basketPositionList[randomPoint];
        }
    
    }

}