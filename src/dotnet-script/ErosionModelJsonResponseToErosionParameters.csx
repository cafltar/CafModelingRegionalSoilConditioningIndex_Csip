/// Command line arguments
/// Args[0]: Full path to folder containing responses to wepp scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_wepp
/// Args[1]: Full path to folder containing responses to weps scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_weps
/// Args[2]: Full path to folder containing responses to rusle2 scenarios, in form of json files; e.g. C:\csip\run_20200110\working\responses_rusle2
/// Args[3]: Full path to output file, including file name; e.g. C:\csip\run_20200110\working\erosion-parameters.csv

#r "../dotnet/Csip.Common/bin/Debug/net7.0/Csip.Common.dll"
#r "../dotnet/Csip.Engine/bin/Debug/net7.0/Csip.Engine.dll"
#r "nuget:CsvHelper, 30.0.1"

using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Services;
using Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Engine;
using System.IO;

var engine = new ErosionModelJsonResponseToErosionParameters(
    new JsonHandler(),
    new WeppV3_1(),
    new WepsV5_2(),
	new Rusle2V5_0(),
    new CsvHandler());

string inputWepp;
string inputWeps;
string inputRusle2;
string outputFile;

if(Args.Count == 4)
{
    inputWepp = Args[0];
    inputWeps = Args[1];
	inputRusle2 = Args[2];
    outputFile = Args[3];
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
	
	inputRusle2 = Path.Combine(
		cwd, "working", "responses_rusle2"
	);

    outputFile = Path.Combine(
        cwd, "working", "erosion-parameters.csv"
    );
} else {
    throw new Exception("Must specify either 0 or 3 arguments");
}

engine.Run(inputWepp, inputWeps, inputRusle2, outputFile);
