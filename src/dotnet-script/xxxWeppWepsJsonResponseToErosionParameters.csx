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

string inputWepp;
string inputWeps;
string outputFile;

if(Args.Count == 3)
{
    inputWepp = Args[0];
    inputWeps = Args[1];
    outputFile = Args[2];
}
else if(Args.Count == 0)
{
    string cwd = Directory.GetCurrentDirectory();

    inputWepp = Path.Combine(
        cwd, "working", "responses_wepp"
    );

    inputWeps = Path.Combine(
        cwd, "working", "responses_weps"
    );

    outputFile = Path.Combine(
        cwd, "working", "erosion-parameters.csv"
    );
} else {
    throw new Exception("Must specify either 0 or 3 arguments");
}

engine.Run(inputWepp, inputWeps, outputFile);
