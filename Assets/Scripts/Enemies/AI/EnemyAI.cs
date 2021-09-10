using UnityEngine;
using Pathfinding;
using System.Reflection;
using System.Collections;

public abstract class EnemyAI : MonoBehaviour
{
    public enum FireType
    {
        Single,
        Cone,
        Radial
    }
    
    GameObject player;
    AIPath pathfinding;

    protected EnemyShooting enemyShooting;
    protected bool isFiring = false;
    protected float countDownTillNextShot = 0f;

    public float detectionRange = 10f;
    public bool alwaysChase = true;
    public FireType fireType = FireType.Single;
    public float fireRate = 3f;

    protected Vector2 getDirectionToPlayer()
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

    protected virtual void Awake()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        pathfinding = GetComponent<AIPath>();
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
                attackPlayer();
            }
        }
    }

    protected abstract void attackPlayer();


}
