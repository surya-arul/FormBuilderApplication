namespace FormBuilderMVC.Utilities
{
    public class StringHelper
    {
        public static List<string> StringToList(string stringInput, List<string>? listInput)
        {
            var stringSplitList = stringInput.Split(',')
                                          .Where(x => !string.IsNullOrWhiteSpace(x))
                                          .Select(x => x.Trim()).ToList();

            return stringSplitList.Count is not 0 ? stringSplitList : listInput ?? [];
        }
    }
}