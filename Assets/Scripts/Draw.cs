using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] private GameObject _brushPrefab = null;

    private GameObject _brush = null;

    private Vector3[] _positions;

    private List<GameObject> _trails = new List<GameObject>();

    public System.Action<Vector3[]> Positions;

    private void Start()
    {
        Camera camera = Camera.main;
        
        Debug.Log(camera.pixelWidth + ";" + camera.pixelHeight);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(_brush);

            _brush = Instantiate(_brushPrefab, transform);

            //_trails.Add(_brush);
            
            var trail = Instantiate(_brushPrefab, transform);
                
            trail.transform.position = Input.mousePosition;
            
            _trails.Add(trail);
        }

        if (Input.GetMouseButton(0))
        {
            _brush.transform.position = Input.mousePosition;

            if (Vector2.Distance(_trails[_trails.Count - 1].transform.position, _brush.transform.position) > 10f)
            {
                var trail = Instantiate(_brushPrefab, transform);
                
                trail.transform.position = Input.mousePosition;

                _trails.Add(trail);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(_brush);

            _positions = new Vector3[_trails.Count];
            
            for (int i = 0; i < _trails.Count; i++)
            {
                _positions[i] = _trails[i].transform.position;
                
                Destroy(_trails[i]);
            }

            Test(_positions);
            
            _trails.Clear();
            
            Positions?.Invoke(_positions);
        }
    }

    private void Test(Vector3[] pos)
    {
        Debug.Log(_positions.Length);
        
        for (int i = 0; i < pos.Length; i++)
        {
            //Debug.Log(pos[i] + "\n");
        }
    }
    
}