using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class KartAudio : MonoBehaviour
{
    public CarController _carController;

    public static float _soundSmooth = 0.1f;

    public Animator _idleAnim;
    public Animator _lowAnim;

    public AudioSource _idle;
    public AudioSource _low;
    public AudioSource _high;

    public AudioSource _acceleration;
    public AudioSource _decceleration;

    // Update is called once per frame
    bool _wasIdle = true;

    Sound _idleSound;
    Sound _lowSound;

    Sound _highSound;

    public class Sound
    {
        AudioSource _source;
        public float _targetVolume;
        float _velocity;

        public Sound(AudioSource source)
        {
            _source = source;
        }

        public void Tick()
        {
            _source.volume = Mathf.SmoothDamp(_source.volume, _targetVolume, ref _velocity, _soundSmooth);
        }
    }

    private void Start()
    {
        _idleSound = new Sound(_idle);
        _lowSound = new Sound(_low);
        _highSound = new Sound(_high);

        _idle.Play();
        _low.Play();
        _low.volume = 0.0f;
        _high.Play();
        _high.volume = 0.0f;
    }


    void Update()
    {
        float verticalInput = _carController.GetVerticalInput();

        bool isIdle = verticalInput == 0.0f;
        bool isBoostActivated = _carController.IsBoostActivated();

        _idleSound._targetVolume = isIdle  ? 1.0f : 0.0f;
        _lowSound._targetVolume  = !isIdle ? 1.0f : 0.0f;

        _highSound._targetVolume = isBoostActivated ? 1.0f : 0.0f;


        _idleSound.Tick();
        _lowSound.Tick();
        _highSound.Tick();

        _wasIdle = isIdle;
    }
}
