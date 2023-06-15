/// Command line arguments (optional)
/// Args[0]: Full path to csv containing CsipLocations; e.g. C:\csip\run_20200110\working\csip-locations.csv
/// Args[1]: Full path to output folder; e.g. C:\csip\run_20200110\working\scenarios_wepp
#r "../dotnet/Csip.Common/bin/Debug/net7.0/Csip.Common.dll"
#r "../dotnet/Csip.Scenario/bin/Debug/net7.0/Csip.Scenario.dll"
#r "nuget:CsvHelper, 30.0.1"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Scenario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

CsvHandler reader = new CsvHandler();
IBuildErosionModel builder = new WeppBuilder();
ScenarioHandler writer = new ScenarioHandler();
string currentDate = DateTime.Now.ToString("yyyyMMdd");

string inputFile;
string expectedZip;
string writePath;

if(Args.Count == 2)
{
    inputFile = Args[0];
    writePath = $"{Args[1]}\\wepp_{currentDate}";
} 
else if(Args.Count == 0)
{
    string cwd = Directory.GetCurrentDirectory();

    inputFile = Path.Combine(
        cwd, "working", "csip-locations.csv");

    writePath = Path.Combine(
        cwd,
        "working",
        "scenarios_wepp",
        $"wepp_{currentDate}"
    );
} else {
    throw new Exception("Must specify either 0 or 2 arguments");
}

if(!Directory.Exists(writePath))
{
    Directory.CreateDirectory(writePath);
}

Console.WriteLine($"Generating WEPP scenarios from {inputFile} to {writePath}");

List<string> scenarios = builder.BuildScenarios(
    reader.ReadCsipLocationFile(inputFile),
    builder.GetTemplate(),
    builder.GetRotations());

writer.WriteScenariosZip(scenarios, writePath);