using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : AbstractHealthSystem
{
    public override void takeDamage()
    {
        GameManager_1._gameManager.DeathEvent();
    }
}
