namespace FSR.NET.Generator;

internal class GenerationPipeline(AstModel model)
{
    internal Dictionary<string, string> Generate()
    {
        Dictionary<string, string> files = [];
        TypeMapper mapper = new(model);
        EnumEmitter enums = new(files);
        StructEmitter structs = new(mapper, files);
        FunctionEmitter functions = new(mapper, files);
        ConstantsEmitter constants = new(model, files);
        CallbackEmitter callbacks = new(mapper, files);

        foreach ((string name, AstEnum value) in model.Enums)
        {
            enums.WriteEnum(name, value);
        }

        foreach ((string name, AstRecord value) in model.Records)
        {
            structs.WriteRecord(name, value);
        }

        structs.WriteHandle("ffxContext", model.Aliases["ffxContext"]);
        functions.WriteFunctions("D3D12", model.Functions.Values);
        functions.WriteFunctions("Vulkan", model.Functions.Values);
        constants.WriteConstants();
        callbacks.WriteCallbacks(model.Aliases);

        return files;
    }
}
