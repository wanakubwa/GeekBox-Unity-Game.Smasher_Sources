using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerData
{
    [Serializable]
    public class PlayerWallet
    {
        #region Fields

        [SerializeField]
        private int coins;
        [SerializeField]
        private int currentLvlNo = Constants.DEFAULT_NO_VALUE;

        [SerializeField]
        private int speedLimitBoost;
        [SerializeField]
        private int startSpeedBoost;

        [SerializeField]
        private List<LvlData> completedLvls = new List<LvlData>();

        #endregion

        #region Propeties

        /// <summary>
        /// int - aktualna liczba monet.
        /// </summary>
        public event Action<int> OnCoinsChange = delegate { };

        /// <summary>
        /// int - aktualny nr poziomu.
        /// </summary>
        public event Action<int> OnLvlNoChange = delegate { };

        public int Coins { 
            get => coins;
            private set => coins = value;
        }
        public int CurrentLvlNo {
            get => currentLvlNo;
            private set => currentLvlNo = value;
        }
        public int SpeedLimitBoost { get => speedLimitBoost; private set => speedLimitBoost = value; }
        public int StartSpeedBoost { get => startSpeedBoost; private set => startSpeedBoost = value; }
        public List<LvlData> CompletedLvls { get => completedLvls; private set => completedLvls = value; }

        #endregion

        #region Methods

        public PlayerWallet() { }
        public PlayerWallet(PlayerWallet source)
        {
            this.Coins = source.coins;
            this.CurrentLvlNo = source.CurrentLvlNo;
            this.SpeedLimitBoost = source.SpeedLimitBoost;
            this.StartSpeedBoost = source.StartSpeedBoost;
            this.CompletedLvls = new List<LvlData>(source.CompletedLvls);
        }

        public void AddLvlData(LvlData data)
        {
            CompletedLvls.Add(data);
        }

        public void AddCoins(int value)
        {
            Coins += value;
            OnCoinsChange(Coins);
        }

        public void SetCurrentLvlNo(int no)
        {
            CurrentLvlNo = no;
            OnLvlNoChange(CurrentLvlNo);
        }

        public void AddSpeedLimitBoost(int value)
        {
            SpeedLimitBoost += value;
        }

        public void AddStartSpeedBoost(int value)
        {
            StartSpeedBoost += value;
        }

        public BallParameters GetBallParameters()
        {
            return new BallParameters(SpeedLimitBoost, StartSpeedBoost);
        }

        public bool CanAfford(int coinsValue)
        {
            return Coins - coinsValue >= Constants.DEFAULT_VALUE;
        }

        public int GetTotalScore()
        {
            int score = 0;
            CompletedLvls.ForEach(x => score += x.Score);
            return score;
        }

        #endregion

        #region Enums



        #endregion
    }
}
