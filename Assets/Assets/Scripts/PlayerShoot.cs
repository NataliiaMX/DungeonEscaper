using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform emitter;

    private void OnFire(InputValue value) //works
    {
        Instantiate(bullet, emitter.transform.position, transform.rotation);
    }
}
