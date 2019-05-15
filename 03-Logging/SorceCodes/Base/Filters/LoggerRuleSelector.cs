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
         * 1. �����������RuleӦ����ĳһ�������Provider�����Һ͵�ǰLog��Provider��һ�£���ֱ�ӷ���False,��Ϊ��ƥ��
         * 2. ����������ĵ�RuleӦ����ĳһ�������Category��Ĭ�������ȫ���������Һ͵�ǰLog��Category��ƥ�䣬�򷵻�False����Ϊ��ƥ��
         * 3. �����ǰ��RuleӦ����ĳһ�������Provider�����Ҵ�������Rule����Ӧ����ĳһ�������Provider���򷵻�False����Ϊ��ǰ�����ȼ�����
         * 4. �����ǰ��Rule����Ӧ����ĳһ�������Provider�����Ҵ�������Rule��Ӧ����ĳһ�������Provider�����ڵ�һ��Ĵ��ڣ���������Rule��Providerһ���Ǻ͵�ǰLogһ�µģ����ڵڶ���Ĵ��ڣ������������Rule����Category,��һ���͵�ǰ��Logһ�£����򷵻�True����Ϊ�����������ȼ�����
         * 5. �����ǰ��Rule��Ӧ����ĳһ�������Category�����Ҵ��ݽ�����Rule����Ӧ����ĳһ�������Category���򷵻�False����Ϊ��ǰ���ȼ�����
         * 6. �����ǰ��Rule��Ӧ����ĳһ�������Category�����ҵ�ǰ���ݽ�����RuleҲ��Ӧ����ĳһ�������Category�����ǵ�ǰ��ƥ��ȸ��ߣ��ַ�������Խ�������򷵻�False����Ϊ��ǰ�����ȼ�����
         * 7. ���඼����True
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