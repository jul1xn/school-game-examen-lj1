using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : Collectible
{
    public int partIndex;
    Sprite partSprite;

    private void Start()
    {
        partSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public override void OnPickup()
    {
        Debug.Log("part_" + partIndex);
        PlayerPrefs.SetInt("part_" + partIndex, 1);
        PlayerController.instance.playerUI.ShowPartPopup(partSprite);
    }
}
