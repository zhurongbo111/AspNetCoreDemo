// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.Logging
{
    internal class LoggerRuleSelector
    {
        public void Select(LoggerFilterOptions options, Type providerType, string category, out LogLevel? minLevel, out Func<string, string, LogLevel, bool> filter)
        {
            filter = null;
            minLevel = options.MinLevel;

            // Filter rule selection:
            // 1. Select rules for current logger type, if there is none, select ones without logger type specified
            // 2. Select rules with longest matching categories
            // 3. If there nothing matched by category take all rules without category
            // 3. If there is only one rule use it's level and filter
            // 4. If there are multiple rules use last
            // 5. If there are no applicable rules use global minimal level

            var providerAlias = ProviderAliasUtilities.GetAlias(providerType);
            LoggerFilterRule current = null;
            foreach (var rule in options.Rules)
            {
                if (IsBetter(rule, current, providerType.FullName, category)
                    || (!string.IsNullOrEmpty(providerAlias) && IsBetter(rule, current, providerAlias, category)))
                {
                    current = rule;
                }
            }

            if (current != null)
            {
                filter = current.Filter;
                minLevel = current.LogLevel;
            }
        }

        /*
         * 1. 如果传进来的Rule应用于某一个具体的Provider，并且和当前Log的Provider不一致，则直接返回False,因为不匹配
         * 2. 如果传进来的的Rule应用于某一个具体的Category（默认是类的全名），并且和当前Log的Category不匹配，则返回False，因为不匹配
         * 3. 如果当前的Rule应用于某一个具体的Provider，并且传进来的Rule不是应用于某一个具体的Provider，则返回False，因为当前的优先级更高
         * 4. 如果当前的Rule不是应用于某一个具体的Provider，并且传进来的Rule是应用于某一个具体的Provider（由于第一点的存在，传进来的Rule的Provider一定是和当前Log一致的，由于第二点的存在，如果传进来的Rule包含Category,则一定和当前的Log一致），则返回True，因为传进来的优先级更高
         * 5. 如果当前的Rule是应用于某一个具体的Category，并且传递进来的Rule不是应用于某一个具体的Category，则返回False，因为当前优先级更高
         * 6. 如果当前的Rule是应用于某一个具体的Category，并且当前传递进来的Rule也是应用于某一个具体的Category，但是当前的匹配度更高（字符串长度越长），则返回False，因为当前的优先级更高
         * 7. 其余都返回True
         */
        private static bool IsBetter(LoggerFilterRule rule, LoggerFilterRule current, string logger, string category)
        {
            // Skip rules with inapplicable type or category
            if (rule.ProviderName != null && rule.ProviderName != logger)
            {
                return false;
            }

            if (rule.CategoryName != null && !category.StartsWith(rule.CategoryName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (current?.ProviderName != null)
            {
                if (rule.ProviderName == null)
                {
                    return false;
                }
            }
            else
            {
                // We want to skip category check when going from no provider to having provider
                if (rule.ProviderName != null)
                {
                    return true;
                }
            }

            if (current?.CategoryName != null)
            {
                if (rule.CategoryName == null)
                {
                    return false;
                }

                if (current.CategoryName.Length > rule.CategoryName.Length)
                {
                    return false;
                }
            }

            return true;
        }
    }
}