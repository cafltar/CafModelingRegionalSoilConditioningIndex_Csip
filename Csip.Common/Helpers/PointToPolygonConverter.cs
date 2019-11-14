using System;

namespace Caf.Projects.CafModelingRegionalSoilConditioningIndex.Csip.Common.Helpers
{
    public class PointToPolygonConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude">Latidude, in decimal degrees, of centroid</param>
        /// <param name="longitude">Longitude, in decimal degrees, of centroid</param>
        /// <param name="pixelSize">Length, in km, of side of pixel (assuming a square)</param>
        /// <returns></returns>
        public string GetPixelAsBoundingBoxString(
            double latitude,
            double longitude,
            double pixelSize)
        {
            if (latitude < -90.0 || latitude > 90.0)
                throw new ArgumentOutOfRangeException("Latitude must be within -90 and 90");
            if (longitude < -180.0 || longitude > 180.0)
                throw new ArgumentOutOfRangeException("Longigude must be within -180 and 180");
            if (pixelSize <= 0)
                throw new ArgumentOutOfRangeException("Pixel size must be greater than 0");

            double half_side = pixelSize / 2;

            double lat = DegreeToRadians(latitude);
            double lon = DegreeToRadians(longitude);

            double radius = 6371;
            double parallel_radius = radius * Math.Cos(lat);

            double lat_min = lat - half_side / radius;
            double lat_max = lat + half_side / radius;
            double lon_min = lon - half_side / parallel_radius;
            double lon_max = lon + half_side / parallel_radius;

            string lon_min_lat_min = FormatPointString(lon_min, lat_min);
            string lon_min_lat_max = FormatPointString(lon_min, lat_max);
            string lon_max_lat_max = FormatPointString(lon_max, lat_max);
            string lon_max_lat_min = FormatPointString(lon_max, lat_min);

            string polygon = $"[{lon_min_lat_min},{lon_min_lat_max},{lon_max_lat_max},{lon_max_lat_min},{lon_min_lat_min}]";

            return polygon;
        }

        // From: https://www.geodatasource.com/developers/c-sharp
        private double DegreeToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double RadiansToDegree(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        private string FormatPointString(
            double lon_rad,
            double lat_rad)
        {
            string result = $"[{RadiansToDegree(lon_rad)},{RadiansToDegree(lat_rad)}]";
            
            return result;
        }
    }
}
