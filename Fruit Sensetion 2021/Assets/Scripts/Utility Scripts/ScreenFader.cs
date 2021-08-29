using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
namespace GamerWolf.Utils{
    [RequireComponent(typeof(MaskableGraphic))]
    public class ScreenFader : MonoBehaviour {
        
        #region Variables.........................................
        [SerializeField] private Color solidColor = Color.white,clearColor = new Color(1f,1f,1f,0f);
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private float timeToFade = 1f;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
        [SerializeField] private UnityEvent OnFadeOut;
        private MaskableGraphic graphic;

        #endregion
        #region Methods.

        private void Awake() {
            graphic = GetComponent<MaskableGraphic>();
        }
        private void Start(){
            UpdateColor(solidColor);
        }
        private void UpdateColor(Color _color){
            graphic.color = _color;
        }
        public void FadeOff(){
            StartCoroutine(FadeOffRoutine());
        }
        private IEnumerator FadeOffRoutine(){
            iTween.ValueTo(gameObject,iTween.Hash(
                "from",solidColor,
                "to",clearColor,
                "dealyTime",delay,
                "easeType",easeType,
                "OnUpdateTarget", gameObject,
                "onUpdate","UpdateColor"
            ));
            yield return new WaitForSeconds(timeToFade);
            OnFadeOut?.Invoke();
        }

        
        

        #endregion


    }

}
