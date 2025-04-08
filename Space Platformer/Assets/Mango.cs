using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mango : Collectible
{
    public int scoreIncrease = 1;

    public override void OnPickup()
    {
        base.OnPickup();
        PlayerController.instance.playerUI.UpdateScore(scoreIncrease);
    }
}
