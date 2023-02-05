public struct BallParameters
{
    #region Fields

    #endregion

    #region Propeties

    public int SpeedLimitBoost { get; private set; }
    public int StartSpeedBoost { get; private set; }

    #endregion

    #region Methods

    public BallParameters(int speedLimitBoost, int startSpeedBoost)
    {
        SpeedLimitBoost = speedLimitBoost;
        StartSpeedBoost = startSpeedBoost;
    }

    #endregion

    #region Enums



    #endregion
}
