using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    private float nextSoundTime = 0f;
    private string currentState = "";
    private bool isAttacking = false;
    private bool isDead = false;
    private Life playerHealth;
    public ZombieHealth zombieHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameObject playerLife = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerLife != null)
        {
            playerHealth = playerLife.GetComponent<Life>();
            //Debug.Log("Referencia a Life del player asignada correctamente.");
        }

        // Aplica stats desde ZombieData
        if (zombieData != null)
        {
            if (agent != null) agent.speed = zombieData.speed;

            if (zombieHealth != null)
            {
                zombieHealth.SetHealth(zombieData.maxHealth);
            }
        }
    }

    private void PlaySound(AudioClip clip, string state)
    {
        if (currentState == state) return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        currentState = state;
    }

    private void Update()
    {
        if (player == null || zombieData == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isDead)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= zombieData.attackRange && !isAttacking)
            {
                animator.SetBool("isAttacking", true);
                animator.SetBool("isChasing", false);
                PlaySound(zombieData.attackSound, "attack");
                StartCoroutine(Attack());
            }
            else
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("isChasing", true);
                PlaySound(zombieData.chaseSound, "chase");
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        if (playerHealth != null && zombieData != null)
        {
            playerHealth.TakeDamage(zombieData.damage);
            
            if (zombieData.attackSound != null)
            audioSource.PlayOneShot(zombieData.attackSound);
        }

        yield return new WaitForSeconds(zombieData.attackCooldown);
        isAttacking = false;
    }

    void PlayRandomZombieSound()
    {
        if (zombieData.randomSounds == null || zombieData.randomSounds.Length == 0) return;

        int index = Random.Range(0, zombieData.randomSounds.Length);
        audioSource.clip = zombieData.randomSounds[index];
        audioSource.Play();
    }

    public void Die()
    {
        GameManager.Instance.ZombieKilled();

        animator.SetBool("isDead", true);
        GetComponent<NavMeshAgent>().enabled = false;
        isDead = true;
        StartCoroutine(DisableAnimatorAfterDelay());

        Destroy(gameObject, 2f);
    }

    IEnumerator DisableAnimatorAfterDelay()
    {
        yield return new WaitForSeconds(1.15f);
        animator.enabled = false;
    }
}
