using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyYUDocument : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }
}
