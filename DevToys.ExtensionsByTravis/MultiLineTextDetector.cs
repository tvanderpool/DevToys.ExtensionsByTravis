using DevToys.Api;
using System.ComponentModel.Composition;

namespace DevToys.ExtensionsByTravis;

[Export(typeof(IDataTypeDetector))]
[DataTypeName(MultiLineTextName, baseName: PredefinedCommonDataTypeNames.Text)]
internal sealed partial class MultiLineTextDetector : IDataTypeDetector {
    internal const string MultiLineTextName = "Multi Line Text";

    public ValueTask<DataDetectionResult> TryDetectDataAsync(object rawData,
                                                             DataDetectionResult? resultFromBaseDetector,
                                                             CancellationToken cancellationToken)
        => ValueTask.FromResult(
            resultFromBaseDetector?.Data is string dataString && dataString.Contains("\n")
                ? new DataDetectionResult(true, dataString)
                : DataDetectionResult.Unsuccessful);

}