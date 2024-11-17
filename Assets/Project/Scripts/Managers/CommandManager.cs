using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    private List<ICommand> _commandBuffer = new List<ICommand>();

    /// <summary>
    /// Adds a new command to the list
    /// </summary>
    /// <param name="command"></param>
    public void AddCommand(ICommand command)
    {
        _commandBuffer.Insert(0, command);
    }

    /// <summary>
    /// Gets the command of given offset
    /// In this game, each body segment will give an offset based on its position in the snake body
    /// so that it can copy exactly what the head did
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ICommand GetCommand(int offset)
    {
        if (_commandBuffer.Count <= offset) {
            return null;
        }
        
        return _commandBuffer[offset];
    }

    public int GetCommandBufferSize()
    {
        return _commandBuffer.Count;
    }

    public void DeleteLastCommand()
    {
        _commandBuffer.RemoveAt(GetCommandBufferSize()-1);
    }
}
