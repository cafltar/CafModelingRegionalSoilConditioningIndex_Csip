/// Command line arguments
/// Args[0]: Full path to file containing csip locations; e.g. C:\csip\run_20200110\working\csip-locations.csv
/// Args[1]: Full path to file containing erosion parameters; e.g. C:\csip\run_20200110\working\erosion-parameters.csv
/// Args[2]: Full path to folder containing responses to sci scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_sci
/// Args[3]: Full path to output file for experimental results, including file name and extension; e.g. C:\csip\run_20200110\output\experimental-results.csv

#r "../dotnet/Csip.Common/bin/Debug/netstandard2.1/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/netstandard2.1/Csip.Engine.dll"
#r "nuget:CsvHelper, 12.0.0"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;

var engine = new SciJsonResponseToExperimentalResults(
    new JsonHandler(),
    new SciV2_1(),
    new CsvHandler());
string csipLocationsPath = Args[0];
string erosionParametersPath = Args[1];
string csipResponsesPath = Args[2];
string outputFile = Args[3];

engine.Run(
    csipLocationsPath, 
    erosionParametersPath, 
    csipResponsesPath, 
    outputFile);