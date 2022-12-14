using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float _speed = 10;
    [SerializeField] int _currentID = 0;
    Vector2 _target;
    [SerializeField] List<Vector2> _listPos = new List<Vector2>();

    bool _way = true;
    // Start is called before the first frame update
    void Start()
    {
        if (_listPos.Count == 0)
            return;
        _target = _listPos[_currentID];
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = Vector2.MoveTowards(this.transform.position, _target, _speed * Time.deltaTime);

        if (Vector2.Distance(this.transform.position, _target) > 0.5f)
        {
            return;
        }

        //if (_target.Equals(_origin))
        //    _target = _finish;
        //else
        //    _target = _origin;

        //_target = _target.Equals(_origin) ? _finish : _origin;

        //_currentID++;
        //if (_currentID >= _listPos.Count)
        //{
        //    _currentID = 0
        //}
        if (_way)
        {
            _currentID++;
            if (_currentID >= _listPos.Count)
            {
                _currentID = _listPos.Count - 2;
                _way = false;
            }
        }
        else
        {
            _currentID--;
            if (_currentID < 0)
            {
                _currentID = 1;
                _way = true;
            }
        }

        _target = _listPos[_currentID];
    }

    private void OnDrawGizmosSelected()
    {
        if (_listPos.Count == 0)
            return;
    }
}