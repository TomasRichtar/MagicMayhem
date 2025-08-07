using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float explosionRadius;
    private float explosionForce;
    private int damage;

    public void Initialize(float radius, float force, int dmg)
    {
        explosionRadius = radius;
        explosionForce = force;
        damage = dmg;
        StartCoroutine(ExpandAndExplode());
    }

    private IEnumerator ExpandAndExplode()
    {
        float duration = 0.3f;
        float startSize = 1f;
        float endSize = explosionRadius * 2;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float scale = Mathf.Lerp(startSize, endSize, elapsed / duration);
            transform.localScale = new Vector3(scale, scale, scale);
            elapsed += Time.deltaTime;
            yield return null;
        }

        ApplyExplosionEffects();
        Destroy(gameObject);
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
