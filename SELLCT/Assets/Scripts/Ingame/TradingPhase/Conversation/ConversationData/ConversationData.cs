using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationData
{
    public int Id { get; set; }
    public int Location { get; set; }
    public int Likability { get; set; }
    public bool HasChoice { get; set; }
    public string Character { get; set; }
    public List<TextData> Texts { get; set; }
}

public class TextData
{
    public string Text { get; set; }
    public int? Delay { get; set; }
    public int? TextId { get; set; }
}