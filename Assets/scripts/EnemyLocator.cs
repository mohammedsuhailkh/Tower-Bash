using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour
{

    [SerializeField]Transform weapon;
    [SerializeField]Transform target;
    [SerializeField]float range = 15f;
    [SerializeField]ParticleSystem projectiles;
    
    
    void Update()
    {
        FindClosestTarget();
        AimAtEnemy();
    }
    

    void FindClosestTarget(){
        Enemyscript[] enemies = FindObjectsOfType<Enemyscript>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemyscript enemy in enemies){
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance){
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void AimAtEnemy(){

        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if(targetDistance < range){
            Attack(true);
        }else{
            Attack(false);
        }
    }

    void Attack(bool isActive){
        var emissionModule = projectiles.emission;
        emissionModule.enabled = isActive;
    }
}
