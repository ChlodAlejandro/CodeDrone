using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DroneMovementMode
{
    Stabilize,
    Targeted,
    Free
}

public enum TiltMode
{
    BodyTilt,
    PropellerTilt
}

[RequireComponent(typeof(Rigidbody))]
public class DroneController : MonoBehaviour
{

    private int id;
    private List<DronePropeller> propellers;

    private WindController wind;
    private Rigidbody rb;
    private DroneMovementMode mode = DroneMovementMode.Stabilize;
    private TiltMode tilt = TiltMode.BodyTilt;

    bool GetDependencies()
    {
        rb = GetComponent<Rigidbody>();

        if (GameObject.FindGameObjectWithTag(WindController.gameManagerTag) == null)
        {
            Debug.LogError("Can't find WindController from GameManager!");
            return false;
        }
        else
        {
            wind = GameObject.FindGameObjectWithTag(WindController.gameManagerTag).GetComponent<WindController>();
        }
        return true;
    }

    void Awake()
    {
        if (!GetDependencies())
            Debug.LogError("Game may not function without dependencies!");

        id = Mathf.RoundToInt(Random.Range(0, (float)int.MaxValue));

        propellers = GetComponentsInChildren<DronePropeller>().ToList();
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case DroneMovementMode.Stabilize:
                {
                    if (tilt == TiltMode.BodyTilt)
                    {
                        Vector3 sv = GetStabilityVector();


                    }
                    break;
                }
        }

        foreach (DronePropeller propeller in propellers)
        {
            if (propeller.Thrust != 0)
            {
                rb.AddForceAtPosition(propeller.GetThrustVector() * Time.fixedDeltaTime, propeller.transform.position, ForceMode.VelocityChange);
            }
        }
        rb.AddForce(wind.GetWindForce() * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(rb.velocity.x, 0, 0));
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, rb.velocity.y, 0));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 0, rb.velocity.z));
        }
    }

    Vector3 GetStabilityVector()
    {
        return new Vector3(0, 0, 0) - rb.velocity;
    }
}
