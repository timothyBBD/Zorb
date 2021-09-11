using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CrabAI : EnemyAI
{
    Animator enemyAnimator;
    float distanceToPlayerBeforeFire = 3f;

    protected override void Awake()
    {
        base.Awake();
        enemyAnimator = GetComponent<Animator>();
    }

    protected override void AttackPlayer()
    {
        if (isCloseEnoughToPlayer())
        {
            StartCoroutine(AnimateWeaponChargeUpAndFire());
        }
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

    bool isCloseEnoughToPlayer()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        return Vector2.Distance(playerPos, enemyPos) <= distanceToPlayerBeforeFire;
    }
}
