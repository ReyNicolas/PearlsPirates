using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IAMoveControlLogic: MonoBehaviour
{
    [SerializeField] MoveToPositionLogic moveToPositionLogic;
    public PlayerSO data;
    float timer;

    private void Awake()
    {
        moveToPositionLogic.OnArriveToPosition += FindPositionToMove;        
    }

   

    private void OnDestroy()
    {
        moveToPositionLogic.OnArriveToPosition -= FindPositionToMove;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
       if(timer <= 0)  FindPositionToMove();
    }

    void FindPositionToMove()
    {

        var marketsToPay = IMarket.MarketList
                    .Where(m
                    => m.HaveAllColorsToCollect(
                        data.powersInCollectors
                        .Where(p=>p.Value != null)
                            .Select(p
                            => p.Value.PowerColor).ToList()));

        if (marketsToPay.Count()  > 0)
        {
            moveToPositionLogic
                .SetPositionToMove(
                    ClosePosition(
                        marketsToPay
                        .Select(m => m.transform.position)));
            timer = 1f;
            return;
        }

        

        moveToPositionLogic
            .SetPositionToMove( 
                ClosePosition( 
                    PearlToObtain.pearlToObtains
                    .Select(pto => pto.transform.position)));
        timer = Random.Range(2,5);
    }

   

    Vector2 ClosePosition(IEnumerable<Vector3> positions)
    {
        Vector2 closePosition = Vector2.positiveInfinity;
        Vector2 actualPosition = transform.position;
        float closeDistance = float.PositiveInfinity;
        float distance;

        foreach(Vector2 position in positions) 
        {
            distance = Vector2.Distance(actualPosition, position);
            if (distance< closeDistance)
            {
                closePosition = position;
                closeDistance = distance;
            }
        }
        return closePosition;
    }
}