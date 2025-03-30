using UnityEngine;
using UnityEngine.AI;

public class CharacterFollower : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private bool following = false;
    [SerializeField] int keepDistance = 2; // Distancia a la que frena el NPC del jugador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            following = true;
        }
    }

    void Update()
    {
        if (following && player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > keepDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath(); // Detiene el NPC si está muy cerca
            }
        }
    }
}

