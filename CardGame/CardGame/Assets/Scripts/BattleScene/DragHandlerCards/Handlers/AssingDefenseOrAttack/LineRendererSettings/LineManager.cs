using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineManager : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private Transform _startPoint;
    private Vector3 targerPosition;

    private void Awake()
    {
        _lineRenderer.enabled = false;
        SettingsLine();
    }

    private void Update()
    {
        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(0, _startPoint.position);
            _lineRenderer.SetPosition(1, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
        }
    }

    private void SettingsLine()
    {
        _lineRenderer.startWidth = 50;
        _lineRenderer.endWidth = 50;
        _lineRenderer.alignment = LineAlignment.TransformZ;
    }
    
    public void ShowIndication()
    {
        _lineRenderer.enabled = true;
    }

    public void OffLine()
    {
        _lineRenderer.enabled = false;
    }
}
