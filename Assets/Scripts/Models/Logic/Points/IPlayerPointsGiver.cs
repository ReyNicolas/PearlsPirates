using System;

public interface IPlayerPointsGiver
{
    event Action<int, int> OnGivePlayerPoints; 
}

