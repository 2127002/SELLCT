using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullChoice : ITalkChoice
{
    public string Text => throw new System.NotImplementedException();

    public bool Enabled => throw new System.NotImplementedException();

    public int Id => throw new System.NotImplementedException();

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Enable()
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        throw new System.NotImplementedException();
    }
}
