using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Obstacles
{

    protected override void Power(Character character)
    {
        var effect = Instantiate(_effect);
        effect.transform.position = character.transform.position;
        effect.Play();
        
        Destroy(effect, 2f);
        
        Debug.Log("Saw");

        var rbCharacter = character.GetComponent<Rigidbody>();

        rbCharacter.isKinematic = false;
        
        rbCharacter.AddForce((Vector3.up + Vector3.back) * 5f, ForceMode.Impulse);

        character.transform.parent = null;
        
        character.RemoveMe();
    }

    private void Update()
    {
        Rotated();
    }

    protected override void Rotated()
    {
        base.Rotated();
    }
}
