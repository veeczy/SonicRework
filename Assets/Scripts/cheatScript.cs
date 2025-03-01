using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheatScript : MonoBehaviour
{
    // variables
    public bool cheat;

    private void Update()
    {
        cheat = CheatOn();
    }

    // for when working on invincibility cheat
    public bool CheatOn()
    {
        cheat = true;
        return cheat;
    }
}
