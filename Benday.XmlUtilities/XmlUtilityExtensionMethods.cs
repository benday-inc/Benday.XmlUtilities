using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace Benday.XmlUtilities
{
    public static class XmlUtilityExtensionMethods
    {
        public static IEnumerable<XElement> ElementsByLocalName(this XElement? parent, string name)
        {
            if (parent == null)
            {
                return Array.Empty<XElement>();
            }
            else
            {
                var result = (from temp in parent.Elements()
                              where temp.Name.LocalName == name
                              select temp);

                return result;
            }
        }

        public static IEnumerable<XElement> ElementsByLocalNameAndAttributeValue(this XElement? parent,
            string elementName,
            string attributeName,
            string attributeValue)
        {
            var matchingElementsByName = parent.ElementsByLocalName(elementName);

            var result = (from temp in matchingElementsByName
                          where
                          temp.HasAttributes == true &&
                          temp.AttributeValue(attributeName) == attributeValue
                          select temp);

            return result;
        }

        public static XElement? ElementByLocalName(this XElement? parent, string name)
        {
            if (parent == null)
            {
                return null;
            }
            else
            {
                var result = (from temp in parent.Elements()
                              where temp.Name.LocalName == name
                              select temp).FirstOrDefault();

                return result;
            }
        }

        public static XElement ElementByLocalNameAndAttributeValue(this XElement? parent,
            string elementName,
            string attributeName,
            string attributeValue)
        {
            var matchingElementsByName = parent.ElementsByLocalName(elementName);

            var match = (from temp in matchingElementsByName
                         where
                         temp.HasAttributes == true &&
                         temp.AttributeValue(attributeName) == attributeValue
                         select temp).FirstOrDefault();

            return match;
        }

        public static XElement ElementByLocalNameAndAttributeValue(this XElement? parent,
            string elementName,
            string attributeName1,
            string attributeValue1,
            string attributeName2,
            string attributeValue2)
        {
            var matchingElementsByName = parent.ElementsByLocalName(elementName);

            var match = (from temp in matchingElementsByName
                         where
                         temp.HasAttributes == true &&
                         temp.AttributeValue(attributeName1) == attributeValue1 &&
                         temp.AttributeValue(attributeName2) == attributeValue2
                         select temp).FirstOrDefault();

            return match;
        }

        /// <summary>
        /// Finds a child element starting from parent and returns the inner text value.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="childElement"></param>
        /// <returns></returns>
        public static string? ElementValue(this XElement? parent, string childElement)
        {
            var child = parent.ElementByLocalName(childElement);

            if (child == null)
            {
                return null;
            }
            else
            {
                return child.Value;
            }
        }

        public static void SetElementValueByLocalName(this XElement parent, string childElement, string value)
        {
            var child = parent.ElementByLocalName(childElement);

            if (child == null)
            {
                throw new InvalidOperationException("Could not locate target element.");
            }
            else
            {
                child.Value = value;
            }
        }

        public static string? ElementValueByChildNameAndAttributeValue(
            this XElement? parent, string childElement,
            string attributeName, string attributeValue)
        {
            var child = parent.ElementByLocalNameAndAttributeValue(childElement, attributeName, attributeValue);

            if (child == null)
            {
                return null;
            }
            else
            {
                return child.Value;
            }
        }

        public static string AttributeValue(this XElement? parent, string attributeName)
        {
            if (parent == null)
            {
                return String.Empty;
            }
            else if (parent.HasAttributes == false)
            {
                return String.Empty;
            }
            else if (parent.Attribute(attributeName) == null)
            {
                return String.Empty;
            }
            else
            {
                return parent.Attribute(attributeName).Value;
            }
        }

        public static bool HasAttribute(this XElement? parent, string attributeName)
        {
            if (parent == null)
            {
                return false;
            }
            else if (parent.HasAttributes == false)
            {
                return false;
            }
            else if (parent.Attribute(attributeName) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool HasAttributeByLocalName(this XElement? parent, string attributeName)
        {
            if (parent == null || parent.HasAttributes == false)
            {
                return false;
            }

            var hasAttribute = parent.HasAttribute(attributeName);

            if (hasAttribute == true)
            {
                return true;
            }
            else
            {
                var attr = parent.Attributes().FirstOrDefault(a => a.Name.LocalName == attributeName);

                if (attr != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string AttributeValueByLocalName(this XElement? sourceElement, string attributeName)
        {
            if (sourceElement == null || sourceElement.HasAttributes == false)
            {
                return string.Empty;
            }

            var returnValue = sourceElement.AttributeValue(attributeName);

            if (string.IsNullOrEmpty(returnValue) == true)
            {
                var attr = sourceElement.Attributes().FirstOrDefault(a => a.Name.LocalName == attributeName);

                if (attr != null)
                {
                    return attr.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return returnValue;
            }
        }

        public static double GetAttributeValueAsDouble(
            this XElement? element, string attributeName)
        {
            var valueAsString = element.HasAttributeByLocalName(attributeName) == true ?
                element.AttributeValueByLocalName(attributeName) :
                string.Empty;

            if (String.IsNullOrEmpty(valueAsString) == true)
            {
                return default;
            }
            else
            {
                if (Double.TryParse(valueAsString, out var temp) == true)
                {
                    return temp;
                }
                else
                {
                    return default;
                }
            }
        }

        public static int GetAttributeValueAsInt32(
            this XElement? element, string attributeName)
        {
            var valueAsString = element.HasAttributeByLocalName(attributeName) == true ?
                element.AttributeValueByLocalName(attributeName) :
                string.Empty;

            if (String.IsNullOrEmpty(valueAsString) == true)
            {
                return default;
            }
            else
            {
                if (Int32.TryParse(valueAsString, out var temp) == true)
                {
                    return temp;
                }
                else
                {
                    return default;
                }
            }
        }

        public static bool GetAttributeValueAsBoolean(
            this XElement? element, string attributeName)
        {
            var valueAsString = element.HasAttributeByLocalName(attributeName) == true ?
                element.AttributeValueByLocalName(attributeName) :
                string.Empty;

            if (String.IsNullOrEmpty(valueAsString) == true)
            {
                return default;
            }
            else
            {
                if (Boolean.TryParse(valueAsString, out var temp) == true)
                {
                    return temp;
                }
                else
                {
                    return default;
                }
            }
        }
    }
}
