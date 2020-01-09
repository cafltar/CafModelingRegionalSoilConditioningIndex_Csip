# Script based on modified version of Publish/Subscribe Jupyter Notebook by CSIP: https://colab.research.google.com/drive/1hfm0BhQMGGLudlQhJnvyhw73ciqXW6SW#scrollTo=Sq__FsvrKfuo
import os, sys
import wget # conda install -c anaconda pywget
from pathlib import Path
from zipfile import ZipFile
#pip install csip --no-cache-dir --upgrade
from csip.utils import Client
import csip
import configparser

# scenario_url: string to zip file containing scenario files. Expects zip file name to be {model type}_{creation date}.zip and contain files of format: scenario*.json
# out_dir: string for directory name where scenario zip file will be downloaded and extracted
def download_extract_scenarios(scenario_url, out_dir):
    # Populate payloads
    Path(out_dir).mkdir(parents=True, exist_ok=True)

    filename = wget.download(scenario_url, out = out_dir)

    #!unzip -u wepp_20191113.zip
    with ZipFile(filename, 'r') as zipObj:
        zipObj.extractall(out_dir)

def run_scenarios(csip_ps_url, webhook_url, bearer_token, scenario_dir):
    # Get the catalog
    catalog = Client.get_catalog('http://csip.engr.colostate.edu:8086/csip-ps/')

    # Print the services
    for service in catalog:
        print(service)

    # Create the client
    # we'll reuse the object from the catalog, or do 'ps = Client()'
    ps = catalog[0]

    # set the bearertoken (this is an invalid token for test puposes)
    ps.bearertoken = bearer_token

    # set the webhook to subscribe for results
    ps.webhook = webhook_url

    # set the batch to run by pointing to all scenario payloads 
    ps.batch = os.path.join(scenario_dir, "scenario*.json")

    # Now P/S can be invoked to publish the requests. The pubsub method in the Client class will take the P/S URL as argument and an optional callback function.
    ps.pubsub(csip_ps_url, callback=cb)

# callback
# The callback function receives the Client instance that was received after publication which allows to verify the 'Queued' status indicating that the publish operation was successful. As a second argument the callback receives the request file name.
def cb(client, file):
    print(file + ": " + client.get_status())
    sys.stdout.flush()

if __name__ == "__main__":
    config = configparser.RawConfigParser()
    config.read('secrets.ini')
    download_extract_scenarios(config['parameters']['scenarioUrl'], 'scenarios')
    run_scenarios(
        'http://csip.engr.colostate.edu:8086/csip-ps/p/pubsub/8086/csip-weps/m/weps/5.2', 
        config['secrets']['webHookUrl'], 
        config['secrets']['csipToken'], 
        'scenarios')