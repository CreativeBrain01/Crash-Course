using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    GameObject player;
    float playerHeight;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rect playerSprite = player.GetComponent<SpriteRenderer>().sprite.rect;
        playerHeight = playerSprite.height * player.transform.localScale.y/10;
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y + playerHeight/10, playerPos.z);
    }
}
