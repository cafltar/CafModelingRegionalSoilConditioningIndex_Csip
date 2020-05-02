# Files

* [dotnet](dotnet)
  * .NET solution (C#) containing class libraries to generate CSIP scenarios and to handle results
* [dotnet-script](dotnet-script)
  * dotnet script (.csx) files that call class libraries that are compiled in the [dotnet](dotnet) solution folder
* [python](python)
  * Python script used to queue scenarios to CSIP bulk run services. Scenarios generated using the script files in [dotnet-script](dotnet-script) which in turn uses libraries compiled in the [dotnet](dotnet) solution directory
