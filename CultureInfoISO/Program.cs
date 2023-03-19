using System;
using System.Globalization;

namespace CultureInfoISO
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Obter informações de cada país da ISO
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo region = new RegionInfo(culture.Name);
                string countryName = region.EnglishName;
                string countryCode = region.TwoLetterISORegionName;
                string name = region.Name;
                string nativeName = region.NativeName;
                string geoID = region.GeoId.ToString();
                string currencyISO = region.ISOCurrencySymbol;
                string cuurencyName = region.CurrencyEnglishName;


                Console.WriteLine(countryName, name);
                Console.WriteLine(countryCode, nativeName);
                Console.WriteLine(geoID);
                Console.WriteLine(currencyISO);
                Console.WriteLine(cuurencyName);
            }
            
        }
    }
}
