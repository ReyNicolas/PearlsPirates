using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PearlCollectedUI: MonoBehaviour
{
    [SerializeField] float timeAlive;
    Image image;
    [SerializeField]Image cover;

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
            ReduceAlpha(image,inc);
            ReduceAlpha(cover, inc);
            yield return new WaitForSeconds(timeAlive / 10);
            inc++;
        }

    }

    private void ReduceAlpha(Image imageToReduceColor, int inc)
    {
        imageToReduceColor.color = new Color(imageToReduceColor.color.r, imageToReduceColor.color.b, imageToReduceColor.color.b, 1 - (0.1f * inc));
    }
}
