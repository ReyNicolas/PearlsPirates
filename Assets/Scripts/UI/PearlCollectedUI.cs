using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PearlCollectedUI: MonoBehaviour
{
    [SerializeField] float timeAlive;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Play());
        StartCoroutine(Die());
    }

    private void Update()
    {
        transform.Translate(Vector2.up  *Time.deltaTime);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(timeAlive);
        Destroy(gameObject);
    }

    IEnumerator Play()
    {
        int inc = 0;
        while (true)
        {
            image.color = new Color(image.color.r, image.color.r, image.color.b, 1 -(0.1f * inc));
            yield return new WaitForSeconds(timeAlive/10);
            inc++;
        }
        
    }
}
