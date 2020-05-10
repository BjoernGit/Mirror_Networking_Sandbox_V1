using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileNetwork : MonoBehaviour
{
    #region serialized
    [Tooltip("how quickly to move")]
    [SerializeField]
    private float _moveRate = 7f;
    #endregion

    #region private
    private float _destroyTime = -1f;

    private float _catchupDistance = 0f;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (_destroyTime < 0)
            return;

        float moveValue = _moveRate * Time.deltaTime;
        float catchupValue = 0f;

        if (_catchupDistance > 0f)
        {
            float step = (_catchupDistance * Time.deltaTime);
            _catchupDistance -= step;
            catchupValue = step;

            if (_catchupDistance < (moveValue * .1f))
            {
                catchupValue += _catchupDistance;
                _catchupDistance = 0f;
            }
        }

        //transform.position += Vector3.up * (moveValue + catchupValue);
        transform.position += transform.forward * (moveValue + catchupValue);


        if (Time.time > _destroyTime)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(float duration)
    {
        _catchupDistance = (duration * _moveRate);
        _destroyTime = Time.time + 5f;
    }
}
