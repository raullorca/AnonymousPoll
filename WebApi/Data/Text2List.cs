using System.IO;

namespace WebApi.Data
{
    public abstract class Text2List
    {
        protected string SetGender(string[] item, int position)
        {
            var value = item[position];
            if (!"MF".Contains(value.ToUpper()))
            {
                GenerateThrow($"Current value {value} but only accept value M or F");
            }
            return value;
        }

        protected string SetString(string[] item, int position)
        {
            var value = item[position];
            ValidateString(value);
            return value;
        }

        protected int SetNumeric(string[] item, int position)
        {
            var value = item[position];
            ValidateNumeric(value);
            return int.Parse(value);
        }



        protected void ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                GenerateThrow("Value required, current value is empty");
            }
            if (IsNumeric(value))
            {
                GenerateThrow("Value required, current value is a number");
            }
        }

        protected void ValidateNumeric(string value)
        {
            if (!IsNumeric(value))
            {
                GenerateThrow("Value isn't number");
            }
            int numeric = int.Parse(value);
            if (numeric <= 0)
            {
                GenerateThrow("Value must be greater than zero");
            }
        }

        protected void GenerateThrow(string message)
        {
            throw new FileLoadException(message);
        }

        protected bool IsNumeric(string value)
        {
            return int.TryParse(value, out int n);
        }
    }
}