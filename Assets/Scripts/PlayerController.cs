using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;

    public bool gameOver;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip[] jumpSounds;
    public AudioClip[] crashSounds;

    private Rigidbody _rigidbody; // Declaramos la variable vacía
    private bool isOnTheGround = true;

    private Animator _animator; // Declaramos la variable vacía
    private AudioSource _audioSource; // Declaramos la variable vacía
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Asignamos la referencia
        _animator = GetComponent<Animator>(); // Asignamos la referencia
        _audioSource = GetComponent<AudioSource>(); // Asignamos la referencia
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !gameOver)
        {
            Jump();
        }
    }
    
    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            Destroy(otherCollider.gameObject);
            GameOver();
        } 
        else if (otherCollider.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            dirtParticle.Play();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        
        _animator.SetBool("Death_b", true);
        _animator.SetInteger("DeathType_int", Random.Range(1, 3));
        
        explosionParticle.Play();
        dirtParticle.Stop();
        
        ChooseRandomSFX(crashSounds);
    }

    private void Jump()
    {
        isOnTheGround = false; // Dejo de tocar el suelo
        
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        // Llamamos al trigger para que se dé la transición de la animación de correr a salto
        _animator.SetTrigger("Jump_trig");
        
        dirtParticle.Stop();
        
        ChooseRandomSFX(jumpSounds);
    }

    private void ChooseRandomSFX(AudioClip[] sounds)
    {
        int randomIdx = Random.Range(0, sounds.Length);
        _audioSource.PlayOneShot(sounds[randomIdx], 1);
    }
}
