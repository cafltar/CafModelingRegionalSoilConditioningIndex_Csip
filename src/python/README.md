# Readme

## Setup

run-scenario.py requires a set of "secrets_{csip service}.ini" file (e.g. "secrets_sci.ini") in the following format:

```ini
[parameters]
scenarioUrl = https://path/file.zip
csipServiceUrl = http://csip.engr.colostate.edu:8086/csip-ps/p/pubsub/8086/csip-sq/m/sci/2.1
workingDir = scenarios_sci

[secrets]
webHookUrl = https://path-to-webhook
csipToken = 1227
```
