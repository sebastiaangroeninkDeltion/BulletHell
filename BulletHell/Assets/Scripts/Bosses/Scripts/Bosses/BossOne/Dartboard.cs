﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dartboard : BossTemplate {

    [Header("Big Dart Settings:")]
    public float bigdartTimerInterval = 20;

    [Header("Small Dart Settings:")]
    public float minVelocityDarts = 5;
    public float maxVelocityDarts = 9;

    [Header("Angle Settings:")]
    public float angleAdjustment = 15;

    #region Private Variables
    private float bigdartTimer = 20;
    private float nextAngle = 0;
    private bool canshootBigdart;
    #endregion

    public void Awake()
    {
        bigdartTimer = bigdartTimerInterval;
    }

    public override void Update()
    {
        base.Update();
        bigdartTimer -= Time.deltaTime;
    }

    public override void BaseAttack() {
        base.BaseAttack();

        if (cooledDown) {
            GameObject projectile = Instantiate(projectiles[0].gameObject, transform.position, Quaternion.identity);    
            projectile.GetComponent<ProjectileTemplate>().forwardAxis = nextAngle;
            projectile.GetComponent<ProjectileTemplate>().movementSpeed = Random.Range(minVelocityDarts, maxVelocityDarts);
            nextAngle += angleAdjustment;
        }

        canshootBigdart = CanShootBigDart();

        if (canshootBigdart) {
            GameObject bigProjectile = Instantiate(projectiles[1].gameObject, transform.position, Quaternion.identity);
            bigProjectile.GetComponent<HeatSeekingProjectile>().forwardAxis = nextAngle;
            bigProjectile.GetComponent<HeatSeekingProjectile>().target = GameObject.FindWithTag("Player").transform;
            nextAngle += angleAdjustment;
        }
    }

    private bool CanShootBigDart() {


        if(bigdartTimer <= 0) 
        {
            bigdartTimer = bigdartTimerInterval;
            return true;
        }

        return false;
    }
}
