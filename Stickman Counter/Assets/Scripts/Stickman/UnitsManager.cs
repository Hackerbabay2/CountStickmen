using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class UnitsManager : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _radiusAroundTarget;
    [SerializeField] private int _circleLayers = 5;
    [SerializeField] private int _gizmosCountUnits = 1;
    [SerializeField] private float _density;

    public int CircleLayrs => _circleLayers;
    public Transform Target => _target;
    public float RadiusAroundTarget => _radiusAroundTarget;
    public List<AIUnit> Units = new List<AIUnit>();
    public float Density => _density;
    public List<Vector3> Points => _points;
    private List<Vector3> _points = new List<Vector3>();

    public float GetSpiralRadius()
    {
        return Mathf.Clamp(Mathf.Sqrt(Units.Count) / Mathf.Sqrt(_density - _radiusAroundTarget),-4,4);
    }

    public void MakeUnitsCircleTarget()
    {
        _points = new List<Vector3>();
        float goldenAngle = Mathf.PI * (3 - Mathf.Sqrt(5));

        for (int z = 0; z < _circleLayers; z++)
        {
            for (int i = 0; i < Units.Count + 3; i++)
            {
                float theta = i * goldenAngle;
                float radius = Mathf.Sqrt(i) / Mathf.Sqrt(_density - _radiusAroundTarget);
                _points.Add(new Vector3(
                    Mathf.Clamp(_target.position.x - radius * Mathf.Cos(theta), -4, 4),
                    _target.position.y,
                    _target.position.z - radius * Mathf.Sin(theta)
                    ));
            }
        }
    }

    private void Start()
    {
        MakeUnitsCircleTarget();
        SetAllUnitsDestination();
    }

    public void RemoveEndUnit()
    {
        if (Units.Count > 0)
        {
            Destroy(Units[Units.Count - 1].gameObject);
            Units.Remove(Units[Units.Count - 1]);
        }
    }

    public IEnumerator SetAnimationAllUnits(int anim, bool isRun = true)
    {
        foreach (AIUnit aiunit in Units)
        {
            aiunit.SetAnimation(anim, isRun);
            yield return null;
        }
    }

    public void RemoveUnit(AIUnit aIUnit)
    {
        Units.Remove(aIUnit);
        Destroy(aIUnit.gameObject);
    }

    public void SetAllUnitsDestination()
    {
        for (int i = 0; i < Units.Count; i++)
            if (Units[i].IsPositionSet == false)
                Units[i].SetDestination(_points[i]);
    }

    public void SetAllUnitsMoveTarget(List<Vector3> positions)
    {
        for (int i = Units.Count; i > 0; i--)
        {
            if (Units.Count < i)
            {
                Units[Units.Count - i].Coroutine = StartCoroutine(Units[Units.Count - i].MoveTarget(positions[i]));
            }
        }
    }

    public IEnumerator StopAllUnitsCoroutine()
    {
        for (int i = Units.Count - 1; i > 0; i--)
        {
            if (Units[i].Coroutine !=null)
            {
                Units[i].StopCoroutine(Units[i].Coroutine);
                Units[i].SetDestination(_points[i]);
            }
            yield return null;
        }
    }
}