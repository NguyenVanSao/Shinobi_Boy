using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 playerPos;
    [SerializeField] float y;
    [SerializeField] float offset;
    [SerializeField] float offsetSmoothing;


    // Update is called once per frame
    void Update()
    {
        if (player.localScale.x > 0f)
        {
            playerPos = new Vector3(player.transform.position.x + offset, player.transform.position.y, this.transform.position.z);
        }
        else
        {
            playerPos = new Vector3(player.transform.position.x - offset, player.transform.position.y, this.transform.position.z);
        }

        this.transform.position = Vector3.Lerp(transform.position, playerPos + new Vector3(0, y, 0), offsetSmoothing * Time.deltaTime);
    }
}
