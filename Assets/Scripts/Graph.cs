using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    float majorRadius;

    [SerializeField, Range(0, 1)]
    float minorRadius;

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    GraphLibrary.functionNames functionName;

    Transform[] points;

    void Awake() {
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++) {
            points[i] = Instantiate(pointPrefab);
        }
    }

    void Update()
    {
        GraphLibrary.ma = majorRadius;
        GraphLibrary.mi = minorRadius;

        float u = 0;
        
        float step = 2f / resolution;
        Vector3 location = Vector3.zero;
        var scale = Vector3.one * step;
        float v = ((0.5f) * step - 1f);
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution) {
                x = 0;
                z += 1;
                v = ((z + 0.5f) * step - 1f);
            }

            Transform point = points[i];
            u = ((x + 0.5f) * step - 1f);
            

            location = (GraphLibrary.getIndex(functionName)(u, v, Time.time * 0.5f));
            //location.y = location.y * 0.4f;

            point.localPosition = location;
            point.localScale = scale;
            point.SetParent(transform, false);

        }

    }
}