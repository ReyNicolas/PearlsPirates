using System;

public interface IPlayerPointsGiver
{
    event Action<string, int> OnGivePlayerPoints; 
}

