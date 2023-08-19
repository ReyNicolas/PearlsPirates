using System.Collections.Generic;
using UnityEngine;

public class MarketPierColorsUI:MonoBehaviour
{
    [SerializeField] List<GameObject> spritesGo;
    [SerializeField] MarketPier marketPier;
    private void Start()
    {
        marketPier.OnChangeColors += SetColors;
    }

     void SetColors(List<Color> colors)
    {
        spritesGo.ForEach(sp => sp.SetActive(false));
        for(int i = 0; i < colors.Count; i++)
        {
            spritesGo[i].SetActive(true);
            spritesGo[i].GetComponent<SpriteRenderer>().color = colors[i];
        }
    }
}