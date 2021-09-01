using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.FruitSensetion{
    public enum BasketTypes{
        AppleBasket,OrangeBasket,MangoBasket,WaterMelonBasket,RedCherryBasekt,
    }
    public class Basket : MonoBehaviour {
        [SerializeField] protected BasketTypes basketType;        
        [SerializeField] private Transform fruitSpawnpoint;
        protected virtual void OnTriggerEnter2D(Collider2D coli2D){
            if(coli2D.gameObject.CompareTag(gameObject.tag)){
                Fruit fruit = coli2D.GetComponent<Fruit>();
                if(fruit != null){
                    GameHandler.i.SetFruitCounts(1);
                    fruit.DestroyMySelf();
                }
            }
        }
        public Vector3 GetFruitSpawnPoint(){
            return fruitSpawnpoint.position;
        }
        public BasketTypes GetBasketTypes(){
            return basketType;
        }
    }

}