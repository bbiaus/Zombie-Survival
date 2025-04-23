using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] zombieSounds;
    [SerializeField] private float soundInterval = 3f;
    [SerializeField] private float hearingDistance = 15f;
    private float nextSoundTime = 0f;
    public AudioClip idleSound;
    public AudioClip chaseSound;
    public AudioClip attackSound;
    private string currentState = "";
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 1.5f;
    private bool isAttacking = false;
    private bool isDead = false;
    public Life playerHealth;

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
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isDead)
        {
            agent.SetDestination(player.position);

            // Si está dentro del rango de ataque, iniciar ataque
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                animator.SetBool("isAttacking", true);
                animator.SetBool("isChasing", false);
                PlaySound(attackSound, "attack");
                StartCoroutine(Attack());
            }
            else
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("isChasing", true);
                PlaySound(chaseSound, "chase");
            }

            if (distanceToPlayer <= hearingDistance && Time.time >= nextSoundTime)
            {
                PlayRandomZombieSound();
                nextSoundTime = Time.time + soundInterval + Random.Range(0f, 2f);
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10f);
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }


    void PlayRandomZombieSound()
    {
        if (zombieSounds.Length == 0) return;

        int index = Random.Range(0, zombieSounds.Length);
        audioSource.clip = zombieSounds[index];
        audioSource.Play();
    }

    public void Die()
    {
        GameManager.Instance.ZombieKilled();

        animator.SetBool("isDead", true);
        GetComponent<NavMeshAgent>().enabled = false; // Para que deje de moverse
        isDead = true;
        // Detenemos todas las animaciones después de un breve delay (para dejarla arrancar)
        StartCoroutine(DisableAnimatorAfterDelay());

        Destroy(gameObject, 2f);
    }

    IEnumerator DisableAnimatorAfterDelay()
    {
        yield return new WaitForSeconds(1.15f); // Tiempo suficiente para que arranque la animación de muerte
        animator.enabled = false;
    }
}
