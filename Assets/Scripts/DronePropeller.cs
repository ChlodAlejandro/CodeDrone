using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePropeller : MonoBehaviour
{

    private int id;
    public float maxThrust;
    public string propellerName = "";

    private DroneController controller;

    [SerializeField]
    private float _thrust = 0;
    public float Thrust
    {
        get => _thrust * 0.1f;
    }

    void Awake()
    {
        id = Mathf.RoundToInt(Random.Range(0, (float) int.MaxValue));

        bool hasController = false;
        Transform currentParent = transform.parent;
        while (currentParent != null)
        {
            if (currentParent.TryGetComponent(out controller))
            {
                if (!hasController)
                    hasController = true;
                else
                {
                    controller = null;
                    id = -1;
                    Debug.LogError("Propeller has two controllers!");
                    break;
                }
            }
            currentParent = currentParent.transform.parent;
        }
    }

    public Vector3 GetThrustVector()
    {
        return transform.up * Thrust;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.up * Thrust));
    }

    public void SetThurst(float newThrust)
    {
        _thrust = Mathf.Clamp(newThrust, 0, maxThrust);
    }

}
