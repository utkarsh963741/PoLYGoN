using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public class EnemyController : MonoBehaviour
{
    public float lookRadius =10f;
    Transform target;
    NavMeshAgent agent;
    GameObject player;

    [SerializeField]
    public float damage=10f;

    public float attactSpeed = 1f;
    private float attackCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance<=lookRadius)
        {
            attackCooldown-=Time.deltaTime;

            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance )
            { 
                FaceTarget();
                AttackTarget(player,damage);
            }

            
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
    }

    void AttackTarget(GameObject player,float damage)
    {
        if(attackCooldown<=0)
        {
            CharacterStats playerStat = (CharacterStats) player.GetComponent(typeof(CharacterStats));
            playerStat.TakeDamage(damage);

            attackCooldown=1/attactSpeed;
        }
        
    }

    void OnDrawGizmosSelected() 
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
        
    
}
