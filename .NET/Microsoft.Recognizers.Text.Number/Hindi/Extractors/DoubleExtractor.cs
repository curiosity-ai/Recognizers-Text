﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Hindi;

namespace Microsoft.Recognizers.Text.Number.Hindi
{
    public class DoubleExtractor : BaseNumberExtractor
    {
        private const RegexOptions RegexFlags = RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly ConcurrentDictionary<string, DoubleExtractor> Instances =
            new ConcurrentDictionary<string, DoubleExtractor>();

        private DoubleExtractor(string placeholder = NumbersDefinitions.PlaceHolderDefault)
        {
            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.DoubleDecimalPointRegex(placeholder), RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleWithoutIntegralRegex(placeholder), RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleWithMultiplierRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleWithRoundNumber, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleAllFloatRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.HINDI)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleExponentialNotationRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.POWER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleCaretExponentialNotationRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.POWER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.DoubleNumCommaDot, placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.DoubleNumBlankDot, placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.DoubleNumNoBreakSpaceDot, placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_DOUBLE; // "Double";

        public static DoubleExtractor GetInstance(string placeholder = NumbersDefinitions.PlaceHolderDefault)
        {
            if (!Instances.ContainsKey(placeholder))
            {
                var instance = new DoubleExtractor(placeholder);
                Instances.TryAdd(placeholder, instance);
            }

            return Instances[placeholder];
        }
    }
}
