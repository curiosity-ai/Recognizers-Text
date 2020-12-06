﻿using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.English;
using Microsoft.Recognizers.Text.DateTime.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.English
{
    public class EnglishSetParserConfiguration : BaseDateTimeOptionsConfiguration, ISetParserConfiguration
    {

        private const RegexOptions RegexFlags = RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex DoubleMultiplierRegex =
            RegexCache.Get(DateTimeDefinitions.DoubleMultiplierRegex, RegexFlags);

        private static readonly Regex HalfMultiplierRegex =
            RegexCache.Get(DateTimeDefinitions.HalfMultiplierRegex, RegexFlags);

        private static readonly Regex DayTypeRegex =
            RegexCache.Get(DateTimeDefinitions.DayTypeRegex, RegexFlags);

        private static readonly Regex WeekTypeRegex =
            RegexCache.Get(DateTimeDefinitions.WeekTypeRegex, RegexFlags);

        private static readonly Regex WeekendTypeRegex =
            RegexCache.Get(DateTimeDefinitions.WeekendTypeRegex, RegexFlags);

        private static readonly Regex MonthTypeRegex =
            RegexCache.Get(DateTimeDefinitions.MonthTypeRegex, RegexFlags);

        private static readonly Regex QuarterTypeRegex =
            RegexCache.Get(DateTimeDefinitions.QuarterTypeRegex, RegexFlags);

        private static readonly Regex YearTypeRegex =
            RegexCache.Get(DateTimeDefinitions.YearTypeRegex, RegexFlags);

        public EnglishSetParserConfiguration(ICommonDateTimeParserConfiguration config)
            : base(config)
        {
            DurationExtractor = config.DurationExtractor;
            TimeExtractor = config.TimeExtractor;
            DateExtractor = config.DateExtractor;
            DateTimeExtractor = config.DateTimeExtractor;
            DatePeriodExtractor = config.DatePeriodExtractor;
            TimePeriodExtractor = config.TimePeriodExtractor;
            DateTimePeriodExtractor = config.DateTimePeriodExtractor;

            DurationParser = config.DurationParser;
            TimeParser = config.TimeParser;
            DateParser = config.DateParser;
            DateTimeParser = config.DateTimeParser;
            DatePeriodParser = config.DatePeriodParser;
            TimePeriodParser = config.TimePeriodParser;
            DateTimePeriodParser = config.DateTimePeriodParser;
            UnitMap = config.UnitMap;

            EachPrefixRegex = EnglishSetExtractorConfiguration.EachPrefixRegex;
            PeriodicRegex = EnglishSetExtractorConfiguration.PeriodicRegex;
            EachUnitRegex = EnglishSetExtractorConfiguration.EachUnitRegex;
            EachDayRegex = EnglishSetExtractorConfiguration.EachDayRegex;
            SetWeekDayRegex = EnglishSetExtractorConfiguration.SetWeekDayRegex;
            SetEachRegex = EnglishSetExtractorConfiguration.SetEachRegex;
        }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeParser DurationParser { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeParser TimeParser { get; }

        public IDateExtractor DateExtractor { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeExtractor DateTimeExtractor { get; }

        public IDateTimeParser DateTimeParser { get; }

        public IDateTimeExtractor DatePeriodExtractor { get; }

        public IDateTimeParser DatePeriodParser { get; }

        public IDateTimeExtractor TimePeriodExtractor { get; }

        public IDateTimeParser TimePeriodParser { get; }

        public IDateTimeExtractor DateTimePeriodExtractor { get; }

        public IDateTimeParser DateTimePeriodParser { get; }

        public IImmutableDictionary<string, string> UnitMap { get; }

        public Regex EachPrefixRegex { get; }

        public Regex PeriodicRegex { get; }

        public Regex EachUnitRegex { get; }

        public Regex EachDayRegex { get; }

        public Regex SetWeekDayRegex { get; }

        public Regex SetEachRegex { get; }

        public bool GetMatchedDailyTimex(string text, out string timex)
        {
            var trimmedText = text.Trim();

            float durationLength = 1; // Default value
            float multiplier = 1;
            string durationType;

            if (DoubleMultiplierRegexCache.IsMatch(trimmedText))
            {
                multiplier = 2;
            }
            else if (HalfMultiplierRegexCache.IsMatch(trimmedText))
            {
                multiplier = 0.5f;
            }

            if (DayTypeRegexCache.IsMatch(trimmedText))
            {
                durationType = "D";
            }
            else if (WeekTypeRegexCache.IsMatch(trimmedText))
            {
                durationType = "W";
            }
            else if (WeekendTypeRegexCache.IsMatch(trimmedText))
            {
                durationType = "WE";
            }
            else if (MonthTypeRegexCache.IsMatch(trimmedText))
            {
                durationType = "M";
            }
            else if (QuarterTypeRegexCache.IsMatch(trimmedText))
            {
                durationLength = 3;
                durationType = "M";
            }
            else if (YearTypeRegexCache.IsMatch(trimmedText))
            {
                durationType = "Y";
            }
            else
            {
                timex = null;
                return false;
            }

            timex = TimexUtility.GenerateSetTimex(durationType, durationLength, multiplier);

            return true;
        }

        public bool GetMatchedUnitTimex(string text, out string timex)
        {
            return GetMatchedDailyTimex(text, out timex);
        }

        public string WeekDayGroupMatchString(Match match) => SetHandler.WeekDayGroupMatchString(match);

    }
}