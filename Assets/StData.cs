using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StData
{
    private static int _checkpoint;
    public static int Checkpoint
    {
        get { return _checkpoint; }
        set { if (value > _checkpoint) _checkpoint = value; }
    }
}
