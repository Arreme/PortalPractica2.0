using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : AbstractHealthSystem
{
    public override void takeDamage()
    {
        AudioManager._instance.PlayAudio((int)Audios.ACIDDEAD);
        GameManager_1._gameManager.DeathEvent();
    }

    public override void takeLaserDamage()
    {
        AudioManager._instance.PlayAudio((int)Audios.BURNT);
        GameManager_1._gameManager.DeathEvent();
    }
}
