using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
    GameObject snakeHead;
    public int bodyPosition;

    private void Start()
    {
        snakeHead = GameObject.Find("SnakeHead");
    }

    /// <summary>
    /// Update is called once per frame
    /// Stops body movement if the game is over
    /// </summary>
    void LateUpdate()
    {
        if (LevelManager.Instance.IsGameOver()) {
            return;
        }

        MoveSnakeBody();
    }

    /// <summary>
    /// Knowing its position in the snake body, searches the correct command in the command list
    /// and copies the exact movement that the head did
    /// </summary>
    private void MoveSnakeBody()
    {
        int commandOffset = GetCommandOffset();

        MoveCommand moveCommand = (MoveCommand)CommandManager.Instance.GetCommand(bodyPosition * commandOffset);
        if (moveCommand != null) {
            transform.position = moveCommand._position;
            transform.Translate(moveCommand._direction * Time.deltaTime * moveCommand._speed);
        }
    }

    private int GetCommandOffset()
    {
        float currentSpeed = snakeHead.GetComponent<SnakeHeadController>().speed;
        int commandOffset = Mathf.FloorToInt(1000 / currentSpeed);
        return commandOffset;
    }
}
