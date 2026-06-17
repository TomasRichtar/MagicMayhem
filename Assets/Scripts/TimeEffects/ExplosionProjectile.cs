using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : MonoBehaviour
{
    [SerializeField] private Explosion _explosionPrefab;

    public int damage = 50;
    public float explosionRadius = 5f;
    public float explosionForce = 500f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TimeController.Instance.IsStoppedTime == true) return;

        _rb.linearVelocity = Vector3.zero;
        _rb.useGravity = false;

        Explode();

        Destroy(gameObject);
    }

    private void Explode()
    {
        if (_explosionPrefab != null)
        {
            Explosion explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explosion.Initialize(explosionRadius, explosionForce, damage);
        }
        else
        {
            Debug.LogError("Explosion prefab nen� nastaven!");
        }
    }
}
