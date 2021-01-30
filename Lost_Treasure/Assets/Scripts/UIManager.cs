using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public HealthMeter healthMeter;
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
        healthMeter.UpdateHearts(player.Health);
        healthText.UpdateText("Health: " + player.Health.ToString());
    }
}
