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
        MarketShip.OnDestroy += TryDestroyMe;
        transform.SetParent(GameObject.FindWithTag("ScreenCanvas").transform);
    }

    void TryDestroyMe(MarketShip ship)
    {
        if (ship == shipPearlsGetter) Destroy(gameObject);
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

