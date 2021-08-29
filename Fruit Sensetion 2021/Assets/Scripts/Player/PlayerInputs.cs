using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace GamerWolf.Utils{
    public class PlayerInputs : MonoBehaviour {
        

        
        private Camera cam;
        private Vector3 screenSize;
        private Collider2D coli2D;
        public static bool isGamePause;
        private Collider2D touchCollider;
        private void Awake(){
            coli2D = GetComponent<Collider2D>();
            cam = Camera.main;
        }

        private void Start(){
            screenSize = cam.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0f));
            Debug.Log("Screen Size " + screenSize);
        }
        private void Update(){
            if(Input.GetKeyDown(KeyCode.Escape)){
                isGamePause = true;
            }
        }
        public void UnPauseGame(){
            if(isGamePause){
                isGamePause = false;
            }
        }
        public bool OnTouchDown(){
            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                int fingerId = touch.fingerId;
                if(!EventSystem.current.IsPointerOverGameObject(fingerId) && touch.phase == TouchPhase.Began){
                    Vector2 touchPos = GetTouchWorldPosition(touch);
                    touchCollider = Physics2D.OverlapPoint(touchPos);
                    if(touchCollider == coli2D){
                        return true;
                    }else{
                        return false;
                    }
                }
            }
            return false;
        }

        public bool OnTouchMove(){
            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                int fingerId = touch.fingerId;
                if(!EventSystem.current.IsPointerOverGameObject(fingerId) && touch.phase == TouchPhase.Moved){
                    if(touchCollider != null){
                        return true;
                    }
                }
            }
            return false;
        }
        public bool OnTouchUp(){
            return Input.touchCount <= 0 || touchCollider == null;
        }
        public Vector2 GetTouchWorldPosition(Touch _touch){
            return cam.ScreenToWorldPoint(_touch.position);
        }
        public Vector3 GetMousePosition(){
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
        public Vector3 GetScreenSize(){
            return screenSize;
        }
    }

}