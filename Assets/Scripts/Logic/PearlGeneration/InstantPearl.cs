using System.Collections;
using UnityEngine;

public class InstantPearl : MonoBehaviour
{
    GameObject content;
    [SerializeField] GameObject CreationEffectPrefab;

    private void Start()
    {
        StartCoroutine(StartCountDownToDestroy());
    }

    IEnumerator StartCountDownToDestroy()
    {
        yield return new WaitForSeconds(4);
        content.transform.position = transform.position;
        content.SetActive(true);
        Destroy(Instantiate(CreationEffectPrefab, transform.position, Quaternion.identity), 1.5f);
        Destroy(gameObject);
        
    }

    public void SetContent(GameObject content)
    {
        this.content = content;
    }


}
