

using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator {

    List<Color> colors;

    public ColorGenerator(List<Color> colors)
    {
        this.colors = colors;
    }

    public void AddThisColors(List<Color> colors) 
    {
        this.colors.AddRange(colors);
    }

    public void ClearThisColor(Color color) 
    {
        colors.RemoveAll(c => c == color);
    }

    public List<Color> GetThisNumberOfRandomColors(int number)
    {   
        List<Color> randomColors = new List<Color>();
        for(int i = 0; i < number; i++)
        {
            randomColors.Add(GetRandomColor());
        }
        return randomColors;        
    }

    Color GetRandomColor() => 
        colors[GetRandom<Color>(colors)];

    int GetRandom<T>(List<T> list)=> 
        Random.Range(0, list.Count);
}
