using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerWeapon{

    public enum Weapon {
        Glock,
        ObstacleCrate
    }

    public Weapon currentWeapon;

    public int damage = 10;
    public float range = 100;

    public int obstacleNumberLimit = 3;
}
