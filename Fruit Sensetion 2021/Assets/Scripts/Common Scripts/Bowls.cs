using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.FruitSensetion{
    public class Bowls : MonoBehaviour {
        
        
        protected virtual void OnTriggerEnter2D(Collider2D coli2D){
            if(coli2D.gameObject.CompareTag(gameObject.tag)){
                Fruit fruit = coli2D.GetComponent<Fruit>();
                if(fruit != null){
                    GameHandler.i.SetFruitCounts(1);
                    fruit.DestroyMySelf();
                }
            }
        }
    }

}