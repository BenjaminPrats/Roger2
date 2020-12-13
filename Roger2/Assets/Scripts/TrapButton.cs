using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    public AudioSource _soundButton;
    public AudioSource _soundTrap;

    public MovingTraps[] traps;
    public float _duration = 5.0f;
    public float _yTrigger = 0.2f;
    public bool _whenTriggerGoDown = true;

    bool _timerActive = false;
    float _timeRemaining;

    private void Start()
    {
        ResetTraps(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerActive)
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining < 0.0f)
            {
                ResetTraps();
                transform.position = transform.position + _yTrigger * Vector3.up;
                _timerActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _soundButton.Play();
        CarController carController = other.gameObject.transform.parent.gameObject.GetComponent<CarController>();
        if (carController)
        {
            _timerActive = false;
            TriggerTraps();
            transform.position = transform.position - _yTrigger * Vector3.up;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CarController carController = other.gameObject.transform.parent.gameObject.GetComponent<CarController>();
        if (carController)
        {
            _timerActive = true;
            _timeRemaining = _duration;
        }
    }

    private void ResetTraps(bool playSound = true)
    {
        MoveTraps(!_whenTriggerGoDown, playSound);
    }

    private void TriggerTraps()
    {
        MoveTraps(_whenTriggerGoDown);
    }

    private void MoveTraps(bool down, bool playSound = true)
    {
        if (playSound)
            _soundTrap.Play();
        for (int i = 0; i < traps.Length; i++)
        {
            if (down)
                traps[i].GoDown();
            else
                traps[i].GoUp();
        }
    }
}
