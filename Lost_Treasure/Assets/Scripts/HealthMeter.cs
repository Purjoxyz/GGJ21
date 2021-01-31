using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    public List<Image> hearts;

    public void UpdateHearts(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = health >= i + 1;
        }
    }
}
