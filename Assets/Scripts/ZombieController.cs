using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ZombieController : MonoBehaviour {

    [SerializeField] private float MAX_HEALTH = 100f;
    [SerializeField] private float DAMAMGE = 10f;
    [SerializeField] private float ATTACK_COOLDOWN = 1f;
    [SerializeField] private float ATTACK_RANGE = 1f;

    public ZombieMovement ZombieMovement;

    [SerializeField] private float VisionRange = 10f;

    private float _currentHealth;
    private GameObject _target;
    private bool _canAttack = true;




    // Use this for initialization
    void Start () {

        InvokeRepeating("FindPlayer", 0.0f, Random.Range(0.4f, 0.5f));
        _currentHealth = MAX_HEALTH;
    }
	

    public void TakeDamage(float amount)
    {
        Debug.Log(amount);
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            if (_currentHealth <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(ATTACK_COOLDOWN);
        _canAttack = true;
    }

    void Attack()
    {
        _target.gameObject.GetComponent<PlayerController>().TakeDamage(DAMAMGE);
    }

    void FindPlayer()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position,
                VisionRange, LayerMask.GetMask("Player") + LayerMask.GetMask("Default"));
            Debug.DrawRay(transform.position, _target.transform.position - transform.position);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    _target = hit.collider.gameObject;
                    ZombieMovement.MoveToPlayer(_target);
                    if (hit.distance < ATTACK_RANGE && _canAttack)
                    {
                        _canAttack = false;
                        Attack();
                        StartCoroutine(AttackCooldown());
                    }
                }
                else
                {
                    ZombieMovement.StopMoving();
                }
            }

        }
    }
}
