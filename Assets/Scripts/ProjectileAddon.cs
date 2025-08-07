using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    [SerializeField] private RewindableDestroy _destroyableObject;
    private RewindableObjectPoolManager _rewindableObjectPoolManager;
    public int damage;
    private bool targetHit;
    private bool byPlayer = false;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rewindableObjectPoolManager = FindFirstObjectByType<RewindableObjectPoolManager>();
    }
    public void Casted()
    {
        byPlayer = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ProjectileAddon>()) return;

        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
        transform.localScale = new Vector3(5, 5, 5);

        _rewindableObjectPoolManager.DestroyObject(_destroyableObject);


        if (other.gameObject.GetComponent<BasicEnemy>())
        {
            if (targetHit)
                return;
            else
                targetHit = true;
            BasicEnemy enemy = other.gameObject.GetComponent<BasicEnemy>();

            enemy.TakeDamage(damage);

            return;
        }
        if (other.gameObject.GetComponent<PlayerCharacter>())
        {
            if (targetHit)
                return;
            else
                targetHit = true;
            PlayerCharacter enemy = other.gameObject.GetComponent<PlayerCharacter>();

            enemy.TakeDamage(damage);

            return;
        }

    }
}