using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollowPlayer : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(player.position.x,
        transform.position.y, player.position.z);

        transform.position = newPosition;

    }
}
