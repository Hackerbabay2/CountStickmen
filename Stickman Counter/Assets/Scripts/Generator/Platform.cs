using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<GameObject> _traps;
    [SerializeField] private List<GameObject> _bounces;
    [SerializeField] private int _seed;
    [SerializeField] private Transform _pool;
    [SerializeField] private UnitsManager _unitsManager;

    private Transform _startPoint;
    private List<Transform> _bouncePoints = new List<Transform>();
    private List<Transform> _trapPoints = new List<Transform>();

    public Transform StartPoint => _startPoint;
    public List<Transform> BouncePoints => _bouncePoints;
    public List<Transform> TrapPoints => TrapPoints;

    private void Start()
    {
        //Random.InitState(_seed);
        Debug.Log($"{Random.seed}");
        _startPoint = transform.GetChild(0).transform.GetChild(0).transform;
        SetChildPoints(transform.GetChild(1).transform,ref _trapPoints);
        SetChildPoints(transform.GetChild(2).transform,ref _bouncePoints);
        StartCoroutine(CreateInteractive());

        for (int i = 0; i < _bounces.Count; i++)
        {
            Debug.Log($"{i}:{_bounces[i].name}");
        }
    }

    private void SetChildPoints(Transform transform, ref List<Transform> _points)
    {
        for (int i = 0; i < transform.childCount; i++)
            _points.Add(transform.GetChild(i));
    }

    private IEnumerator CreateInteractive()
    {
        foreach (Transform bounce in _bouncePoints)
        {
            if (Random.Range(0,100) <= 25)
            {
                int index = Random.Range(0, _bounces.Count);
                GameObject tempBounce = Instantiate(_bounces[index]);

                switch (index)
                {
                    case 0: case 1:
                        tempBounce.transform.SetParent(bounce);
                        tempBounce.transform.localPosition = Vector3.zero + tempBounce.transform.position;
                        tempBounce.GetComponent<BonuseScaler>().SetData(_unitsManager,_pool);
                        break;
                    default:
                        break;
                }
            }
            yield return null;
        }

        for (int i = 0; i < _trapPoints.Count; i++)
        {
            if (Random.Range(0,100) <= 30 && _bouncePoints[i].childCount == 0)
            {
                int index = Random.Range(0, _traps.Count);
                Debug.Log(index + ", " + i);
                GameObject tempTrap = Instantiate(_traps[index]);
                tempTrap.transform.SetParent(_trapPoints[i]);

                if (i == 2)
                {
                    if (index == 2)
                    {
                        tempTrap.transform.localPosition += new Vector3(1,0,0);
                    }
                    
                    if (index == 0)
                        tempTrap.transform.localPosition += new Vector3(1,0,0);
                }
                tempTrap.transform.localPosition = Vector3.zero + tempTrap.transform.position;
            }
            yield return null;
        }
    }
}