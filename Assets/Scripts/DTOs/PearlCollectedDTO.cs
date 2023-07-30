public class PearlCollectedDTO
{
    public PowerSO powerData;
    public string playerName;
    public string bonusID;

    public PearlCollectedDTO(PowerSO powerData, string playerName, string bonusID)
    {
        this.powerData = powerData;
        this.playerName = playerName;
        this.bonusID = bonusID;
    }
}