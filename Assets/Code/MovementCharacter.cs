using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class MovementCharacter : MonoBehaviour
{
    public int velocity = 0;
    public GameObject gameObject = null;
    [SerializeField]private bool test = false;
    public Transform transform = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si activo este codigo mi robot avanza y se cae.
        //transform.position = transform.position + new Vector3(0, 0, velocity * Time.deltaTime);
    }
}
