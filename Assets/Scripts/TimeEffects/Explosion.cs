using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float explosionRadius;
    private float explosionForce;
    private int damage;

    public float duration = 0.3f;
    private float elapsed = 0f;
    public float startSize = 1f;
    private float endSize;

    private void Update()
    {
        if (TimeController.Instance.IsStoppedTime) return;

        elapsed += Time.deltaTime;
        float scale = Mathf.Lerp(startSize, endSize, Mathf.Clamp01(elapsed / duration));
        transform.localScale = new Vector3(scale, scale, scale);

        if (elapsed >= duration)
        {
            ApplyExplosionEffects();
            Destroy(gameObject);
        }
    }
    public void Initialize(float radius, float force, int dmg)
    {
        explosionRadius = radius;
        explosionForce = force;
        damage = dmg;
        endSize = explosionRadius * 2;
    }

   
    private void ApplyExplosionEffects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            BasicEnemy enemy = hit.GetComponent<BasicEnemy>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }

            PlayerCharacter player = hit.GetComponent<PlayerCharacter>();
            if (player)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
