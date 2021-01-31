using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public HealthMeter healthMeter;
    public GameObject instructions;
    public GameObject win;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
    }

    public void UpdateHealth()
    {
        healthMeter.UpdateHearts(player.Health);
    }

    public void ShowWinScreen()
    {
        Debug.Log("You win!");
        win.gameObject.SetActive(true);
    }
}
