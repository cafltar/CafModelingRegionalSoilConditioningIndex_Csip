# Files

* [Csip.Common](Csip.Common)
  * .NET Standard 2.1 class library
  * Contains utility classes for business logic, classes for file input/output, model classes for CSIP scenarios and file outputs, and service classes to handle output files from CSIP services
* [Csip.Common.Tests](Csip.Common.Tests)
  * .NET Core 3.0 console application
  * Contains unit tests for [Csip.Common](Csip.Common) library
* [Csip.Engine](Csip.Engine)
  * .NET Standard 2.1 class library
  * Contains classes to orchestrate data processing between [Csip.Scenario](Csip.Scenario) classes and [Csip.Common](Csip.Common) classes.
* [Csip.IntegrationTests](Csip.IntegrationTests)
  * .NET Core 3.0 console application
  * Integration tests to test classes defined throughout the project. Can hit outside servers as well.
* [Csip.Scenario](Csip.Scenario)
  * .NET Standard 2.1 class library
  * Contains classes that build CSIP scenarios in JSON string format that are used in a HTTP POST body
* [Csip.Scenario.Tests](Csip.Scenario.Tests)
  * .NET Core 3.0 console application
  * Contains unit tests for [Csip.Scenario](Csip.Scenario) library