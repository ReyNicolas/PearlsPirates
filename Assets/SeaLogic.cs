using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLogic : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] float speed;
    [SerializeField] float timeFlip;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void Update()
    {
        transform.Translate(matchData.wind * speed * Time.deltaTime);
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeFlip);
            speed *= -1;
        }
    }
}
