using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLocalization
{
    public class AllStringLocalizer : IStringLocalizer
    {
        private readonly IStringLocalizer _stringLocalizer;
        public AllStringLocalizer(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizer = stringLocalizerFactory.Create("ALL", "CoreLocalization");
        }

        public LocalizedString this[string name] => _stringLocalizer[name];

        public LocalizedString this[string name, params object[] arguments] => _stringLocalizer[name,arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _stringLocalizer.GetAllStrings(includeParentCultures);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return _stringLocalizer.WithCulture(culture);
        }
    }
}
