using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Serializer {

    /// <summary>
    /// Serializes the specified object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    public static byte[] Serialize(object obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// Deserializes the specified arr bytes. Type must be known !
    /// </summary>
    /// <param name="arrBytes">The arr bytes.</param>
    /// <returns></returns>
    public static object Deserialize(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        object obj = (object)binForm.Deserialize(memStream);

        return obj;
    }

}
