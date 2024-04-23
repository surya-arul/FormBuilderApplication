using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.Utilities
{
    public class HtmlHelper
    {
        public static readonly List<KeyValuePair<HtmlType, string>> HtmlTypeDropdownList =
        [
            new KeyValuePair<HtmlType, string>(HtmlType.Text, "Text"),
            new KeyValuePair<HtmlType, string>(HtmlType.Number, "Number"),
            new KeyValuePair<HtmlType, string>(HtmlType.Date, "Date"),
            new KeyValuePair<HtmlType, string>(HtmlType.Email, "Email"),
            new KeyValuePair<HtmlType, string>(HtmlType.File, "File"),
            new KeyValuePair<HtmlType, string>(HtmlType.CheckBox, "CheckBox"),
            new KeyValuePair<HtmlType, string>(HtmlType.RadioButton, "RadioButton"),
            new KeyValuePair<HtmlType, string>(HtmlType.Textarea, "Textarea"),
            new KeyValuePair<HtmlType, string>(HtmlType.Select, "Select"),
            new KeyValuePair<HtmlType, string>(HtmlType.Button, "Button"),
            new KeyValuePair<HtmlType, string>(HtmlType.SubmitButton, "SubmitButton"),
        ];

        public static string GenerateForm(List<InputsDto> inputs)
        {
            string htmlTagWithForm = $"<form>";

            if (inputs == null)
            {
                return htmlTagWithForm;
            }

            foreach (var item in inputs)
            {
                if (!Enum.TryParse(item.InputType, true, out HtmlType inputType))
                {
                    throw new Exception($"{item.InputType} - {item.InternalName} is not a valid Html type");
                }

                string inputTag = GenerateHtmlTag(inputType, item);

                htmlTagWithForm += $"{inputTag}";
            }

            htmlTagWithForm += $"</form>";

            return htmlTagWithForm;
        }

        private static string GenerateLabel(InputsDto inputs)
        {
            string labelTag = $"<label";
            labelTag += string.IsNullOrEmpty(inputs.LabelClassName) ? string.Empty : $" class=\"{inputs.LabelClassName}\"";
            labelTag += $">{inputs.Label}</label>";

            return labelTag;
        }

        private static string GenerateHtmlTag(HtmlType inputType, InputsDto inputs)
        {
            string htmlTag = $"<div";

            htmlTag += string.IsNullOrEmpty(inputs.DivClassName) ? string.Empty : $" class=\"{inputs.DivClassName}\"";

            htmlTag += $">";

            switch (inputType)
            {
                case HtmlType.Text:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"text\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Number:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"number\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Date:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"date\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Email:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"email\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.File:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"file\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += " />";
                    break;
                case HtmlType.CheckBox:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : "";
                            htmlTag += $"<input type=\"checkbox\" name=\"{item}\" value=\"{item}\" class=\"{inputs.InputClassName}\" {requiredAttribute} />";
                            htmlTag += $"<label for=\"{item}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlType.RadioButton:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : "";
                            htmlTag += $"<input type=\"radio\" id=\"{item}\" name=\"{inputs.Label}\" value=\"{item}\" class=\"{inputs.InputClassName}\" {requiredAttribute} />";
                            htmlTag += $"<label for=\"{item}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlType.Textarea:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<textarea";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += $">{(string.IsNullOrEmpty(inputs.Value) ? string.Empty : inputs.Value)}";
                    htmlTag += "</textarea>";
                    break;
                case HtmlType.Select:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<select";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += $">";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            htmlTag += $"<option>{item}</option>";
                        }
                    }
                    htmlTag += "</select>";
                    break;
                case HtmlType.Button:
                    htmlTag += $"<input type=\"button\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.SubmitButton:
                    htmlTag += $"<input type=\"submit\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                default:
                    return string.Empty;
            }

            htmlTag += $"</div>";

            return htmlTag;
        }
    }
}