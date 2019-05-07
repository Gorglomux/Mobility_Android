using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

public class CodeParser
{
    private decimal lbToKgRatio = 0.45m;
    private Dictionary<string, GroupDefinition> patterns;

    public CodeParser()
    {
        patterns = new Dictionary<string, GroupDefinition>();
        patterns.Add("01", new GroupDefinition(14, 14, GroupDefinitionField.codeSSCC));
        patterns.Add("13", new GroupDefinition(6, 6, GroupDefinitionField.packingDate));
        patterns.Add("11", new GroupDefinition(6, 6, GroupDefinitionField.productionDate));
        patterns.Add("15", new GroupDefinition(6, 6, GroupDefinitionField.ExpirationDate));
        patterns.Add("16", new GroupDefinition(6, 6, GroupDefinitionField.SaleDate));
        patterns.Add("31", new GroupDefinition(8, 8, GroupDefinitionField.weightKG));
        patterns.Add("32", new GroupDefinition(8, 8, GroupDefinitionField.weightLb));
        patterns.Add("37", new GroupDefinition(8, 8, GroupDefinitionField.Count));
        patterns.Add("21", new GroupDefinition(1, 20, GroupDefinitionField.palletCode));
    }

    public ParsedLicence getLicense(string input)
    {
        ParsedLicence parsedLicense = new ParsedLicence();
        CultureInfo provider = CultureInfo.InvariantCulture;
        string matchedGroup = string.Empty;

        if (Regex.IsMatch(input, "^(01).*"))
        {
            bool groupParsed;
            while (input.Length > 0)
            {
                groupParsed = false;
                foreach (KeyValuePair<string, GroupDefinition> pair in patterns)
                {
                    string currentPattern = "^" + pair.Key + "(.{" + pair.Value.minLength.ToString() + "," + pair.Value.maxLength.ToString() + "})";

                    if (Regex.IsMatch(input, currentPattern))
                    {
                        matchedGroup = Regex.Split(input, currentPattern)[1];

                        switch (pair.Value.field)
                        {
                            case GroupDefinitionField.codeSSCC:
                                {
                                    parsedLicense.codeSSCC = matchedGroup;
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.packingDate:
                                {
                                    parsedLicense.packingDate = DateTime.ParseExact(matchedGroup, "yyMMdd", provider);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.productionDate:
                                {
                                    parsedLicense.productionDate = DateTime.ParseExact(matchedGroup, "yyMMdd", provider);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.ExpirationDate:
                                {
                                    parsedLicense.ExpirationDate = DateTime.ParseExact(matchedGroup, "yyMMdd", provider);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.SaleDate:
                                {
                                    parsedLicense.SaleDate = DateTime.ParseExact(matchedGroup, "yyMMdd", provider);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.weightKG:
                                {
                                    parsedLicense.weightKG = parseWeight(matchedGroup);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.weightLb:
                                {
                                    parsedLicense.weightLb = parseWeight(matchedGroup);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.Count:
                                {
                                    parsedLicense.Count = Int32.Parse(matchedGroup);
                                    groupParsed = true;
                                    break;
                                }

                            case GroupDefinitionField.palletCode:
                                {
                                    parsedLicense.palletCode = matchedGroup;
                                    groupParsed = true;
                                    break;
                                }

                            default:
                                {
                                    //throw new InvalidStringFormatException(input.Substring(0, 2));
                                    break;
                                }
                        }

                        input = Regex.Split(input, currentPattern)[2];
                    }
                }
                if (!groupParsed)
                {
                    //throw new InvalidStringFormatException(input.Substring(0, 2));
                }
            }
        }
        else
        {
            //throw new InvalidStringFormatException(input.Substring(0, 2));
        }
            
        if (parsedLicense.weightKG == 0 & parsedLicense.weightLb != 0)
            parsedLicense.weightKG = parsedLicense.weightLb * lbToKgRatio;

        return parsedLicense;
    }
    private decimal parseWeight(string matchedGroup)
    {
        decimal value = 0;
        string decimalIndicator = matchedGroup.Substring(0, 2);

        matchedGroup = matchedGroup.Substring(2);
        matchedGroup = matchedGroup.Insert((matchedGroup.Length - Int32.Parse(decimalIndicator)), NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);

        value = decimal.Parse(matchedGroup, NumberStyles.AllowDecimalPoint);

        return value;
    }
    private enum GroupDefinitionField
    {
        codeSSCC,
        weightKG,
        weightLb,
        packingDate,
        productionDate,
        ExpirationDate,
        SaleDate,
        palletCode,
        Count
    }
    public class ParsedLicence
    {
        private string _codeSSCC;
        private decimal _weightKG;
        private decimal _weightLb;
        private DateTime? _packingDate;
        private DateTime? _productionDate;
        private DateTime? _ExpirationDate;
        private DateTime? _SaleDate;
        private string _palletCode;
        private int _Count;

        public string codeSSCC
        {
            get
            {
                return _codeSSCC;
            }
            set
            {
                _codeSSCC = value;
            }
        }
        public decimal weightKG
        {
            get
            {
                return _weightKG;
            }
            set
            {
                _weightKG = value;
            }
        }
        public decimal weightLb
        {
            get
            {
                return _weightLb;
            }
            set
            {
                _weightLb = value;
            }
        }
        public DateTime? packingDate
        {
            get
            {
                return _packingDate;
            }
            set
            {
                _packingDate = value;
            }
        }
        public DateTime? productionDate
        {
            get
            {
                return _productionDate;
            }
            set
            {
                _productionDate = value;
            }
        }
        public DateTime? ExpirationDate
        {
            get
            {
                return _ExpirationDate;
            }
            set
            {
                _ExpirationDate = value;
            }
        }
        public DateTime? SaleDate
        {
            get
            {
                return _SaleDate;
            }
            set
            {
                _SaleDate = value;
            }
        }
        public string palletCode
        {
            get
            {
                return _palletCode;
            }
            set
            {
                _palletCode = value;
            }
        }
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
            }
        }
    }
    private class GroupDefinition
    {
        public int minLength;
        public int maxLength;
        public GroupDefinitionField field;

        public GroupDefinition(int minLength, int maxLength, GroupDefinitionField field)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
            this.field = field;
        }
    }
}
