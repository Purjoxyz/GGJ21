using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject wall = other.gameObject;
            //player.CollideWithWall(wall);
        }
    }
}
