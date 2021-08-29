using System;
using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System.Collections.Generic;
namespace GamerWolf.FruitSensetion{
    public class RewardedAdManager : MonoBehaviour {
        
        [SerializeField] private string adUnitID =  "ca-app-pub-3940256099942544/5224354917";

        private string appID = "ca-app-pub-1447736674902262~3907766596";
        private RewardedAd rewardedAd;
        private bool isShowingAd;
        private bool isRewarded;
        private bool hasAddInGame;
        #region Singleton.......

        public static RewardedAdManager i {get;private set;}
        private void Awake(){
            if(i == null){
                i = this;
            }
        }
        
        #endregion


        private void Start(){
            hasAddInGame = true;
            // hasAddInGame = !PlayerPrefs.HasKey("ADD");
            if(hasAddInGame){
                MobileAds.Initialize((InitializationStatus) =>{
                    Debug.Log(InitializationStatus);
                });
            }
            if(hasAddInGame){
                rewardedAd = new RewardedAd(adUnitID);

                rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
                rewardedAd.OnAdOpening += HandleRewardedAdOpening;
                rewardedAd.OnAdLoaded += HandleRewardedAdOpening;
                rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
                rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
                rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded ad with the request.
                rewardedAd.LoadAd(request);

            }
            
        }
        

        
        
        public void TryShowAd(){
            if(rewardedAd.IsLoaded()){
                rewardedAd.Show();
            }
        }
        public void HandleRewardedAdLoaded(object sender, EventArgs args){
            GameHandler.i.SetHasAd(true);
            
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args){
            GameHandler.i.SetHasAd(false);
            
            Debug.Log(
                "HandleRewardedAdFailedToLoad event received with message: "
                                + args.Message);
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args){
            GameHandler.i.SetHasAd(true);
            Debug.Log("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args){
            GameHandler.i.SetHasAd(false);
            Debug.Log(
                "HandleRewardedAdFailedToShow event received with message: "
                                + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args){
            GameHandler.i.SetHasAd(false);
            Debug.Log("HandleRewardedAdClosed event received");
        }

        public void HandleUserEarnedReward(object sender, Reward args){
            string type = args.Type;
            double amount = args.Amount;
            Debug.Log("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
            GameHandler.i.GetSecondChanceAfterAD();
        }
    }

}