using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerWeapon{

    public enum Weapon {
        Freezer,
        ObstacleCreator
    }

    public Weapon currentWeapon;

    public int damage = 10;
    public float range = 100;

    public int obstacleNumberLimit = 3;

    public float coolDownTime = 0;

    public float SetupCoolDownTime(Weapon _weapon) {
        if(currentWeapon == Weapon.Freezer) {
            coolDownTime = 0.5f;
        } else if (currentWeapon == Weapon.ObstacleCreator) {
            coolDownTime = 1;
        }

        return coolDownTime;
    }
}
