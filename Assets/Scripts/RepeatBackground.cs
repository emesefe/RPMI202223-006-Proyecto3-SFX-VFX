using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; // Posición inicial del fondo
    private float repeatWidth; // Anchura a la que debe llevarse a cabo el "teletransporte" para simular fondo infinito
    
    private void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2f; // Obtenemos la anchura a partir del Collider
    }
    
    private void Update(){
        if (transform.position.x < startPos.x - repeatWidth) // Si hemos recorrido la mitad del fondo, reiniciamos su posición
        {
            transform.position = startPos;
        }
    }
}
