using UnityEngine;
using GamerWolf.Utils;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GamerWolf.FruitSensetion{
    public class GameHandler : MonoBehaviour {

        


        [Header("Timer Names")]
        [SerializeField] private string gameTimerName = "Game Timer";
        [SerializeField] private string fruitSpawningTimerName = "Fruit";

        [Header("Timer Attributes")]
        [SerializeField] private int maxTime = 60;
        [SerializeField] private float timePerSecond = 1f;
        [SerializeField] private int maxLife = 5;
        [SerializeField] private int chancesForAD = 2;
        [SerializeField] private float maxFruitSpawnTime = 3f;
        [SerializeField] private bool hasAd;

        [Header("Events")]
        [SerializeField] private UnityEvent OnGameStart;
        [SerializeField] private UnityEvent OnGamePlaying,OnGameEnd,OnGamePause,OnGameResume,OnPlayerWin,OnPlayerLoss;
        [SerializeField] private UnityEvent OnSetSecondChanceToPlayer,OnGetSecondChancetoPlayer,OnPlayerLossWithAd;
        
        [Header("Debug Attributes")]
        [SerializeField] private bool isGameStart;
        [SerializeField] private bool fruitDrop;
        [SerializeField] private bool isGameOver;
        [SerializeField] private int collectedFruitCounts;

        private int currentTime;
        private LevelHandler levelHandler;
        private UiHandler uiHandler;

        #region Singelton..........
        public static GameHandler i{get;private set;}    
        private void Awake(){
            #if UNITY_ANDROID
                Debug.unityLogger.logEnabled = false;
                Application.targetFrameRate = 60;
            #endif

            if(i == null){
                i = this;
            }else {
                Destroy(i.gameObject);
            }

            
        }
        #endregion
        
        
        
        private void Start(){
            uiHandler = UiHandler.i;
            levelHandler = LevelHandler.i;
            StartCoroutine(nameof(StartGameRoutine));
        }


        private IEnumerator StartGameRoutine(){
            OnGameStart?.Invoke();
            while(!isGameStart){
                yield return null;
            }
            yield return StartCoroutine(nameof(GamePlayRoutine));
        }
        private IEnumerator GamePlayRoutine(){

            OnGamePlaying?.Invoke();
            OnGameResume?.Invoke();
            StartTimers();
            while(!isGameOver){
                PlayPauseGame(PlayerInputs.isGamePause);
                if(currentTime < maxTime){
                    if(fruitDrop){
                        if(chancesForAD > 0){
                            if(maxLife > 0){
                                yield return StartCoroutine(NextChanceRoutine());
                            }else if(hasAd && maxLife <= 0){
                                SetGameOver(false,true);
                                SetSecondChanceAfterAd();
                            }else{
                                SetGameOver(false,false);
                            }
                        }else {
                            SetGameOver(false,false);
                        }
                    }
                }else {
                    SetGameOver(true,false);
                }
                yield return null;
            }
            OnGameEnd?.Invoke();
            RemoveTimers();
            
        }

        
        
        private void StartTimers(){
            TimerTickSystem.CreateTimer(IncreaseTimer,timePerSecond,gameTimerName);

            TimerTickSystem.CreateTimer(SpawnItems,maxFruitSpawnTime,fruitSpawningTimerName);
        }
        private void IncreaseTimer(){
            currentTime++;
            uiHandler.ShowTimerText(currentTime.ToString());
        }
        private void SpawnItems(){
            levelHandler.SpawnFruit();
        }
        private void RemoveTimers(){
            TimerTickSystem.StopTimer(gameTimerName);
            TimerTickSystem.StopTimer(fruitSpawningTimerName);
        }
        private void PauseTimers(bool _pausing){
            TimerTickSystem.PauseTimers(gameTimerName,_pausing);
            TimerTickSystem.PauseTimers(fruitSpawningTimerName,_pausing);
        }
        
        public void SetSecondChanceAfterAd(){
            OnSetSecondChanceToPlayer?.Invoke();
        }
        
        public void GetSecondChanceAfterAD(){
            
            chancesForAD--;
            hasAd = false;
            currentTime -= 5;
            fruitDrop = false;
            PauseTimers(false);
            OnGetSecondChancetoPlayer?.Invoke();
        }
        private IEnumerator NextChanceRoutine(){
            PauseTimers(true);
            maxLife--;
            UiHandler.i.SetLivesLeft(maxLife);
            yield return new WaitForSeconds(0.4f);
            fruitDrop = false;
            PauseTimers(false);
        }
        public void PlayPauseGame(bool isPause){
            if(isPause){
                Time.timeScale = 0f;
                OnGamePause?.Invoke();
            }else{
                Time.timeScale = 1f;
                OnGameResume?.Invoke();
            }
        }
        public void SetGameOver(bool isWin,bool showAD){
            if(isWin){
                isGameOver = true;
                OnPlayerWin?.Invoke();
            }
            if(!isWin && !showAD){
                isGameOver = true;
                OnPlayerLoss?.Invoke();
            }
            if(showAD && !isWin){
                isGameOver = false;
                OnPlayerLossWithAd?.Invoke();
            }
        }
        public void Restart(){
            OnGameResume?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Play(){
            isGameStart = true;
        }

        public void SetFruitDrop(bool isDrop){
            fruitDrop = isDrop;
        }
        public void SetFruitCounts(int count){
            collectedFruitCounts += count;
        }
        public void SetHasAd(bool _hasAD){
            hasAd = _hasAD;
        }
    }

}