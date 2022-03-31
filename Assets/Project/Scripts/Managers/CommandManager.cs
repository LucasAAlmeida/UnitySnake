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

    public void AddCommand(ICommand command)
    {
        _commandBuffer.Insert(0, command);
    }

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
