namespace FSR.NET.Generator;

internal class StructEmitter(TypeMapper mapper, Dictionary<string, string> files)
{
    internal void WriteRecord(string name, AstRecord record)
    {
        if (record.IsOpaque)
        {
            return;
        }

        string managed = mapper.GetRecordName(name);
        List<(string Native, string Managed)> members = [.. record.Fields.Select(static field => (field.Name, GetFieldName(field.Name)))];
        foreach (AstField field in record.Fields.Where(static field => field.Type.Kind is NativeTypeKind.ConstantArray))
        {
            string buffer = GetFieldName(field.Name) + "Buffer";
            for (AstType type = field.Type; type.Kind is NativeTypeKind.ConstantArray; type = type.Element!)
            {
                members.Add(($"{field.Name} inline array", buffer));
                buffer += "Element";
            }
        }

        if (record.DescriptorType is not null)
        {
            members.Add(("descriptor type constant", "Type"));
        }

        ValidateNames(members, $"members in {name}");
        CodeWriter text = CreateFile();
        text.WriteLine($"[StructLayout(LayoutKind.Explicit, Size = {record.SizeInBytes})]");
        string modifier = record.Fields.Any(field => mapper.GetFieldType(field).Contains('*')) ? "unsafe " : string.Empty;
        text.BeginBlock($"public {modifier}struct {managed}");
        foreach (AstField field in record.Fields)
        {
            if (field.OffsetInBits < 0 || field.OffsetInBits % 8 is not 0)
            {
                throw new InvalidOperationException($"Unsupported field layout: {name}.{field.Name}.");
            }

            text.WriteLine($"[FieldOffset({field.OffsetInBits / 8})]");
            text.WriteLine($"public {mapper.GetFieldType(field)} {GetFieldName(field.Name)};");
            text.WriteBlankLine();
        }

        if (record.DescriptorType is string descriptorType)
        {
            text.WriteLine($"public const ulong Type = Fsr.{GetConstantName(descriptorType)};");
            text.WriteBlankLine();
            text.BeginBlock($"public {managed}()");
            text.WriteLine("this = default;");
            text.WriteLine($"{GetFieldName("header")}.{GetFieldName("type")} = Type;");
            text.EndBlock();
        }

        foreach (AstField field in record.Fields.Where(static field => field.Type.Kind is NativeTypeKind.ConstantArray))
        {
            WriteArray(text, $"{GetFieldName(field.Name)}Buffer", field.Type);
        }

        text.EndBlock();
        files.Add($"Structs/{managed}.g.cs", text.ToString());
    }

    internal void WriteHandle(string native, AstType type)
    {
        if (type is not { Kind: NativeTypeKind.Pointer, Element.Kind: NativeTypeKind.Void })
        {
            throw new InvalidOperationException($"Unsupported native handle: {native}.");
        }

        string name = GetTypeName(native);
        CodeWriter text = CreateFile();
        text.WriteLine("[StructLayout(LayoutKind.Sequential)]");
        text.BeginBlock($"public readonly struct {name}(nint value) : IEquatable<{name}>");
        text.WriteLine("public readonly nint Value = value;");
        text.WriteBlankLine();
        text.WriteLine("public bool IsNull => Value is 0;");
        text.WriteBlankLine();
        text.BeginBlock($"public bool Equals({name} other)");
        text.WriteLine("return Value == other.Value;");
        text.EndBlock();
        text.BeginBlock("public override bool Equals(object? obj)");
        text.WriteLine($"return obj is {name} other && Equals(other);");
        text.EndBlock();
        text.BeginBlock("public override int GetHashCode()");
        text.WriteLine("return Value.GetHashCode();");
        text.EndBlock();
        text.BeginBlock("public override string ToString()");
        text.WriteLine($"return $\"{name} {{{{ Value = {{Value}}, IsNull = {{IsNull}} }}}}\";");
        text.EndBlock();
        text.BeginBlock($"public static bool operator ==({name} left, {name} right)");
        text.WriteLine("return left.Equals(right);");
        text.EndBlock();
        text.BeginBlock($"public static bool operator !=({name} left, {name} right)");
        text.WriteLine("return !left.Equals(right);");
        text.EndBlock();
        text.EndBlock();
        files.Add($"Structs/{name}.g.cs", text.ToString());
    }

    private void WriteArray(CodeWriter text, string name, AstType type)
    {
        AstType element = type.Element!;
        string elementType = element.Kind is NativeTypeKind.ConstantArray ? $"{name}Element" : mapper.GetManagedType(element);
        text.WriteLine($"[InlineArray({type.Count})]");
        string modifier = elementType.Contains('*') ? "unsafe " : string.Empty;
        text.BeginBlock($"public {modifier}struct {name}");
        text.WriteLine($"private {elementType} element;");
        text.EndBlock();
        if (element.Kind is NativeTypeKind.ConstantArray)
        {
            WriteArray(text, elementType, element);
        }
    }
}
