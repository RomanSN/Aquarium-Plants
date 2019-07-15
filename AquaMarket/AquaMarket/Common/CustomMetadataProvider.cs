using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AquaMarket.Common
{

    public class CustomMetadataProvider : DataAnnotationsModelMetadataProvider
    {
       protected override ModelMetadata CreateMetadata(
       IEnumerable<Attribute> attributes,
       Type containerType,
       Func<object> modelAccessor,
       Type modelType,
       string propertyName)
        {
            var meta = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (meta.DisplayName == null)
                meta.DisplayName = ToSeparatedWords(meta.PropertyName);
            return meta;
        }
        public string ToSeparatedWords(string value)
        {
            if (value != null)
                return Regex.Replace(value, "([A-Z][a-z]?)", " $1").Trim();
            return value;
        }
    }
}