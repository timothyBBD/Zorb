using UnityEngine;
using Pathfinding;
using System.Reflection;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum FireType
    {
        Single,
        Cone,
        Radial
    }
    
    EnemyShooting enemyShooting;
    GameObject player;
    GameObject weapon;
    Animator weaponAnimator;
    AIPath pathfinding;
    bool isFiring = false;

    public float detectionRange = 10f;
    public bool alwaysChase = true;
    public FireType fireType = FireType.Single;
    public float fireRate = 3f;
    float countDownTillNextShot = 0f;

    Vector2 getDirectionToPlayer()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        return playerPosition - enemyPosition;
    }

    bool IsPlayerDetected()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(playerPosition, enemyPosition);
        return distance <= detectionRange;
    }

    void Awake()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        pathfinding = GetComponent<AIPath>();
        weapon = gameObject.transform.GetChild(0).gameObject;
        weaponAnimator = weapon.GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        if (alwaysChase)
        {
            if (!pathfinding.canSearch && IsPlayerDetected())
            {
                pathfinding.canSearch = true;
            }
        }
        else
        {
            pathfinding.canSearch = IsPlayerDetected();
        }
        if(pathfinding.canSearch)
        {
            countDownTillNextShot -= Time.deltaTime;
            if(countDownTillNextShot <= 0 && !isFiring)
            {
                StartCoroutine(AnimateWeaponChargeUpAndFire());
            }
        }
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
