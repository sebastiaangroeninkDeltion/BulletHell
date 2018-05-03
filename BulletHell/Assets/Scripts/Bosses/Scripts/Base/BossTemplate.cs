﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*This script is the base template for enemy variables and behaviour*/

public class BossTemplate : MonoBehaviour {

    [Header("Statistics:")]
    public float health = 1000;
    public float attackInterval = 0.5f; //Used for projectile instantiation;
    public float moveInterval = 3f; //Used for randomizing position;
    public float moveSpeed = 1.5f;

    [Header("Trivial Info:")]
    public Vector2 starterPosition; //Stored initial position saved for movement purposes;
    public Vector2 targetLoc; //Location to move towards;
    public float movementRadiusX = 5;
    public float movementRadiusY = 2;

    [Header("Offense Settings:")]
    public List<ProjectileTemplate> projectiles; //Different types of projectile;

    #region Private Variables
    private float attackTimer = 0.5f; //Actual timer;
    private float moveTimer = 0;
    protected bool cooledDown;

    #endregion

    public virtual void Start() {
        starterPosition = transform.position;
        targetLoc = targetLoc = new Vector2(Random.Range(starterPosition.x - movementRadiusX, starterPosition.x + movementRadiusX),
                                     Random.Range(starterPosition.y - movementRadiusY, starterPosition.y + movementRadiusY));
        moveTimer = moveInterval;
        attackTimer = attackInterval;
        
    }

    public virtual void Update() {
        CoolDown(); //Decreasing timer;
        BaseAttack();
        Move();
        DecideNextLocation();
    }

    public void Move() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetLoc.x, targetLoc.y, -3), moveSpeed * Time.deltaTime);
    }

    public void DecideNextLocation() {
        moveTimer -= Time.deltaTime;

        if(moveTimer <= 0)
        {
            moveTimer = moveInterval;
            targetLoc = new Vector2(Random.Range(starterPosition.x - movementRadiusX, starterPosition.x + movementRadiusX),
                                     Random.Range(starterPosition.y - movementRadiusY, starterPosition.y + movementRadiusY));
        }
    }

    //Cooldown check for next initialization of attacking
    public virtual bool CoolDown() {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0) { //Checks if the attack timer hits 0;
            attackTimer = attackInterval; //Resets the timer;
            return true;
        }
        else
            return false; //If timer is not met with the conditions, no action will be performed;
    }


    public virtual void BaseAttack() {
        cooledDown = CoolDown();

    }
}
