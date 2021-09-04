using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyShooting enemyShooting;

    void Awake()
    {
        enemyShooting = GetComponent<EnemyShooting>();    
    }
}
