public class PearlCollectedDTO
{
    public SelectionPearl pearl;
    public int playerID;
    public string bonusID;

    public PearlCollectedDTO(SelectionPearl pearl, int playerID, string bonusID)
    {
        this.pearl = pearl;
        this.playerID = playerID;
        this.bonusID = bonusID;
    }
}