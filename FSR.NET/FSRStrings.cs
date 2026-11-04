using System.Text;

namespace FSR.NET;

public unsafe class FSRStrings : DisposableObject
{
    private readonly List<nint> allocations = [];

    public byte* AllocUtf8(string value)
    {
        return Alloc(value, Encoding.UTF8, sizeof(byte));
    }

    public void* AllocWide(string value)
    {
        return Alloc(value, Encoding.Unicode, sizeof(char));
    }

    protected override void Destroy()
    {
        if (allocations is null)
        {
            return;
        }

        for (int i = allocations.Count - 1; i >= 0; i--)
        {
            NativeMemory.Free((void*)allocations[i]);
        }
        allocations.Clear();
    }

    private byte* Alloc(string value, Encoding encoding, int terminatorSizeInBytes)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        int lengthInBytes = encoding.GetByteCount(value);
        int sizeInBytes = checked(lengthInBytes + terminatorSizeInBytes);
        allocations.EnsureCapacity(checked(allocations.Count + 1));

        byte* pointer = (byte*)NativeMemory.AllocZeroed((nuint)sizeInBytes);
        allocations.Add((nint)pointer);
        encoding.GetBytes(value, new Span<byte>(pointer, lengthInBytes));

        return pointer;
    }
}
