using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.IO
{
    public class ScenarioHandler
    {
        public void WriteScenariosZip(
            List<string> scenarios, 
            string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            int count = 0;
            foreach (string scenario in scenarios)
            {
                string fileName =
                    $"scenario{count.ToString().PadLeft(4, '0')}.json";
                string path = Path.Combine(dirPath, fileName);
                File.WriteAllText(path, scenario);

                count++;
            }

            string zipPath = Path.Combine(
                Directory.GetParent(dirPath).FullName,
                $"{Path.GetFileNameWithoutExtension(dirPath)}.zip");

            ZipFile.CreateFromDirectory(dirPath, zipPath);
        }
    }
}
