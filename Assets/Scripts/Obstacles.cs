using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] protected ParticleSystem _effect = null;
    
    protected virtual void OnTriggerEnter(Collider col)
    {
        var player = col.GetComponent<Character>();

        if (player)
        {
            Power(player);
        }
    }

    protected virtual void Movement()
    {
        transform.position = new Vector3(Mathf.PingPong(1f, 3f), 0,0);
    }

    protected virtual void Rotated()
    {
        transform.Rotate(new Vector3(0,0, 200 * Time.deltaTime));
    }

    protected virtual void Power(Character character)
    {
        
    }
}
