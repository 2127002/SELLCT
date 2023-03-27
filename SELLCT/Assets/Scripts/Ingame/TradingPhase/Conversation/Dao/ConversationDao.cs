using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class ConversationDao
{
    public List<ConversationData> LoadConversations(string filePath)
    {
        var serializer = new XmlSerializer(typeof(List<ConversationData>));
        using var stream = new FileStream(filePath, FileMode.Open);
        return (List<ConversationData>)serializer.Deserialize(stream);
    }
}
