using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float speed = 4f;
    public float attackSpeed = 6f;
    public int damage = 20;
    bool touchPlayer;
    private bool canAttack = true;
    private float lastAttackTime;
    Rigidbody rb;
    public static int health;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (touchPlayer)
        {
            if (touchPlayer && canAttack)
            {
                DealDamage();
                lastAttackTime = Time.time;
                canAttack = false;
            }
        }
        else if (!touchPlayer && !canAttack)
        {
            canAttack = true;
        }
        else
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            rb.MovePosition(pos);
        }
        transform.LookAt(player);
    }
    private void Update()
    {
        if (!canAttack && Time.time - lastAttackTime > attackSpeed)
        {
            canAttack = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchPlayer = false;
        }
    }

    void DealDamage()
    {
        FPCamera.CurrentHealth -= damage;
    }

    private void Die()
    {
        
    }
}
