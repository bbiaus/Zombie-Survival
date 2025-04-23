using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Civilian : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isFollowing = false;
    private AudioSource audioSource;

    private float deathTimer = 60f; // Tiempo límite para salvarlo

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        StartCoroutine(DeathCountdown());
    }

    private void Update()
    {
        if (isFollowing)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFollowing)
        {
            isFollowing = true;
            agent.speed = 3.5f;
            animator.SetBool("isFollowing", true);
            audioSource.Stop();
        }
        else if (other.CompareTag("Shelter") && isFollowing)
        {
            GameManager.Instance.CivilianRescued();
            Destroy(gameObject);
        }
    }

    private IEnumerator DeathCountdown()
    {
        yield return new WaitForSeconds(deathTimer);

        if (!isFollowing)
        {
            Destroy(gameObject);
        }
    }
}
