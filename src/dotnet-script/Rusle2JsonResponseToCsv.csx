/// Command line arguments
/// Args[0]: Full path to folder containing responses to rusle2 scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_rusle2
/// Args[1]: Full path to output file, including file name; e.g. C:\csip\run_20200110\working\erosion-parameters.csv

#r "../dotnet/Csip.Common/bin/Debug/netstandard2.1/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/netstandard2.1/Csip.Engine.dll"
#r "nuget:CsvHelper, 12.0.0"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;

var engine = new Rusle2JsonResponseToCsv(
    new JsonHandler(),
    new Rusle2V5_0(),
    new CsvHandler());

string inputRusle2;

string outputFile;

if(Args.Count == 2)
{
    inputRusle2 = Args[0];
    outputFile = Args[1];
}
else if(Args.Count == 0)
{
    string cwd = Directory.GetCurrentDirectory();

    inputRusle2 = Path.Combine(
        cwd, "working", "responses_rusle2"
    );

    outputFile = Path.Combine(
        cwd, "working", "rusle2-results.csv"
    );
} else {
    throw new Exception("Must specify either 0 or 2 arguments");
}

bool v = engine.Run(inputRusle2, outputFile);
