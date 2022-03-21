using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Animations;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    [Range(-100, 100)][SerializeField] float missileSpeed = 18f;
    [Range(0, 10)] [SerializeField] float timeBeforeDissapearing = 5f;

    float direction;
    Animater animater;
    Rigidbody2D rb;

    public void SetDirection(float value)
    {
        direction = Mathf.Sign(value);
        animater.CheckIfFlip(direction);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animater = GetComponent<Animater>();
        Invoke("Dissapear", timeBeforeDissapearing);
    }

    private void Update() { rb.velocity = Vector2.right * direction * missileSpeed; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { return; }

        Destroy(gameObject);
    }

    private void Dissapear() { Destroy(gameObject); }
}
