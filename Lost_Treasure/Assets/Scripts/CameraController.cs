using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 cameraPositionOffset = new Vector3(0, 5, -5);

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 worldOffset = player.transform.right * cameraPositionOffset.x +
                              player.transform.up * cameraPositionOffset.y +
                              player.transform.forward * cameraPositionOffset.z;
        transform.position = player.transform.position + worldOffset;

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            player.transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);
    }
}
