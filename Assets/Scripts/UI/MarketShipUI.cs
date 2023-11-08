using System.Collections.Generic;
using UnityEngine;

public class MarketShipUI : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> imagesToSetColors;
    [SerializeField] MarketShip shipPearlsGetter;

    private void Awake()
    {
        shipPearlsGetter.OnChangeColors += SetColors;
    }
    private void OnDestroy()
    {
        shipPearlsGetter.OnChangeColors -= SetColors;
    }



    void SetColors(List<Color> colors)
    {
        imagesToSetColors.ForEach(ic => ic.gameObject.SetActive(false));
        for(int i = 0; i < colors.Count; i++)
        {
            imagesToSetColors[i].gameObject.SetActive(true);
            imagesToSetColors[i].color = colors[i];
        }
    }
}

