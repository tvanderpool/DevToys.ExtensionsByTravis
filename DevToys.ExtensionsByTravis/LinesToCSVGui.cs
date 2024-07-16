using DevToys.Api;
using System.ComponentModel.Composition;
using static DevToys.Api.GUI;

namespace DevToys.ExtensionsByTravis;

[Export(typeof(IGuiTool))]
[Name("Lines2CSV")]
[ToolDisplayInformation(
    IconFontName = "FluentSystemIcons",
    IconGlyph = '\uF4E2',
    GroupName = PredefinedCommonToolGroupNames.Converters,
    ResourceManagerAssemblyIdentifier = nameof(MyResourceAssemblyIdentifier),
    ResourceManagerBaseName = $"DevToys.ExtensionsByTravis.{nameof(LinesToCSV)}",
    ShortDisplayTitleResourceName = nameof(LinesToCSV.ShortDisplayTitle),
    LongDisplayTitleResourceName = nameof(LinesToCSV.LongDisplayTitle),
    DescriptionResourceName = nameof(LinesToCSV.Description),
    AccessibleNameResourceName = nameof(LinesToCSV.AccessibleName))]
[AcceptedDataTypeName(MultiLineTextDetector.MultiLineTextName)]
internal sealed class LinesToCSVGui : IGuiTool {
    private readonly IUISwitch _swSpace = Switch().On().Text("Space");
    private readonly IUISwitch _swTrim = Switch().On().Text("Trim");
    private readonly IUISelectDropDownList _ddlQuote = SelectDropDownList()
        .AlignHorizontally(UIHorizontalAlignment.Left)
        .WithItems(
            Item(text: "",   value: ""),
            Item(text: "\"", value: "\""),
            Item(text: "'",  value: "'"))
        .Select(0);
    private readonly IUIMultiLineTextInput _multiLineTextInput = MultiLineTextInput()
        .Title("Lines")
        .Extendable();
    private readonly IUIMultiLineTextInput _multiLineTextOutput = MultiLineTextInput()
        .Title("CSV")
        .Extendable()
        .ReadOnly()
        .AlwaysWrap();

    private string separator = ", ";

    public UIToolView View
        => new UIToolView(
            Stack()
                .Vertical()
                .WithChildren(
                    Stack().Horizontal()
                           .WithChildren(
                                _swTrim.OnToggle(UpdateOutput),
                                _swSpace.OnToggle(OnSpaceToggle),
                                Label().Text("Quote:"),
                                _ddlQuote.OnItemSelected(UpdateOutput)
                           ),
                    _multiLineTextInput.OnTextChanged(UpdateOutput),
                    _multiLineTextOutput
                )
        );

    public void OnDataReceived(string dataTypeName, object? parsedData) {
        if (parsedData is string text)
            _multiLineTextInput.Text(text);
    }

    private void OnSpaceToggle(bool arg){
        separator = arg ? ", " : ",";
        UpdateOutput();
    }

    private void UpdateOutput<T>(T _) => UpdateOutput();

    private void UpdateOutput() {
        var lines = _multiLineTextInput.Text
                                       .Replace("\r\n", "\n")
                                       .Split('\n')
                                       .AsEnumerable();
        if(_swTrim.IsOn)
            lines = lines.Select(line => line.Trim());
        lines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
        lines = lines.Select(line => $"{quote}{line}{quote}");
        _multiLineTextOutput.Text(string.Join(separator, lines));
    }

    private string quote => (string)_ddlQuote.SelectedItem!.Value!;

}