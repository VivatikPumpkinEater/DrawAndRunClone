using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Obstacles
{
    protected override void Power(Character character)
    {
        Collider[] characters = Physics.OverlapSphere(transform.position, 2f);

        for (int i = 0; i < characters.Length; i++)
        {
            var rb = characters[i].attachedRigidbody;

            if (rb && rb.GetComponent<Character>())
            {
                rb.isKinematic = false;

                rb.AddExplosionForce(500, transform.position, 2f);

                rb.gameObject.transform.parent = null;
                
                rb.GetComponent<Character>().RemoveMe();
            }
        }

        var effect = Instantiate(_effect);
        effect.transform.position = transform.position;
        effect.Play();
        
        Destroy(this.gameObject);
    }
}