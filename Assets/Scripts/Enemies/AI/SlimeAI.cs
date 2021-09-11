using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SlimeAI : EnemyAI
{
    GameObject weapon;
    Animator weaponAnimator;

    protected override void Awake()
    {
        base.Awake();
        weapon = gameObject.transform.GetChild(0).gameObject;
        weaponAnimator = weapon.GetComponent<Animator>();
    }

    protected override void AttackPlayer()
    {
        StartCoroutine(AnimateWeaponChargeUpAndFire());
    }


    IEnumerator AnimateWeaponChargeUpAndFire()
    {
        isFiring = true;
        weaponAnimator.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.35f);
        weaponAnimator.SetBool("isShooting", false);
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
