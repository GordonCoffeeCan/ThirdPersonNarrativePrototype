using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerWeapon{

    public enum Weapon {
        none,
        freezer,
        obstacleCreator,
        spring
    }

    public Weapon currentWeapon;

    public int damage = 10;
    public float range = 100;

    public int obstacleNumberLimit = 3;

    public float coolDownTime = 0;

    public float SetupCoolDownTime(Weapon _weapon) {
        if(currentWeapon == Weapon.freezer) {
            coolDownTime = 0.5f;
        } else if (currentWeapon == Weapon.obstacleCreator) {
            coolDownTime = 1;
        } else {
            coolDownTime = 0;
        }

        return coolDownTime;
    }
}
