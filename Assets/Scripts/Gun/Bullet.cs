using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject explosion;
    [SerializeField] LayerMask whatIsEnemy;

    [SerializeField, Range(0f, 1f)] float bounciness;
    [SerializeField] bool useGravity;

    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRange;

    [SerializeField] int maxCollisions;
    [SerializeField] float maxLifetime;
    [SerializeField] bool explodeOnCollision = true;

    int collisions;
    PhysicMaterial physic_mat;

    

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisions > maxCollisions)
            Explode();

        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0)
            Explode();
    }

    private void Setup()
    {
        physic_mat = new PhysicMaterial();
        physic_mat.bounciness = bounciness;
        physic_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physic_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physic_mat;

        rb.useGravity = useGravity;
    }

    private void Explode()
    {
        if(explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);
        for(int i = 0; i < enemies.Length; i++)
        {
            //Add an enemy script for this line of code

            //enemies[i].GetComponent<Enemy>().TakeDamage(explosionDamage);
        }

        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enviroment"))
            Destroy(gameObject);

        collisions++;

        if(collision.collider.CompareTag("Enemy") && explodeOnCollision)
            Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
