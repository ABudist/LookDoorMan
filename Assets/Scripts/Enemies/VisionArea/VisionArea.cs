using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.VisionArea
{
  public class VisionArea : MonoBehaviour
  {
    public event Action<GameObject> OnVisionEnter;
    public event Action<GameObject> OnVisionExit;
    
    [SerializeField] private float _visionAngle;
    [SerializeField] private MeshRenderer _renderer;

    private List<Transform> _objInTrigger = new List<Transform>();
    private List<Transform> _objInVision = new List<Transform>();

    private Material _material;
    private static readonly int Angle = Shader.PropertyToID("_Angle");
    private Color _origColor;

    private void Start()
    {
      _material = new Material(_renderer.material);
      _renderer.material = _material;
      _material.SetFloat(Angle, _visionAngle);

      _origColor = _material.color;
    }

    private void Update()
    {
      foreach (Transform obj in _objInTrigger)
      {
        Vector3 dirToTarget = (obj.position - transform.position).normalized;
        float angle = Mathf.Acos(Vector3.Dot(dirToTarget, transform.forward)) * Mathf.Rad2Deg;
          
        if (angle < _visionAngle)
        {
          int layerMask = 1 << 8;
            
          if (Physics.Raycast(new Ray(transform.position+ new Vector3(0, 0.5f, 0), dirToTarget), out var hit, layerMask))
          {
            if (hit.transform == obj)
            {
              if (!_objInVision.Contains(obj))
              {
                OnVisionEnter?.Invoke(obj.gameObject);
            
                _objInVision.Add(obj);
              }
            }
          }
        }
        else
        {
          TryRemoveObjFromVision(obj);
        }
      }
    }

    public void SetColor(Color color)
    {
      _material.color = color;
    }

    public void SetOrigColor()
    {
      _material.color = _origColor;
    }

    private void OnTriggerEnter(Collider other)
    {
      _objInTrigger.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
      _objInTrigger.Remove(other.transform);
      
      TryRemoveObjFromVision(other.transform);
    }

    private void TryRemoveObjFromVision(Transform target)
    {
      if (_objInVision.Contains(target))
      {
        _objInVision.Remove(target);
            
        OnVisionExit?.Invoke(target.gameObject);
      }
    }
  }
}