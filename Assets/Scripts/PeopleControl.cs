using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PeopleControl : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Vector2 _activeZoneSize;

    [SerializeField] private Draw _draw = null;

    [SerializeField] private List<Character> _characters = new List<Character>();

    private float _stepX = 0, _stepY = 0;

    private Camera _camera;

    private bool _startGame = false;
    private bool _winGame = false, _gameOver = false;

    private void Start()
    {
        _camera = Camera.main;

        _stepX = _camera.pixelWidth / _activeZoneSize.x;
        _stepY = _camera.pixelHeight / 3 / _activeZoneSize.y;

        _draw.Positions += RecalculateVector;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_startGame && !_gameOver)
        {
            _startGame = true;

            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].Animator.SetTrigger("Run");
            }
        }

        if (_startGame && !_winGame && !_gameOver)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        if (_characters.Count <= 0 && !_gameOver)
        {
            _gameOver = true;
        }
    }

    private void OnDisable()
    {
        _draw.Positions -= Relocate;
    }

    private void Relocate(Vector3[] positions)
    {
        if (!_gameOver && !_winGame)
        {
            if (positions.Length > _characters.Count)
            {
                int step = positions.Length / _characters.Count;

                Debug.Log(step);

                int cont = 0;

                for (int i = 0; i < positions.Length; i++)
                {
                    if (i % step == 0 && cont < _characters.Count)
                    {
                        //_characters[cont].transform.localPosition = positions[i];
                        _characters[cont].StartRelocate(positions[i]);
                        cont++;
                    }
                }

                Debug.Log(cont);
            }

            else
            {
                int onePosCharacter = _characters.Count / positions.Length;

                int cont = 0;

                for (int i = 0; i < positions.Length; i++)
                {
                    //_characters[i].transform.localPosition = positions[i];

                    for (int j = 0; j < onePosCharacter; j++)
                    {
                        //_characters[cont].transform.localPosition = positions[i];
                        _characters[cont].StartRelocate(positions[i]);

                        cont++;
                    }
                }
            }
        }
    }

    private void RecalculateVector(Vector3[] pos)
    {
        Vector3[] position = new Vector3[pos.Length];

        for (int i = 0; i < pos.Length; i++)
        {
            position[i] = new Vector3(pos[i].x / _stepX, 0, pos[i].y / _stepY);
        }

        Relocate(position);
    }

    public void AddCharacter(Character newBoy)
    {
        _characters.Add(newBoy);

        newBoy.Animator.SetTrigger("Run");
    }

    public void RemoveCharacter(Character remove)
    {
        _characters.Remove(remove);
    }

    public void Win()
    {
        _winGame = true;

        for (int i = 0; i < _characters.Count; i++)
        {
            Vector3 winPos = new Vector3(Random.Range(0f, _activeZoneSize.x), 0, Random.Range(0f, _activeZoneSize.y));

            _characters[i].StartRelocate(winPos);

            _characters[i].Animator.SetTrigger("Win");
        }
    }
}