using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Models.Json
{
    public class WweSoilParamsV2Results
    {
        public MapUnitMeta MapUnitMeta { get; set; }
        public List<MapUnit> MapUnits { get; set; }
    }

    public class MapUnitMeta
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Area { get; set; }

        [JsonPropertyName("description")]
        public string Units { get; set; }
    }
    public class MapUnit
    {
        // A non-connotative string of characters used to uniquely identify a record in the Mapunit table
        public string Mukey { get; set; }

        // Correlated name of the mapunit (recommended name or field name for surveys in progress)
        public string Name { get; set; }

        // Map unit intersection area with the AoI
        public double Area { get; set; }


        public List<Component> Components {get; set;}
    }

    public class Component
    {
        // A non-connotative string of characters used to uniquely identify a record in the Component table
        public string Cokey { get; set; }

        // Name assigned to a component based on its range of properties
        public string Name { get; set; }

        // Slope RV: The difference in elevation between two points, expressed as a percentage of the distance between those points. (SSM)
        public double Slope { get; set; }

        // Calculated slope length (in feet)
        public double SlopeLength { get; set; }

        // The percentage of the component of the mapunit
        public double PercentOfMapUnit { get; set; }
    }

}
