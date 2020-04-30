/// Command line arguments (optional)
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

string csipLocationsPath;
string erosionParametersPath;
string csipResponsesPath;
string outputFile ;

string currentDate = DateTime.Now.ToString("yyyyMMdd");

if(Args.Count == 4)
{
    csipLocationsPath = Args[0];
    erosionParametersPath = Args[1];
    csipResponsesPath = Args[2];
    outputFile = Args[3];
} 
else if(Args.Count == 0)
{
    string cwd = Directory.GetCurrentDirectory();

    csipLocationsPath = Path.Combine(
        cwd, "working", "csip-locations.csv"
    );

    erosionParametersPath = Path.Combine(
        cwd, "working", "erosion-parameters.csv"
    );

    csipResponsesPath = Path.Combine(
        cwd, "working", "responses_sci", "sci_rusle2"
    );

    outputFile = Path.Combine(
        cwd, "output", $"experimental-results-rusle2_{currentDate}.csv"
    );
}else {
    throw new Exception("Must specify either 0 or 4 arguments");
}

if(!Directory.Exists(Path.GetDirectoryName(outputFile)))
{
    Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
}


var engine = new SciJsonResponseToExperimentalResults(
    new JsonHandler(),
    new SciV2_1(),
    new CsvHandler());

engine.Run(
    csipLocationsPath, 
    erosionParametersPath, 
    csipResponsesPath, 
    outputFile);