using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Collectible
{
    public override void OnPickup()
    {
        PlayerController.instance.playerUI.IncreaseLife();
    }
}
