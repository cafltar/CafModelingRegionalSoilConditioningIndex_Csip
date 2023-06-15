/// Command line arguments
/// Args[0]: Full path to csv containing InputCentroidLocation Locations; e.g. C:\csip\run_20200110\input\input-locations.csv
/// Args[1]: Size of pixel, in km. Assumes square pixel, so this specifies length or width
/// Args[2]: (Optional) Full path to output file, including file name; e.g. C:\csip\run_20200110\working\csip-locations.csv
#r "../dotnet/Csip.Common/bin/Debug/netstandard2.1/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/netstandard2.1/Csip.Engine.dll"
#r "nuget:CsvHelper, 12.0.0"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Helpers;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;
using System.Net.Http;


var engine = new InputCentroidLocationToCsipLocation(
    new CsvHandler(),
    new WweSoilParamsV2_0(new HttpClient()),
    new PointToPolygonConverter(),
    new CokeyChooser());

string inputFile = Args[0];
double pixelSize = Convert.ToDouble(Args[1]);
string outputFile;

if(Args.Count > 2)
{
    string outputFile = Args[2];
}
else {
    outputFile = Path.Combine(
        Directory.GetCurrentDirectory(), 
        "working", 
        "csip-locations.csv");
}

if(!Directory.Exists(Path.GetDirectoryName(outputFile)))
{
    Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
}

Console.WriteLine($"Running with input: {inputFile}, pixel size: {pixelSize}, output: {outputFile}");

await engine.Run(inputFile, outputFile, pixelSize);