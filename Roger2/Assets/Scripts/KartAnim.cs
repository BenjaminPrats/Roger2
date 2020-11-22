using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class KartAnim : MonoBehaviour
{
    public CarController _carController;
    public Transform _modelTransform;
    public float _rotationBoost;
    public GameObject[] _particlesBoost;

    public Transform _wheelsBack;
    public Transform _wheelFront_Right;
    public Transform _wheelFront_Left;
    public Transform _barFront;
    public Transform _steeringWheel;

    public float _maxRotationWheel = 45.0f;
    public float _maxRotationSteeringWheel = 80.0f;
    public float _smoothRotationFactor = 5.0f;
    public float _wheelSpeed = 1.0f;

    private bool _boostWasActivated = false;
    private Volume _ppVolume;
    private ChromaticAberration _chromaticAberration;

    private void Start()
    {
        _ppVolume = GetComponent<Volume>();
        _ppVolume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
    }

    void Update()
    {
        float verticalInput = _carController.GetVerticalInput();
        float rotationInput = _carController.GetRotationInput();

        Quaternion targetRotationWheel = Quaternion.Euler(0.0f, 0.0f, rotationInput * _maxRotationWheel); // Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        Quaternion smoothRotation = Quaternion.RotateTowards(_wheelFront_Right.localRotation, targetRotationWheel, _smoothRotationFactor);
        _wheelFront_Right.localRotation = smoothRotation;
        _wheelFront_Left.localRotation = smoothRotation;

        _steeringWheel.localRotation = Quaternion.RotateTowards(_steeringWheel.localRotation, Quaternion.Euler(0.0f, 0.0f, -rotationInput * _maxRotationSteeringWheel), _smoothRotationFactor);


        _wheelsBack.localRotation = Quaternion.Euler(_wheelsBack.localRotation.eulerAngles + new Vector3(verticalInput * _wheelSpeed, 0.0f, 0.0f));


        // Boost Anim
        bool isBoostActivated = _carController.IsBoostActivated();
        bool boostStateChanged = isBoostActivated != _boostWasActivated;
        if (boostStateChanged)
        {
            for (int i = 0; i < _particlesBoost.Length; i++)
                _particlesBoost[i].SetActive(isBoostActivated);

            _chromaticAberration.active = isBoostActivated;
        }

        float smoothnessFactor = 1.0f;
        Quaternion targetRotation = isBoostActivated ? Quaternion.Euler(_rotationBoost, 0.0f, 0.0f) : Quaternion.identity;
        _modelTransform.localRotation = Quaternion.RotateTowards(_modelTransform.localRotation, targetRotation, smoothnessFactor);

        _boostWasActivated = isBoostActivated;
    }


}
