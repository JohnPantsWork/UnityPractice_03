using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup")]
    [Tooltip("How fast ship moves up and down")] [SerializeField] float controlSpeed = 10f;
    [Tooltip("How far ship moves x")] [SerializeField] float xRange = 5f;
    [Tooltip("How far ship moves y")] [SerializeField] float yRange = 2.5f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -8f;
    [SerializeField] float positionYawFactor = 9f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10;
    [SerializeField] float controlRollFactor = -10;

    float xThrow;
    float yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float pitchDueToControl = yThrow * controlPitchFactor;
        float rollDueToControl = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = yawDueToPosition;
        float roll = rollDueToControl;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        // if push button , print console : don't print console
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (var item in lasers)
        {
            var emissionModule = item.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
