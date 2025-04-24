using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Civilian : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private bool isFollowing = false;
    private AudioSource audioSource;
    private Animator animator;
    private bool isIdle = true;

    private float deathTimer = 60f; // Tiempo límite para salvarlo

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DeathCountdown());
    }

    private void Update()
    {
        if (isFollowing)
        {
            agent.SetDestination(player.position);
            // Actualiza el parámetro de velocidad
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);

            if (speed < 0.1f && !isIdle)
            {
                // Randomiza una de las dos animaciones cuando estan quietos
                int idleChoice = Random.Range(0, 2); // 0 o 1
                animator.SetInteger("idleIndex", idleChoice);
                isIdle = true;
            }
            else if (speed >= 0.1f)
            {
                isIdle = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFollowing)
        {
            isFollowing = true;
            audioSource.Stop(); // Detenemos el grito de ayuda
            animator.SetBool("isFollowing", true); // Animación de correr
            agent.speed = 5.5f;
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
