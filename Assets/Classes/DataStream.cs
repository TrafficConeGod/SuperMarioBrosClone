using System;
using System.IO;
using System.Collections.Generic;

/// <Summary> This object contains a stream of binary data from a source </Summary>
public class DataStream {
    private List<byte> data;

    public DataStream(string path) {
        data = new List<byte>(File.ReadAllBytes(path));
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
        return true;
    }
}