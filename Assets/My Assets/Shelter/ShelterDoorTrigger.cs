using UnityEngine;

public class ShelterDoorTrigger : MonoBehaviour
{
    public Animator shelterDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shelterDoor.SetBool("Abrir", true);
        }

        if (other.CompareTag("Follower"))
        {
            GameManager.Instance.RescuedCharacter();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shelterDoor.SetBool("Abrir", false);
        }
    }
}
