using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUGraph : MonoBehaviour
{
    [SerializeField]
    Material material;

    [SerializeField]
    Mesh mesh;
    
    [SerializeField] 
    ComputeShader computeShader;
    
    [SerializeField, Range(0, 1)]
    float majorRadius;

    [SerializeField, Range(0, 1)]
    float minorRadius;
    
    [SerializeField, Range(10, 1000)]
    int resolution = 10;

    [SerializeField]
    GraphLibrary.functionNames functionName;

    Transform[] points;

    ComputeBuffer positionsBuffer;
    
    private ComputeBuffer argsBuffer;
    
    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    private static readonly int
        positionsID = Shader.PropertyToID("_Positions"),
        stepID = Shader.PropertyToID("_Step"),
        resolutionID = Shader.PropertyToID("_Resolution"),
        timeID = Shader.PropertyToID("_Time");

    void UpdateFunctionOnGPU()
    {
        float step = 2f / resolution;
        computeShader.SetFloat(stepID, step);
        computeShader.SetInt(resolutionID, resolution);
        computeShader.SetFloat(timeID, Time.time);
        computeShader.SetBuffer(0, positionsID, positionsBuffer);
        int groups =  Mathf.CeilToInt(resolution / 4f );
        computeShader.Dispatch(0, groups, groups, 1);
        var bounds = new Bounds(Vector3.zero, Vector3.one * (2f + 2f / resolution));
        Graphics.DrawMeshInstancedProcedural(mesh, 0, material, bounds, positionsBuffer.count);
        //Graphics.DrawMeshInstancedIndirect(mesh, 0, material, bounds, argsBuffer);
    }
    void OnEnable()
    {
        positionsBuffer = new ComputeBuffer(resolution * resolution, 3 * sizeof(float));
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
    }

    void OnDisable()
    {
        positionsBuffer.Release();
        positionsBuffer = null;
    }
    
    void Update()
    {
        UpdateFunctionOnGPU();
    }
}