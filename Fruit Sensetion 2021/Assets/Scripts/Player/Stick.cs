using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
namespace GamerWolf.FruitSensetion{
    public class Stick : MonoBehaviour {
        

        [SerializeField] private float offsetX = 0.1f,OffsetYTop = 0.1f,OffsetYBottom = 2.5f;
        [SerializeField] private float followSpeed = 100f;
        [SerializeField] private Transform ropeHook;
        
        private Vector3 dragOffset;
        private PlayerInputs inputs;
        
        private void Awake(){
            inputs = GetComponent<PlayerInputs>();
        }
        private void OnMouseDown(){
            #if UNITY_EDITOR
            RotateTowardRopeHook();
            dragOffset = transform.position - inputs.GetMousePosition();
            #endif
        }
        private void OnMouseDrag(){
            #if UNITY_EDITOR
            RotateTowardRopeHook();
            Vector3 pos = inputs.GetMousePosition() + dragOffset;
            // Horizontal Check......
            if(pos.x + offsetX > inputs.GetScreenSize().x){
                pos.x = inputs.GetScreenSize().x - offsetX;
            }
            else if(pos.x - offsetX < -inputs.GetScreenSize().x){
                pos.x = -inputs.GetScreenSize().x + offsetX;
            }
            // Top Check...
            if(pos.y + OffsetYTop > inputs.GetScreenSize().y){
                pos.y = inputs.GetScreenSize().y - OffsetYTop;
            }
            else if(pos.y - OffsetYBottom < -inputs.GetScreenSize().y){
                pos.y = -inputs.GetScreenSize().y + OffsetYBottom;
            }
            transform.position = Vector2.MoveTowards(transform.position, pos, followSpeed * Time.deltaTime);
            #endif
        }
        

        private void Update(){
            #if UNITY_ANDROID
            if(inputs.OnTouchDown()){
                RotateTowardRopeHook();
                dragOffset = transform.position - inputs.GetMousePosition();
            }
            if(inputs.OnTouchMove()){
                RotateTowardRopeHook();
                Vector3 pos = inputs.GetMousePosition() + dragOffset;
                // Horizontal Check......
                if(pos.x + offsetX > inputs.GetScreenSize().x){
                    pos.x = inputs.GetScreenSize().x - offsetX;
                }
                else if(pos.x - offsetX < -inputs.GetScreenSize().x){
                    pos.x = -inputs.GetScreenSize().x + offsetX;
                }
                // Top Check...
                if(pos.y + OffsetYTop > inputs.GetScreenSize().y){
                    pos.y = inputs.GetScreenSize().y - OffsetYTop;
                }
                else if(pos.y - OffsetYBottom < -inputs.GetScreenSize().y){
                    pos.y = -inputs.GetScreenSize().y + OffsetYBottom;
                }
                transform.position = Vector2.MoveTowards(transform.position, pos, followSpeed * Time.deltaTime);
            }
            #endif
        }
        private void RotateTowardRopeHook(){
            Vector3 dir = (ropeHook.position - transform.position).normalized;
            dir.z = 0f;
            float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0f,0f,-angle);
        }
        
        
    }

}