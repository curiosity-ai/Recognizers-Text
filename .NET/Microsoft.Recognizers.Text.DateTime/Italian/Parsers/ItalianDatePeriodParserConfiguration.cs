﻿using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Italian;

namespace Microsoft.Recognizers.Text.DateTime.Italian
{
    public class ItalianDatePeriodParserConfiguration : BaseDateTimeOptionsConfiguration, IDatePeriodParserConfiguration
    {
        public static readonly Regex UpcomingPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.UpcomingPrefixRegex, RegexFlags);

        public static readonly Regex NextPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.NextPrefixRegex, RegexFlags);

        public static readonly Regex PastPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.PastPrefixRegex, RegexFlags);

        public static readonly Regex PreviousPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.PreviousPrefixRegex, RegexFlags);

        public static readonly Regex ThisPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.ThisPrefixRegex, RegexFlags);

        public static readonly Regex NextSuffixRegex =
            RegexCache.Get(DateTimeDefinitions.NextSuffixRegex, RegexFlags);

        public static readonly Regex PastSuffixRegex =
            RegexCache.Get(DateTimeDefinitions.PastSuffixRegex, RegexFlags);

        public static readonly Regex AfterNextSuffixRegex =
            RegexCache.Get(DateTimeDefinitions.AfterNextSuffixRegex, RegexFlags);

        public static readonly Regex RelativeRegex =
            RegexCache.Get(DateTimeDefinitions.RelativeRegex, RegexFlags);

        public static readonly Regex UnspecificEndOfRangeRegex =
            RegexCache.Get(DateTimeDefinitions.UnspecificEndOfRangeRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public ItalianDatePeriodParserConfiguration(ICommonDateTimeParserConfiguration config)
            : base(config)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            CardinalExtractor = config.CardinalExtractor;
            OrdinalExtractor = config.OrdinalExtractor;
            IntegerExtractor = config.IntegerExtractor;
            NumberParser = config.NumberParser;
            DurationExtractor = config.DurationExtractor;
            DateExtractor = config.DateExtractor;
            DurationParser = config.DurationParser;
            DateParser = config.DateParser;

            MonthFrontBetweenRegex = ItalianDatePeriodExtractorConfiguration.MonthFrontBetweenRegex;
            BetweenRegex = ItalianDatePeriodExtractorConfiguration.BetweenRegex;
            MonthFrontSimpleCasesRegex = ItalianDatePeriodExtractorConfiguration.MonthFrontSimpleCasesRegex;
            SimpleCasesRegex = ItalianDatePeriodExtractorConfiguration.SimpleCasesRegex;
            OneWordPeriodRegex = ItalianDatePeriodExtractorConfiguration.OneWordPeriodRegex;
            MonthWithYear = ItalianDatePeriodExtractorConfiguration.MonthWithYear;
            MonthNumWithYear = ItalianDatePeriodExtractorConfiguration.MonthNumWithYear;
            YearRegex = ItalianDatePeriodExtractorConfiguration.YearRegex;
            PastRegex = ItalianDatePeriodExtractorConfiguration.PastPrefixRegex;
            FutureRegex = ItalianDatePeriodExtractorConfiguration.NextPrefixRegex;
            FutureSuffixRegex = ItalianDatePeriodExtractorConfiguration.FutureSuffixRegex;
            NumberCombinedWithUnit = ItalianDurationExtractorConfiguration.NumberCombinedWithDurationUnit;
            WeekOfMonthRegex = ItalianDatePeriodExtractorConfiguration.WeekOfMonthRegex;
            WeekOfYearRegex = ItalianDatePeriodExtractorConfiguration.WeekOfYearRegex;
            QuarterRegex = ItalianDatePeriodExtractorConfiguration.QuarterRegex;
            QuarterRegexYearFront = ItalianDatePeriodExtractorConfiguration.QuarterRegexYearFront;
            AllHalfYearRegex = ItalianDatePeriodExtractorConfiguration.AllHalfYearRegex;
            SeasonRegex = ItalianDatePeriodExtractorConfiguration.SeasonRegex;
            WhichWeekRegex = ItalianDatePeriodExtractorConfiguration.WhichWeekRegex;
            WeekOfRegex = ItalianDatePeriodExtractorConfiguration.WeekOfRegex;
            MonthOfRegex = ItalianDatePeriodExtractorConfiguration.MonthOfRegex;
            RestOfDateRegex = ItalianDatePeriodExtractorConfiguration.RestOfDateRegex;
            LaterEarlyPeriodRegex = ItalianDatePeriodExtractorConfiguration.LaterEarlyPeriodRegex;
            WeekWithWeekDayRangeRegex = ItalianDatePeriodExtractorConfiguration.WeekWithWeekDayRangeRegex;
            YearPlusNumberRegex = ItalianDatePeriodExtractorConfiguration.YearPlusNumberRegex;
            DecadeWithCenturyRegex = ItalianDatePeriodExtractorConfiguration.DecadeWithCenturyRegex;
            YearPeriodRegex = ItalianDatePeriodExtractorConfiguration.YearPeriodRegex;
            ComplexDatePeriodRegex = ItalianDatePeriodExtractorConfiguration.ComplexDatePeriodRegex;
            RelativeDecadeRegex = ItalianDatePeriodExtractorConfiguration.RelativeDecadeRegex;
            InConnectorRegex = config.UtilityConfiguration.InConnectorRegex;
            WithinNextPrefixRegex = ItalianDatePeriodExtractorConfiguration.WithinNextPrefixRegex;
            ReferenceDatePeriodRegex = ItalianDatePeriodExtractorConfiguration.ReferenceDatePeriodRegex;
            AgoRegex = ItalianDatePeriodExtractorConfiguration.AgoRegex;
            LaterRegex = ItalianDatePeriodExtractorConfiguration.LaterRegex;
            LessThanRegex = ItalianDatePeriodExtractorConfiguration.LessThanRegex;
            MoreThanRegex = ItalianDatePeriodExtractorConfiguration.MoreThanRegex;
            CenturySuffixRegex = ItalianDatePeriodExtractorConfiguration.CenturySuffixRegex;
            NowRegex = ItalianDatePeriodExtractorConfiguration.NowRegex;
            SpecialDayRegex = ItalianDateExtractorConfiguration.SpecialDayRegex;
            TodayNowRegex = RegexCache.Get(DateTimeDefinitions.TodayNowRegex, RegexOptions.Singleline);

            UnitMap = config.UnitMap;
            CardinalMap = config.CardinalMap;
            DayOfMonth = config.DayOfMonth;
            MonthOfYear = config.MonthOfYear;
            SeasonMap = config.SeasonMap;
            SpecialYearPrefixesMap = config.SpecialYearPrefixesMap;
            WrittenDecades = config.WrittenDecades;
            Numbers = config.Numbers;
            SpecialDecadeCases = config.SpecialDecadeCases;
        }

        public int MinYearNum { get; }

        public int MaxYearNum { get; }

        public string TokenBeforeDate { get; }

        public IDateExtractor DateExtractor { get; }

        public IExtractor CardinalExtractor { get; }

        public IExtractor OrdinalExtractor { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public IParser NumberParser { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeParser DurationParser { get; }

        public Regex MonthFrontBetweenRegex { get; }

        public Regex BetweenRegex { get; }

        public Regex MonthFrontSimpleCasesRegex { get; }

        public Regex SimpleCasesRegex { get; }

        public Regex OneWordPeriodRegex { get; }

        public Regex MonthWithYear { get; }

        public Regex MonthNumWithYear { get; }

        public Regex YearRegex { get; }

        public Regex PastRegex { get; }

        public Regex FutureRegex { get; }

        public Regex FutureSuffixRegex { get; }

        public Regex NumberCombinedWithUnit { get; }

        public Regex WeekOfMonthRegex { get; }

        public Regex WeekOfYearRegex { get; }

        public Regex QuarterRegex { get; }

        public Regex QuarterRegexYearFront { get; }

        public Regex AllHalfYearRegex { get; }

        public Regex SeasonRegex { get; }

        public Regex WhichWeekRegex { get; }

        public Regex WeekOfRegex { get; }

        public Regex MonthOfRegex { get; }

        public Regex InConnectorRegex { get; }

        public Regex WithinNextPrefixRegex { get; }

        public Regex RestOfDateRegex { get; }

        public Regex LaterEarlyPeriodRegex { get; }

        public Regex WeekWithWeekDayRangeRegex { get; }

        public Regex YearPlusNumberRegex { get; }

        public Regex DecadeWithCenturyRegex { get; }

        public Regex YearPeriodRegex { get; }

        public Regex ComplexDatePeriodRegex { get; }

        public Regex RelativeDecadeRegex { get; }

        public Regex ReferenceDatePeriodRegex { get; }

        public Regex AgoRegex { get; }

        public Regex LaterRegex { get; }

        public Regex LessThanRegex { get; }

        public Regex MoreThanRegex { get; }

        public Regex CenturySuffixRegex { get; }

        public Regex NowRegex { get; }

        public Regex SpecialDayRegex { get; }

        public Regex TodayNowRegex { get; }

        Regex ISimpleDatePeriodParserConfiguration.RelativeRegex => RelativeRegex;

        Regex IDatePeriodParserConfiguration.NextPrefixRegex => NextPrefixRegex;

        Regex IDatePeriodParserConfiguration.PreviousPrefixRegex => PreviousPrefixRegex;

        Regex IDatePeriodParserConfiguration.ThisPrefixRegex => ThisPrefixRegex;

        Regex IDatePeriodParserConfiguration.UnspecificEndOfRangeRegex => UnspecificEndOfRangeRegex;

        bool IDatePeriodParserConfiguration.CheckBothBeforeAfter => DateTimeDefinitions.CheckBothBeforeAfter;

        public IImmutableDictionary<string, string> UnitMap { get; }

        public IImmutableDictionary<string, int> CardinalMap { get; }

        public IImmutableDictionary<string, int> DayOfMonth { get; }

        public IImmutableDictionary<string, int> MonthOfYear { get; }

        public IImmutableDictionary<string, string> SeasonMap { get; }

        public IImmutableDictionary<string, string> SpecialYearPrefixesMap { get; }

        public IImmutableDictionary<string, int> WrittenDecades { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public IImmutableDictionary<string, int> SpecialDecadeCases { get; }

        public IImmutableList<string> InStringList { get; }

        public int GetSwiftDayOrMonth(string text)
        {
            var swift = 0;

            var trimmedText = text.Trim();

            if (AfterNextSuffixRegexCache.IsMatch(trimmedText))
            {
                swift = 2;
            }
            else if (NextPrefixRegexCache.IsMatch(trimmedText))
            {
                swift = 1;
            }
            else if (PreviousPrefixRegexCache.IsMatch(trimmedText))
            {
                swift = -1;
            }

            return swift;
        }

        public int GetSwiftYear(string text)
        {
            var swift = -10;

            var trimmedText = text.Trim();

            if (AfterNextSuffixRegexCache.IsMatch(trimmedText))
            {
                swift = 2;
            }
            else if (NextPrefixRegexCache.IsMatch(trimmedText))
            {
                swift = 1;
            }
            else if (PreviousPrefixRegexCache.IsMatch(trimmedText))
            {
                swift = -1;
            }
            else if (ThisPrefixRegexCache.IsMatch(trimmedText))
            {
                swift = 0;
            }

            return swift;
        }

        public bool IsFuture(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.FutureStartTerms.Any(o => trimmedText.StartsWith(o, StringComparison.Ordinal)) ||
                   DateTimeDefinitions.FutureEndTerms.Any(o => trimmedText.EndsWith(o, StringComparison.Ordinal));
        }

        public bool IsLastCardinal(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.LastCardinalTerms.Any(o => trimmedText.Equals(o, StringComparison.Ordinal));
        }

        public bool IsMonthOnly(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.MonthTerms.Any(o => trimmedText.EndsWith(o, StringComparison.Ordinal)) ||
                   (DateTimeDefinitions.MonthTerms.Any(o => trimmedText.Contains(o)) && (AfterNextSuffixRegexCache.IsMatch(trimmedText) ||
                   ThisPrefixRegexCache.IsMatch(trimmedText) || NextSuffixRegexCache.IsMatch(trimmedText) || PastSuffixRegexCache.IsMatch(trimmedText)));
        }

        public bool IsMonthToDate(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.MonthToDateTerms.Any(o => trimmedText.Equals(o, StringComparison.Ordinal));
        }

        public bool IsWeekend(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.WeekendTerms.Any(o => trimmedText.EndsWith(o, StringComparison.Ordinal)) ||
                   (DateTimeDefinitions.WeekendTerms.Any(o => trimmedText.Contains(o)) && (AfterNextSuffixRegexCache.IsMatch(trimmedText) ||
                   ThisPrefixRegexCache.IsMatch(trimmedText) || NextSuffixRegexCache.IsMatch(trimmedText) || PastSuffixRegexCache.IsMatch(trimmedText)));
        }

        public bool IsWeekOnly(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.WeekTerms.Any(o => trimmedText.EndsWith(o, StringComparison.Ordinal)) ||
                   (DateTimeDefinitions.WeekTerms.Any(o => trimmedText.Contains(o)) && (AfterNextSuffixRegexCache.IsMatch(trimmedText) ||
                   ThisPrefixRegexCache.IsMatch(trimmedText) || NextSuffixRegexCache.IsMatch(trimmedText) || PastSuffixRegexCache.IsMatch(trimmedText)));
        }

        public bool IsYearOnly(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.YearTerms.Any(o => trimmedText.EndsWith(o, StringComparison.Ordinal)) ||
                   (DateTimeDefinitions.YearTerms.Any(o => trimmedText.Contains(o)) && (AfterNextSuffixRegexCache.IsMatch(trimmedText) ||
                   ThisPrefixRegexCache.IsMatch(trimmedText) || NextSuffixRegexCache.IsMatch(trimmedText) || PastSuffixRegexCache.IsMatch(trimmedText)));
        }

        public bool IsYearToDate(string text)
        {
            var trimmedText = text.Trim();
            return DateTimeDefinitions.YearToDateTerms.Any(o => trimmedText.Equals(o, StringComparison.Ordinal));
        }
    }
}
