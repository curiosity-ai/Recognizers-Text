﻿using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Italian;
using Microsoft.Recognizers.Text.DateTime.Italian.Utilities;
using Microsoft.Recognizers.Text.DateTime.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Italian
{
    public class ItalianDateTimeExtractorConfiguration : BaseDateTimeOptionsConfiguration, IDateTimeExtractorConfiguration
    {

        // à - time at which, en - length of time, dans - amount of time
        public static readonly Regex PrepositionRegex =
          RegexCache.Get(DateTimeDefinitions.PrepositionRegex, RegexFlags);

        // right now, as soon as possible, recently, previously
        public static readonly Regex NowRegex =
            RegexCache.Get(DateTimeDefinitions.NowRegex, RegexFlags);

        // in the evening, afternoon, morning, night
        public static readonly Regex SuffixRegex =
            RegexCache.Get(DateTimeDefinitions.SuffixRegex, RegexFlags);

        public static readonly Regex TimeOfDayRegex =
            RegexCache.Get(DateTimeDefinitions.TimeOfDayRegex, RegexFlags);

        public static readonly Regex SpecificTimeOfDayRegex =
            RegexCache.Get(DateTimeDefinitions.SpecificTimeOfDayRegex, RegexFlags);

        public static readonly Regex TimeOfTodayAfterRegex =
             RegexCache.Get(DateTimeDefinitions.TimeOfTodayAfterRegex, RegexFlags);

        public static readonly Regex TimeOfTodayBeforeRegex =
            RegexCache.Get(DateTimeDefinitions.TimeOfTodayBeforeRegex, RegexFlags);

        public static readonly Regex SimpleTimeOfTodayAfterRegex =
            RegexCache.Get(DateTimeDefinitions.SimpleTimeOfTodayAfterRegex, RegexFlags);

        public static readonly Regex SimpleTimeOfTodayBeforeRegex =
            RegexCache.Get(DateTimeDefinitions.SimpleTimeOfTodayBeforeRegex, RegexFlags);

        public static readonly Regex SpecificEndOfRegex =
            RegexCache.Get(DateTimeDefinitions.SpecificEndOfRegex, RegexFlags);

        public static readonly Regex UnspecificEndOfRegex =
            RegexCache.Get(DateTimeDefinitions.UnspecificEndOfRegex, RegexFlags);

        public static readonly Regex UnitRegex =
            RegexCache.Get(DateTimeDefinitions.TimeUnitRegex, RegexFlags);

        public static readonly Regex ConnectorRegex =
            RegexCache.Get(DateTimeDefinitions.ConnectorRegex, RegexFlags);

        public static readonly Regex NumberAsTimeRegex =
            RegexCache.Get(DateTimeDefinitions.NumberAsTimeRegex, RegexFlags);

        public static readonly Regex DateNumberConnectorRegex =
            RegexCache.Get(DateTimeDefinitions.DateNumberConnectorRegex, RegexFlags);

        public static readonly Regex YearSuffix =
            RegexCache.Get(DateTimeDefinitions.YearSuffix, RegexFlags);

        public static readonly Regex YearRegex =
             RegexCache.Get(DateTimeDefinitions.YearRegex, RegexFlags);

        public static readonly Regex SuffixAfterRegex =
            RegexCache.Get(DateTimeDefinitions.SuffixAfterRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public ItalianDateTimeExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
            IntegerExtractor = Number.Italian.IntegerExtractor.GetInstance();
            DatePointExtractor = new BaseDateExtractor(new ItalianDateExtractorConfiguration(this));
            TimePointExtractor = new BaseTimeExtractor(new ItalianTimeExtractorConfiguration(this));
            DurationExtractor = new BaseDurationExtractor(new ItalianDurationExtractorConfiguration(this));
            UtilityConfiguration = new ItalianDatetimeUtilityConfiguration();
        }

        public IExtractor IntegerExtractor { get; }

        public IDateExtractor DatePointExtractor { get; }

        public IDateTimeExtractor TimePointExtractor { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        Regex IDateTimeExtractorConfiguration.NowRegex => NowRegex;

        Regex IDateTimeExtractorConfiguration.SuffixRegex => SuffixRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfTodayAfterRegex => TimeOfTodayAfterRegex;

        Regex IDateTimeExtractorConfiguration.SimpleTimeOfTodayAfterRegex => SimpleTimeOfTodayAfterRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfTodayBeforeRegex => TimeOfTodayBeforeRegex;

        Regex IDateTimeExtractorConfiguration.SimpleTimeOfTodayBeforeRegex => SimpleTimeOfTodayBeforeRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfDayRegex => TimeOfDayRegex;

        Regex IDateTimeExtractorConfiguration.SpecificEndOfRegex => SpecificEndOfRegex;

        Regex IDateTimeExtractorConfiguration.UnspecificEndOfRegex => UnspecificEndOfRegex;

        Regex IDateTimeExtractorConfiguration.UnitRegex => UnitRegex;

        Regex IDateTimeExtractorConfiguration.NumberAsTimeRegex => NumberAsTimeRegex;

        Regex IDateTimeExtractorConfiguration.DateNumberConnectorRegex => DateNumberConnectorRegex;

        Regex IDateTimeExtractorConfiguration.YearSuffix => YearSuffix;

        Regex IDateTimeExtractorConfiguration.YearRegex => YearRegex;

        Regex IDateTimeExtractorConfiguration.SuffixAfterRegex => SuffixAfterRegex;

        public IDateTimeExtractor DurationExtractor { get; }

        public bool IsConnector(string text)
        {
            text = text.Trim();
            return string.IsNullOrEmpty(text) || PrepositionRegexCache.IsMatch(text) || ConnectorRegexCache.IsMatch(text);
        }
    }
}
