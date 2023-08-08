using System.Collections;
using UnityEngine;

public class InstantPearl : MonoBehaviour
{
    GameObject content;

    private void Start()
    {
        StartCoroutine(StartCountDownToDestroy());
    }

    IEnumerator StartCountDownToDestroy()
    {
        yield return new WaitForSeconds(4);
        content.transform.position = transform.position;
        content.SetActive(true);
        Destroy(gameObject);
        
    }

    public void SetContent(GameObject content)
    {
        this.content = content;
    }


}
