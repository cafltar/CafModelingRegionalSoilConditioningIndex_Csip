/// Command line arguments
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
IBuildErosionModel builder = new WeppBuilder();
ScenarioHandler writer = new ScenarioHandler();
string currentDate = DateTime.Now.ToString("yyyyMMdd");
string expectedZip = $"{Args[1]}\\wepp_{currentDate}.zip";
string writePath = $"{Args[1]}\\wepp_{currentDate}";

List<string> actual = builder.BuildScenarios(
    reader.ReadCsipLocationFile(Args[0]),
    builder.GetTemplate(),
    builder.GetRotations());

writer.WriteScenariosZip(actual, writePath);