using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{

    public const string gameManagerTag = "GameManager";

    private float minSpeed;
    private float maxSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    [Range(0, 1)]
    private float direction; // 0 being 0 deg (towards X), 1 being 360 deg (towards X)

    public Vector3 GetWindForce()
    {

        float directionRad = Mathf.Lerp(0, 2 * Mathf.PI, direction);
        float xComp = Mathf.Cos(directionRad) * speed;
        float zComp = Mathf.Sin(directionRad) * speed;

        return new Vector3(xComp, 0, zComp);

    }



}
