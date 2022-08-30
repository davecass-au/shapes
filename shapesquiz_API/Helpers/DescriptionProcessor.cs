using shapesquiz_API.Enums;

namespace shapesquiz_API.Helpers
{
   public class DescriptionProcessor
   {
      private List<string> WordsList { get; set; } 
      private Dictionary<ShapeProperty, string> PropertyNames { get; set; }

      private Dictionary<ShapeProperty, int> PropertyValues { get; set; }


      public DescriptionProcessor(List<string> wWordList, List<ShapeProperty> wProperties)
      {
         WordsList = wWordList;
         PropertyNames = wProperties.ToDictionary(x => x, y => EnumHelpers.NameByShapeProperty(y)); 
         PropertyValues = GetValues();
      }

      public int GetValueByProperty(ShapeProperty wProperty)
      {
         return PropertyValues.TryGetValue(wProperty, out var value) ? value : 0;
      }

      // Could make this generic to handle other types such as decimals
      private Dictionary<ShapeProperty, int> GetValues()
      {
         Dictionary<ShapeProperty, int> pReturn = new();
         ShapeProperty pFoundProperty = ShapeProperty.Unknown;

         // Start looping
         foreach (string curWord in WordsList)
         {
            // Try and parse the word to int
            if (int.TryParse(curWord, out int propValue))
            {
               // We have a numeric value. Make sure we have a property flagged.
               if (pFoundProperty != ShapeProperty.Unknown)
               {
                  // Add it to the return dictionary
                  pReturn.Add(pFoundProperty, propValue);

                  // Make sure we reset the property flag
                  pFoundProperty = ShapeProperty.Unknown;
               }               
            }
            else
            {
               // We have a string. Try and match it to an expected property otherwise will be unknown.
               pFoundProperty = PropertyNames.FirstOrDefault(x => string.Equals(x.Value, curWord)).Key;
            }
         }

         return pReturn;
      }
   }
}
