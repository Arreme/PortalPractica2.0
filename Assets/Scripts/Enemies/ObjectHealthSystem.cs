using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealthSystem : AbstractHealthSystem
{
    public override void takeDamage()
    {
        AudioManager._instance.PlayAudio((int)Audios.OBJBREAK);
        Destroy(gameObject);
    }

    public override void takeLaserDamage()
    {

    }
}
