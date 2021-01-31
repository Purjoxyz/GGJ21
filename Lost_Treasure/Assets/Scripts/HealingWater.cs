using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWater : MonoBehaviour
{
    public bool isRespawnPoint = true;
    public Vector3 respawnPointOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                if (isRespawnPoint)
                {
                    player.HealToFull(transform.position + respawnPointOffset);
                }
                else
                {
                    player.HealToFull();
                }

                SFXPlayer.Instance.Play(Sound.Heal, volumeFactor: 0.2f);
            }
        }
    }

}
