using NaughtyAttributes;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform monster;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private float bounceHeight;

    [SerializeField] private float moveSpeed;

    private int _tileIndex = 1;

    private Vector3 _nextPos;

    private bool _reachedEnd;

    private bool hasMonster;

    private void Start()
    {
        _nextPos = Grid.Instance.Path[_tileIndex++].transform.position;
        hasMonster = monster != null;
    }

    public void Update()
    {
        if (hasMonster)
        {
            var y = bounceHeight + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            var monsterTransform = monster.transform;
            var monsterPos = monsterTransform.localPosition;
            monsterPos.y = y;
            monsterTransform.localPosition = monsterPos;
        }

        if (_reachedEnd)
            return;

        var pos = transform.position;
        _nextPos.y = pos.y;
        var dir = (_nextPos - pos).normalized;
        var distanceToTarget = Vector3.Distance(pos, _nextPos);
        pos += (dir * (Time.deltaTime * moveSpeed));
        LookTowardsNext(pos, distanceToTarget);
        transform.position = pos;

        if (distanceToTarget < 0.1f)
        {
            if (_tileIndex == Grid.Instance.Path.Count)
            {
                _reachedEnd = true;
                return;
            }

            _nextPos = Grid.Instance.Path[_tileIndex++].transform.position;
        }
    }

    [SerializeField] private float rotationDistance;
    [SerializeField] private float rotationMultiplier;

    //TODO: Make this a spline path
    private void LookTowardsNext(Vector3 pos, float distance)
    {
        if (_tileIndex + 1 >= Grid.Instance.Path.Count)
            return;

        if (distance > rotationDistance)
            return;

        var nextPos = Grid.Instance.Path[_tileIndex + 1].transform.position;
        nextPos.y = pos.y;
        var projectedDir = (nextPos - pos).normalized;
        var lookRotation = Quaternion.LookRotation(projectedDir);
        Debug.DrawRay(pos, projectedDir * 2f, Color.red);
        Debug.DrawRay(pos, transform.forward * 1.5f, Color.blue);
        var rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationMultiplier);
        transform.rotation = rotation;
    }

    [Button]
    public void ResetFollower()
    {
        _tileIndex = 1;
        _nextPos = Grid.Instance.Path[_tileIndex++].transform.position;
        var startPoint = Grid.Instance.Path[0].transform.position;
        var pos = transform.position;
        startPoint.y = pos.y;
        transform.position = startPoint;
        _reachedEnd = false;
    }
}