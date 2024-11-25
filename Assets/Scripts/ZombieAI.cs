using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// FSM States for the Zombie
public enum ZombieState { ATTACK, CHASE, MOVING, DEAD, DEFAULT};

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    public float chaseDistance = 20.0f;

    protected ZombieState state = ZombieState.DEFAULT;
    protected Vector3 destination = new Vector3(0, 0, 0);

    Animator animator;

    AudioSource myaudio;

    //Blood Splatter Effect
    ParticleSystem bloodSplatterEffect;
    bool effectStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        myaudio = GetComponent<AudioSource>();
        bloodSplatterEffect = transform.GetComponent<ParticleSystem>();
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ZombieState.DEFAULT:
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
                {
                    state = ZombieState.CHASE;
                }
                else
                {
                    state = ZombieState.MOVING;
                    destination = transform.position + RandomPosition();
                    agent.SetDestination(destination);
                }
                break;
            case ZombieState.MOVING:
                animator.SetBool("isWalking", true);
                //Debug.Log("Dest = " + destination + "Distance = " + Vector3.Distance(transform.position, destination));
                if (Vector3.Distance(transform.position, destination) < 2.0f)
                {
                    state = ZombieState.DEFAULT;
                }

                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance)
                {
                    state = ZombieState.CHASE;
                }
                break;
            case ZombieState.CHASE:
                if (Vector3.Distance(transform.position, player.transform.position) > chaseDistance)
                {
                    state = ZombieState.DEFAULT;
                }
                agent.SetDestination(player.transform.position);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                if (Vector3.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
                {
                    state = ZombieState.ATTACK;
                }
                break;
            case ZombieState.ATTACK:
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                if (Vector3.Distance(transform.position, player.transform.position) > agent.stoppingDistance + 1)
                {
                    state = ZombieState.MOVING;
                    animator.SetBool("isAttacking", false);
                }
                break;
            case ZombieState.DEAD:
                animator.SetBool("isDead", true);
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            // Disable all Renderers and Colliders
            //Renderer[] allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            //foreach (Renderer c in allRenderers) c.enabled = false;
            Collider[] allColliders = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider c in allColliders) c.enabled = false;

            state = ZombieState.DEAD;
            gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;
            StartBloodSplatter();

            StartCoroutine(PlayAndDestroy(3.0f));
        }
    }

    private IEnumerator PlayAndDestroy(float waitTime)
    {
        myaudio.Play();
        yield return new WaitForSeconds(waitTime);
        StopBloodSplatter();
        Destroy(gameObject);
    }

    private void StartBloodSplatter()
    {
        if (effectStarted == false)
        {
            bloodSplatterEffect.Play();
            effectStarted = true;
        }

    }
    private void StopBloodSplatter()
    {
        effectStarted = false;
        bloodSplatterEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

}
