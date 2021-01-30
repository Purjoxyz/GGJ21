﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIText healthText;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthText.UpdateText("Health: " + player.Health.ToString());
    }
}
