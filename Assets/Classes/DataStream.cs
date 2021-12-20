using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Godot;

/// <Summary> This object contains a stream of binary data from a source </Summary>
public class DataStream {
    private List<byte> data;

    public DataStream(string path) {
        data = new List<byte>(System.IO.File.ReadAllBytes(path));
    }

    /// <Summary> If the stream is empty </Summary>
    public bool Empty { get => data.Count == 0; }

    /// <Summary> Reads count bytes from the stream </Summary>
    public byte[] ReadBytes(uint count) {
        // read and remove count bytes from stream
        var bytes = data.GetRange(0, checked((int)count)).ToArray();
        data.RemoveRange(0, checked((int)count));
        //
        return bytes;
    }

    /// <Summary> Reads a variant type from the stream </Summary>
    public object ReadVariant(Type type) {
        switch (Type.GetTypeCode(type)) {
            case TypeCode.Int32: return (object)ReadInt();
            case TypeCode.UInt32: return (object)ReadUInt();
            case TypeCode.Int16: return (object)ReadShort();
            case TypeCode.UInt16: return (object)ReadUShort();
            case TypeCode.Boolean: return (object)ReadBoolean();
            case TypeCode.Object: return (object)ReadObject(type);
        }
        throw new InvalidStateException("Unsupported type " + type.Name);
    }

    private object ReadObject(Type type) {
        if (type == typeof(Vector2)) {
            return (object)ReadVector2();
        }
        throw new InvalidStateException("Unsupported type " + type.Name);
    }

    /// <Summary> Reads an int32 from the stream </Summary>
    public int ReadInt() {
        return BitConverter.ToInt32(ReadBytes(4), 0);
    }

    /// <Summary> Reads a uint32 from the stream </Summary>
    public uint ReadUInt() {
        return BitConverter.ToUInt32(ReadBytes(4), 0);
    }

    /// <Summary> Reads an int16 from the stream </Summary>
    public short ReadShort() {
        return BitConverter.ToInt16(ReadBytes(2), 0);
    }

    /// <Summary> Reads a uint16 from the stream </Summary>
    public ushort ReadUShort() {
        return BitConverter.ToUInt16(ReadBytes(2), 0);
    }

    /// <Summary> Reads a boolean from the stream </Summary>
    public bool ReadBoolean() {
        return BitConverter.ToBoolean(ReadBytes(1), 0);
    }

    /// <Summary> Reads a float from the stream </Summary>
    public float ReadFloat() {
        return BitConverter.ToSingle(ReadBytes(4), 0);
    }

    /// <Summary> Reads a Vector2 from the stream </Summary>
    public Vector2 ReadVector2() {
        return new Vector2(ReadFloat(), ReadFloat());
    }
}