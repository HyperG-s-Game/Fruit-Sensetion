using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.FruitSensetion {
    
    public class Rope : MonoBehaviour {

        [SerializeField] private Transform PointA,PointB;
        [SerializeField] private float ropeSegementLength = 0.25f;
        [SerializeField] private int iteration = 30;
        [SerializeField] private int segementsLength = 25;
        [SerializeField] private float ropeWidth = .3f;
        [SerializeField] private Vector2 forceGravity = new Vector2(0f, -1.5f);
        private List<RopeSegements> ropeSegements = new List<RopeSegements>();
        
        private LineRenderer lr;
        

        private void Start(){
            ropeSegements = new List<RopeSegements>();
            lr = GetComponent<LineRenderer>();
            Vector3 ropeStart = (Vector2)transform.position;
            for (int i = 0; i < segementsLength; i++) {
                ropeSegements.Add(new RopeSegements(ropeStart));
                ropeStart.y -= ropeSegementLength;
            }
            
        }

        private void Update(){
            DrawRope();
        }
        private void FixedUpdate(){
            SimulateRope();
        }
        
        private void SimulateRope(){
            // SIMULATION......
            for (int i = 0; i < segementsLength; i++){
                RopeSegements firstSeg = ropeSegements[i];
                Vector2 velocity = firstSeg.posNow - firstSeg.posOld;
                firstSeg.posOld = firstSeg.posNow;
                firstSeg.posNow += velocity;
                firstSeg.posNow += forceGravity * Time.deltaTime;
                ropeSegements[i] = firstSeg;
            }
            // CONSTRAINS;
            for (int i = 0; i < iteration; i++){
                ApplyConstrain();
            }
        }
        private void ApplyConstrain(){
            RopeSegements firestSegment = ropeSegements[0];
            firestSegment.posNow = (Vector2)PointA.position;
            ropeSegements[0] = firestSegment;
            
            RopeSegements endSegemnt = ropeSegements[segementsLength - 1];
            endSegemnt.posNow = (Vector2)PointB.position;
            ropeSegements[segementsLength - 1] = endSegemnt;

            for (int i = 0; i < segementsLength - 1; i++){
                RopeSegements firstSeg = ropeSegements[i];
                RopeSegements secondSeg = ropeSegements[i + 1];

                float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
                float error = Mathf.Abs(dist - ropeSegementLength);
                Vector2 changeDir = Vector2.zero;

                if(dist > ropeSegementLength){
                    changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;

                }else if(dist < ropeSegementLength){
                    changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;

                }

                Vector2 changeAmount = changeDir * error;
                if(i != 0){
                    firstSeg.posNow -= changeAmount * 0.5f;
                    ropeSegements[i] = firstSeg;
                    secondSeg.posNow += changeAmount * 0.5f;
                    ropeSegements[i + 1] = secondSeg;

                }else{
                    secondSeg.posNow += changeAmount;
                    ropeSegements[i + 1] = secondSeg;
                }
            }
        }
        private void DrawRope(){
            float lineWidth = ropeWidth;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;


            Vector3[] ropePositions = new Vector3[segementsLength];
            for (int i = 0; i < segementsLength; i++){
                ropePositions[i] = ropeSegements[i].posNow;

            }
            lr.positionCount = ropePositions.Length;
            lr.SetPositions(ropePositions);
        }
        
    }

    public struct RopeSegements{
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegements(Vector2 pos){
            this.posNow = pos;
            this.posOld = pos;
        }
    }

}