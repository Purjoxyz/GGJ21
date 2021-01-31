using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 cameraPositionOffset = new Vector3(0, 5, -5);
    public float timeToReachPlayer = 1;

    private Player player;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime;
    private bool movingToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void LateUpdate()
    {
        if (!player.IsDead && !movingToPlayer)
        {
            FollowPlayer();
        }
        else if (movingToPlayer || !player.JustDied)
        {
            MoveToPlayer();
        }
    }

    private Vector3 GetPositionWithCameraOffset(Vector3 position)
    {
        Vector3 worldOffset = player.transform.right * cameraPositionOffset.x +
                              player.transform.up * cameraPositionOffset.y +
                              player.transform.forward * cameraPositionOffset.z;
        return position + worldOffset;
    }

    private void FollowPlayer()
    {
        transform.position = GetPositionWithCameraOffset(player.transform.position);
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            player.transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);
    }

    private void MoveToPlayer()
    {
        if (!movingToPlayer)
        {
            startPosition = transform.position;
            targetPosition = GetPositionWithCameraOffset(player.respawnPoint);
            movingToPlayer = true;
        }

        if (timeToReachPlayer == 0)
        {
            transform.position = targetPosition;
            return;
        }

        elapsedTime += Time.deltaTime;

        float ratio = elapsedTime / timeToReachPlayer;
        if (ratio < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, ratio);
        }
        else
        {
            transform.position = targetPosition;
            elapsedTime = 0;
            movingToPlayer = false;
        }
    }
}
