using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 15;

    private PeopleControl _peopleControl = null;

    private Vector3 _target;

    private bool _relocate = false;

    private Animator _animator = null;

    public Animator Animator
    {
        get => _animator = _animator ?? GetComponent<Animator>();
    }

    private void Start()
    {
        _peopleControl = GetComponentInParent<PeopleControl>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_relocate)
        {
            transform.Translate(Direction() * _speed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, _target) < 0.1f)
            {
                _relocate = false;
            }
        }
    }

    private Vector3 Direction()
    {
        Vector3 direction = _target - transform.localPosition;
        direction.Normalize();

        return direction;
    }

    public void StartRelocate(Vector3 target)
    {
        _target = target;

        _relocate = true;
    }

    public void RemoveMe()
    {
        _peopleControl.RemoveCharacter(this);
        
        Destroy(this.gameObject, 3f);
    }
}