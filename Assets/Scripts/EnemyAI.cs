using UnityEngine;
using Pathfinding;
using System.Reflection;
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
    AIPath pathfinding;
    float timeSinceLastShot = 0f;

    public float detectionRange = 10f;
    public bool alwaysChase = true;
    public FireType fireType = FireType.Single;
    public float fireRate = 3f;

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
            timeSinceLastShot += Time.deltaTime;
            if(timeSinceLastShot >= fireRate)
            {
                Fire();
                timeSinceLastShot = 0f;
            }
        }
    }

    void Fire()
    {
        string fireTypeName = fireType.ToString() + "Fire";
        MethodInfo fireMethod = typeof(EnemyShooting).GetMethod(fireTypeName);
        fireMethod.Invoke(enemyShooting, new object[] { getDirectionToPlayer() });
    }


}
