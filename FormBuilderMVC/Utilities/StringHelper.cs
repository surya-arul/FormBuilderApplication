using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Utilities
{
    public static class StringHelper
    {
        public static List<string> StringToList(string stringInput, List<string>? listInput)
        {
            var stringSplitList = stringInput.Split(',')
                                          .Where(x => !string.IsNullOrWhiteSpace(x))
                                          .Select(x => x.Trim()).ToList();

            return stringSplitList.Count is not 0 ? stringSplitList : listInput ?? [];
        }

        public static string ConvertToLowercaseAndRemoveSpaces(string? input)
        {
            return input?.ToLower().Replace(" ", "") ?? string.Empty;
        }

        public static string ExtractControllerName(this Type controllerType)
        {
            const string controllerSuffix = "Controller";
            string typeName = controllerType.Name;

            int controllerIndex = typeName.LastIndexOf(controllerSuffix);

            if (controllerIndex >= 0)
            {
                return typeName.Substring(0, controllerIndex);
            }

            return typeName;
        }
    }
}