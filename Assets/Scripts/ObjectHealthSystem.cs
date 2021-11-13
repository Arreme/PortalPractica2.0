using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealthSystem : AbstractHealthSystem
{
    public override void takeDamage()
    {
        Destroy(gameObject);
    }
}
