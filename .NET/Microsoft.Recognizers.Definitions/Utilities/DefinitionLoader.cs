﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Recognizers.Definitions.Utilities
{
    public static class DefinitionLoader
    {
        public static Dictionary<Regex, Regex> LoadAmbiguityFilters(Dictionary<string, string> filters)
        {
            var ambiguityFiltersDict = new Dictionary<Regex, Regex>();

            if (filters != null)
            {
                foreach (var item in filters)
                {
                    if (!"null".Equals(item.Key, StringComparison.Ordinal))
                    {
                        ambiguityFiltersDict.Add(RegexCache.Get(item.Key, RegexOptions.Singleline), RegexCache.Get(item.Value, RegexOptions.Singleline));
                    }
                }
            }

            return ambiguityFiltersDict;
        }
    }
}
