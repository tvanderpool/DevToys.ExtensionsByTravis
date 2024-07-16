using DevToys.Api;
using System.ComponentModel.Composition;

namespace DevToys.ExtensionsByTravis;

[Export(typeof(IResourceAssemblyIdentifier))]
[Name(nameof(MyResourceAssemblyIdentifier))]
internal sealed class MyResourceAssemblyIdentifier : IResourceAssemblyIdentifier {
    public ValueTask<FontDefinition[]> GetFontDefinitionsAsync() {
        return ValueTask.FromResult(new FontDefinition[] { });
    }
}
