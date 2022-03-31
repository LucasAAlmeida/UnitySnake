using UnityEngine;

public class MoveCommand : ICommand
{
    Transform _transform;
    public Vector3 _position;
    public Vector3 _direction;
    public float _speed;

    public MoveCommand(Transform transform, Vector3 direction, float speed)
    {
        _transform = transform;
        _position = transform.position;
        _direction = direction;
        _speed = speed;
    }

    public void Execute()
    {
        _transform.Translate(_direction * Time.deltaTime * _speed);
    }
}
