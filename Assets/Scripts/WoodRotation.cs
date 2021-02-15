using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotationInfo
    {
        public float speedRotation;
        public float duration;
    }

    [SerializeField]
    private RotationInfo[] allRotationInfo;

    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();

        StartCoroutine("StartWoodRotation");
    }

    private IEnumerator StartWoodRotation()
    {
        int rotationIndex = Random.Range(0, allRotationInfo.Length-1);

        while (true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = allRotationInfo[rotationIndex].speedRotation;
            motor.maxMotorTorque = 10000;

            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(allRotationInfo[rotationIndex].duration);

            rotationIndex = Random.Range(0, allRotationInfo.Length - 1);
        }
    }

    //TODO: надо бы сделать плавное ускорение и замедление;
}
