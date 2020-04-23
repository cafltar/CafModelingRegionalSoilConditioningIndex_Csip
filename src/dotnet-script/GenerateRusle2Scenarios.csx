/// Command line arguments (optional)
/// Args[0]: Full path to csv containing CsipLocations; e.g. C:\csip\run_20200110\working\csip-locations.csv
/// Args[1]: Full path to output folder; e.g. C:\csip\run_20200110\working\scenarios_wepp
#r "../dotnet/Csip.Common/bin/Debug/netstandard2.1/Csip.Common.dll"
#r "../dotnet/Csip.Scenario/bin/Debug/netstandard2.1/Csip.Scenario.dll"
#r "nuget:CsvHelper, 12.0.0"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

CsvHandler reader = new CsvHandler();
IBuildErosionModel builder = new Rusle2Builder();
ScenarioHandler writer = new ScenarioHandler();

string currentDate = DateTime.Now.ToString("yyyyMMdd");

string inputFile;
string expectedZip;
string writePath;

if(Args.Count == 2)
{
    writePath = $"{Args[1]}\\rusle2_{currentDate}";
} 
else if(Args.Count == 0)
{
    string cwd = Directory.GetCurrentDirectory();

    inputFile = Path.Combine(
        cwd, "working", "csip-locations.csv");

    writePath = Path.Combine(
        cwd,
        "working",
        "scenarios_rusle2",
        $"rusle2_{currentDate}"
    );
} else {
    throw new Exception("Must specify either 0 or 2 arguments");
}

if(!Directory.Exists(writePath))
{
    Directory.CreateDirectory(writePath);
}

Console.WriteLine($"Generating RUSLE2 scenarios from {inputFile} to {writePath} and {expectedZip}");

List<string> scenarios = builder.BuildScenarios(
    reader.ReadCsipLocationFile(inputFile),
    builder.GetTemplate(),
    builder.GetRotations());

writer.WriteScenariosZip(scenarios, writePath);