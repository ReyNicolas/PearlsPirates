using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketShipUI : MonoBehaviour
{
    [SerializeField] List<Image> imagesToSetColors;
    [SerializeField] MarketShip shipPearlsGetter;

    private void Awake()
    {
        shipPearlsGetter.OnChangeColors += SetColors;
        shipPearlsGetter.OnDestroy+= (ship => Destroy(gameObject));
        transform.SetParent(GameObject.FindWithTag("ScreenCanvas").transform);
    }

    private void Update()
    {
        transform.position = shipPearlsGetter.transform.position + Vector3.right;
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

