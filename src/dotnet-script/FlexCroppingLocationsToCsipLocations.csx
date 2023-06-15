/// Command line arguments
/// Args[0]: Full path to csv containing FlexCropping Locations; e.g. C:\csip\run_20200110\input\flex-cropping-locations.csv
/// Args[1]: (Optional) Full path to output file, including file name; e.g. C:\csip\run_20200110\working\csip-locations.csv
#r "../dotnet/Csip.Common/bin/Debug/net7.0/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/net7.0/Csip.Engine.dll"
#r "nuget:CsvHelper, 30.0.1"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Helpers;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;
using System.Net.Http;


var engine = new FlexCroppingLocationToCsipLocation(
    new CsvHandler(),
    new WweSoilParamsV2_0(new HttpClient()),
    new PointToPolygonConverter(),
    new CokeyChooser());

string inputFile = Args[0];
string outputFile;

if(Args.Count > 1)
{
    string outputFile = Args[1];
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

await engine.Run(inputFile, outputFile);