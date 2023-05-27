

using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator {

    List<Color> colors;

    public void AddThisColors(List<Color> colors)=> colors.AddRange(colors);   
    public void ClearThisColor(Color color)=> colors.RemoveAll(c => c == color);
    public List<Color> GetThisNumberOfRandomColors(int number)
    {   
        List<Color> randomColors = new List<Color>();
        for(int i = 0; i < Mathf.Min(colors.Count,number); i++)
        {
            randomColors.Add(GetRandomColor());
        }
        return randomColors;        
    }
    Color GetRandomColor()
    {
        var color = colors[GetRandomIndex<Color>(colors)];
        colors.Remove(color);
        return color;
    }

    int GetRandomIndex<T>(List<T> list)=> Random.Range(0, list.Count);
}
