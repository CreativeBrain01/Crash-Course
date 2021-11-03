using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMarker : MonoBehaviour
{
    [SerializeField]
    string targetTag;
    GameObject target;
    float targetHeight;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        Rect playerSprite = target.GetComponent<SpriteRenderer>().sprite.rect;
        targetHeight = playerSprite.height * target.transform.localScale.y / 10;
    }

    void Update()
    {
        if (targetTag != "Player")
        {
            target = GameObject.FindGameObjectWithTag(targetTag);
            Rect targetSprite = target.GetComponent<SpriteRenderer>().sprite.rect;
            targetHeight = targetSprite.height * target.transform.localScale.y / 10;
        }

        Vector3 playerPos = target.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y + targetHeight/10, playerPos.z);
    }
}
