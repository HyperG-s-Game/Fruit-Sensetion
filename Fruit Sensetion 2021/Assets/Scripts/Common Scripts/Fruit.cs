using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
namespace GamerWolf.FruitSensetion{

    public class Fruit : MonoBehaviour, IPooledObject{
        [SerializeField] protected BasketTypes basketType;
        [SerializeField] private float rotationForce = 5f;
        private Rigidbody2D rb2D;

        private void Awake(){
            rb2D = GetComponent<Rigidbody2D>();
        }
        
        public void DestroyMySelf(){
            gameObject.SetActive(false);
        }

        public void OnObjectReuse(){
            rb2D.AddTorque(rotationForce);
            // Debug.Log(transform.name + " is Reused");
        }

        
        
        
        private void OnCollisionEnter2D(Collision2D coli2D){
            if(coli2D.gameObject.CompareTag("Player")){
                return;
            }
            Invoke(nameof(DestroyMySelf),5f);
        }

        private void OnTriggerEnter2D(Collider2D coli2D){
            if(coli2D.gameObject.CompareTag("Player")){
                return;
            }
            if(!coli2D.gameObject.CompareTag(gameObject.tag)){
                DestroyMySelf();
                GameHandler.i.SetFruitDrop(true);
                // Debug.Log(transform.name + " Miss the Bowls");
            }
            
        }
        public BasketTypes GetFruitBasket(){
            return basketType;
        }
    }

}