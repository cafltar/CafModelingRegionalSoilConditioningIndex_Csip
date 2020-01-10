/// Command line arguments
/// Args[0]: Full path to folder containing responses to wepp scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_wepp
/// Args[1]: Full path to folder containing responses to weps scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_weps
/// Args[2]: Full path to output file, including file name; e.g. C:\csip\run_20200110\working\erosion-parameters.csv

#r "../dotnet/Csip.Common/bin/Debug/netstandard2.1/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/netstandard2.1/Csip.Engine.dll"
#r "nuget:CsvHelper, 12.0.0"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;

var engine = new WeppWepsJsonResponseToErosionParameters(
    new JsonHandler(),
    new WeppV3_1(),
    new WepsV5_2(),
    new CsvHandler());
string inputWepp = Args[0];
string inputWeps = Args[1];
string outputFile = Args[2];

engine.Run(inputWepp, inputWeps, outputFile);