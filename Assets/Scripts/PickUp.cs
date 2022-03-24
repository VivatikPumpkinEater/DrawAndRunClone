using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle = null;
    
    private CapsuleCollider _capsuleCollider = null;
    private SkinnedMeshRenderer _meshRenderer = null;

    private bool _pick = false;

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider col)
    {
        var player = col.GetComponent<Character>();
        
        if(player && !_pick)
        {
            _pick = true;
            
            _capsuleCollider.isTrigger = false;

            _meshRenderer.material = player.GetComponentInChildren<SkinnedMeshRenderer>().material;

            gameObject.AddComponent<Character>();
            
            transform.SetParent(player.transform.parent);

            GetComponentInParent<PeopleControl>().AddCharacter(gameObject.GetComponent<Character>());

            var effect = Instantiate(_particle);
            effect.transform.position = transform.position;
            effect.Play();
            
            Destroy(effect, 2f);
            
            Destroy(this);
        }
    }
}
