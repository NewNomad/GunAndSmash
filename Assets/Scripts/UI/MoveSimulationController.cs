using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationController : MonoBehaviour
{
    [SerializeField] int maxStep;
    LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();

}
