/// Command line arguments
/// Args[0]: Full path to csv containing ErosionParameters; e.g. C:\csip\run_20200110\working\erosion-parameters.csv
/// Args[1]: Full path to output folder; e.g. C:\csip\run_20200110\working\scenarios_sci
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
SciBuilder builder = new SciBuilder();
ScenarioHandler writer = new ScenarioHandler();
string currentDate = DateTime.Now.ToString("yyyyMMdd");

string inputFile;
string writePath;

if(Args.Count == 2)
{
    inputFile = Args[0];
    writePath = $"{Args[1]}\\sci_{currentDate}";
}
else if(Args.Count == 0)
{
    string cwd = Directory.GetCurrentDirectory();

    inputFile = Path.Combine(
        cwd, 
        "working", 
        "erosion-parameters.csv"
    );

    writePath = Path.Combine(
        cwd, 
        "working", 
        "scenarios_sci", 
        $"sci_{currentDate}"
    );
} else {
    throw new Exception("Must specify either 0 or 2 arguments");
}

if(!Directory.Exists(writePath))
{
    Directory.CreateDirectory(writePath);
}

Console.WriteLine($"Generating SCI scenarios from {inputFile} to {writePath}");

List<string> scenarios = builder.BuildScenarios(
    reader.ReadErosionParameters(inputFile),
    builder.GetTemplate());

writer.WriteScenariosZip(scenarios, writePath);