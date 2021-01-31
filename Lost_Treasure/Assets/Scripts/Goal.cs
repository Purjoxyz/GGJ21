using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if (player != null)
                player.WinGame();
        }
    }
}
