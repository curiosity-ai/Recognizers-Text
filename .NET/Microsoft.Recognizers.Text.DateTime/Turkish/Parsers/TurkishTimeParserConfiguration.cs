﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Turkish;
using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Text.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Turkish
{
    public class TurkishTimeParserConfiguration : BaseDateTimeOptionsConfiguration, ITimeParserConfiguration
    {
        private const RegexOptions RegexFlags = RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex TimeSuffixFull =
            RegexCache.Get(DateTimeDefinitions.TimeSuffixFull, RegexFlags);

        private static readonly Regex LunchRegex =
            RegexCache.Get(DateTimeDefinitions.LunchRegex, RegexFlags);

        private static readonly Regex NightRegex =
            RegexCache.Get(DateTimeDefinitions.NightRegex, RegexFlags);

        private static readonly Regex HalfTokenRegex =
            RegexCache.Get(DateTimeDefinitions.HalfTokenRegex, RegexFlags);

        private static readonly Regex QuarterTokenRegex =
            RegexCache.Get(DateTimeDefinitions.QuarterTokenRegex, RegexFlags);

        private static readonly Regex ToTokenRegex =
            RegexCache.Get(DateTimeDefinitions.ToTokenRegex, RegexFlags);

        public TurkishTimeParserConfiguration(ICommonDateTimeParserConfiguration config)
         : base(config)
        {
            TimeTokenPrefix = DateTimeDefinitions.TimeTokenPrefix;
            AtRegex = TurkishTimeExtractorConfiguration.AtRegex;
            TimeRegexes = TurkishTimeExtractorConfiguration.TimeRegexList;
            UtilityConfiguration = config.UtilityConfiguration;
            Numbers = config.Numbers;
            TimeZoneParser = config.TimeZoneParser;
        }

        public string TimeTokenPrefix { get; }

        public Regex AtRegex { get; }

        public Regex MealTimeRegex { get; }

        public IEnumerable<Regex> TimeRegexes { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public IDateTimeParser TimeZoneParser { get; }

        public void AdjustByPrefix(string prefix, ref int hour, ref int min, ref bool hasMin)
        {
            int deltaMin;

            var trimedPrefix = prefix.Trim();

            if (HalfTokenRegexCache.IsMatch(trimedPrefix))
            {
                deltaMin = 30;
            }
            else if (QuarterTokenRegexCache.IsMatch(trimedPrefix))
            {
                deltaMin = 15;
            }
            else
            {
                var match = TurkishTimeExtractorConfiguration.LessThanOneHour.Match(trimedPrefix);
                var minStr = match.Groups["deltamin"].Value;
                if (!string.IsNullOrWhiteSpace(minStr))
                {
                    deltaMin = int.Parse(minStr);
                }
                else
                {
                    minStr = match.Groups["deltaminnum"].Value;
                    if (!string.IsNullOrWhiteSpace(minStr))
                    {
                        deltaMin = Numbers[minStr];
                    }
                    else
                    {
                        return;
                    }

                }
            }

            if (ToTokenRegexCache.IsMatch(trimedPrefix))
            {
                deltaMin = -deltaMin;
            }

            min += deltaMin;
            if (min < 0)
            {
                min += 60;
                hour -= 1;
            }

            hasMin = true;
        }

        public void AdjustBySuffix(string suffix, ref int hour, ref int min, ref bool hasMin, ref bool hasAm, ref bool hasPm)
        {
            var deltaHour = 0;
            var match = TimeSuffixFull.MatchExact(suffix, trim: true);

            if (match.Success)
            {
                var matchAmStr = match.Groups[Constants.AmGroupName].Value;
                if (!string.IsNullOrEmpty(matchAmStr))
                {
                    if (hour >= Constants.HalfDayHourCount)
                    {
                        deltaHour = -Constants.HalfDayHourCount;
                    }
                    else
                    {
                        hasAm = true;
                    }
                }

                var matchPmStr = match.Groups[Constants.PmGroupName].Value;
                if (!string.IsNullOrEmpty(matchPmStr))
                {
                    if (hour < Constants.HalfDayHourCount)
                    {
                        deltaHour = Constants.HalfDayHourCount;
                    }

                    if (LunchRegexCache.IsMatch(matchPmStr))
                    {
                        if (hour >= 10 && hour <= Constants.HalfDayHourCount)
                        {
                            deltaHour = 0;
                            if (hour == Constants.HalfDayHourCount)
                            {
                                hasPm = true;
                            }
                            else
                            {
                                hasAm = true;
                            }
                        }
                        else
                        {
                            hasPm = true;
                        }
                    }
                    else if (NightRegexCache.IsMatch(matchPmStr))
                    {
                        if (hour <= 3 || hour == Constants.HalfDayHourCount)
                        {
                            if (hour == Constants.HalfDayHourCount)
                            {
                                hour = 0;
                            }

                            deltaHour = 0;
                            hasAm = true;
                        }
                        else
                        {
                            hasPm = true;
                        }
                    }
                    else
                    {
                        hasPm = true;
                    }
                }
            }

            hour = (hour + deltaHour) % 24;
        }
    }
}
