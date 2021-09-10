using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CrabAI : EnemyAI
{
    Animator enemyAnimator;

    protected override void Awake()
    {
        base.Awake();
        enemyAnimator = GetComponent<Animator>();
    }

    protected override void attackPlayer()
    {
        StartCoroutine(AnimateWeaponChargeUpAndFire());
    }

    IEnumerator AnimateWeaponChargeUpAndFire()
    {
        isFiring = true;
        enemyAnimator.SetBool("IsShooting", true);
        yield return new WaitForSeconds(0.35f);
        enemyAnimator.SetBool("IsShooting", false);
        Fire();
    }

    void Fire()
    {
        isFiring = false;
        countDownTillNextShot = fireRate;
        string fireTypeName = fireType.ToString() + "Fire";
        MethodInfo fireMethod = typeof(EnemyShooting).GetMethod(fireTypeName);
        fireMethod.Invoke(enemyShooting, new object[] { getDirectionToPlayer() });
    }
}
