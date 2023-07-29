public class PearlCollectedDTO
{
    public SelectionPearl pearl;
    public string playerName;
    public string bonusID;

    public PearlCollectedDTO(SelectionPearl pearl, string playerName, string bonusID)
    {
        this.pearl = pearl;
        this.playerName = playerName;
        this.bonusID = bonusID;
    }
}