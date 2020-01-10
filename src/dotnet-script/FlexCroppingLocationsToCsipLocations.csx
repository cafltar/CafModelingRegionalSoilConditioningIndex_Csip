/// Command line arguments
/// Args[0]: Full path to csv containing FlexCropping Locations; e.g. C:\csip\run_20200110\input\flex-cropping-locations.csv
/// Args[1]: Full path to output file, including file name; e.g. C:\csip\run_20200110\working\csip-locations.csv
#r "../dotnet/Csip.Common/bin/Debug/netstandard2.1/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/netstandard2.1/Csip.Engine.dll"
#r "nuget:CsvHelper, 12.0.0"

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
string outputFile = Args[1];

await engine.Run(inputFile, outputFile);