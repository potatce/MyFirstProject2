using System;
using UnityEngine;
using UnityEngine.UI;

public class AppleCountController : MonoBehaviour
{
    private AppleController _appleController; 
    public Image[] apples;
    public int appleCount;
    
    void Update()
    {

        for (int i = 0; i < apples.Length; i++)
        {
            if (i < appleCount)
            {
                apples[i].color = new Color(1, 1, 1, 1); 
            }
            else
            {
                apples[i].color = new Color(1, 1, 1, 0.2f);
            }
        }
    }
}