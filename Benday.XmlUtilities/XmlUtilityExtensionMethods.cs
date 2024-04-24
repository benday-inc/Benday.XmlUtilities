using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace Benday.XmlUtilities
{
    public static class XmlUtilityExtensionMethods
    {
        /// <summary>
        /// Gets all child elements of the parent XElement that have the specified local name.
        /// </summary>
        /// <param name="parent">The parent XElement.</param>
        /// <param name="name">The local name of the child elements.</param>
        /// <returns>An IEnumerable of XElements that match the specified local name.</returns>
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

        /// <summary>
        /// Gets all child elements of the parent XElement that have the specified local name and attribute value.
        /// </summary>
        /// <param name="parent">The parent XElement.</param>
        /// <param name="elementName">The local name of the child elements.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <returns>An IEnumerable of XElements that match the specified local name and attribute value.</returns>
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

        /// <summary>
        /// Gets the first child element of the parent XElement that has the specified local name.
        /// </summary>
        /// <param name="parent">The parent XElement.</param>
        /// <param name="name">The local name of the child element.</param>
        /// <returns>The first XElement that matches the specified local name, or null if no match is found.</returns>
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

        /// <summary>
        /// Gets the first child element of the parent XElement that has the specified local name and attribute value.
        /// </summary>
        /// <param name="parent">The parent XElement.</param>
        /// <param name="elementName">The local name of the child element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <returns>The first XElement that matches the specified local name and attribute value, or null if no match is found.</returns>
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
        /// <returns>Inner text value for matching element</returns>
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


        
        /// <summary>
        /// Sets the value of a child element with the specified local name.
        /// </summary>
        /// <param name="parent">The parent element.</param>
        /// <param name="childElement">The local name of the child element.</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="InvalidOperationException">Thrown when the target element cannot be located.</exception>
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

        /// <summary>
        /// Retrieves the value of a child element by its name and an attribute's name and value.
        /// </summary>
        /// <param name="parent">The parent XElement.</param>
        /// <param name="childElement">The name of the child element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <returns>The value of the child element, or null if the child element is not found.</returns>
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


        
        /// <summary>
        /// Retrieves the value of the specified attribute from the given XML element.
        /// </summary>
        /// <param name="parent">The XML element to retrieve the attribute value from.</param>
        /// <param name="attributeName">The name of the attribute to retrieve the value from.</param>
        /// <returns>The value of the specified attribute, or an empty string if the attribute or the parent element is null.</returns>
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


        /// <summary>
        /// Determines whether the specified XML element has the specified attribute.
        /// </summary>
        /// <param name="parent">The XML element to check for the attribute.</param>
        /// <param name="attributeName">The name of the attribute to check.</param>
        /// <returns><c>true</c> if the XML element has the specified attribute; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Determines whether the specified XML element has an attribute with the given local name.
        /// </summary>
        /// <param name="parent">The XML element to check for the attribute.</param>
        /// <param name="attributeName">The local name of the attribute to check.</param>
        /// <returns><c>true</c> if the XML element has the attribute; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Retrieves the value of an attribute with the specified local name from the source element.
        /// If the attribute is not found, an empty string is returned.
        /// </summary>
        /// <param name="sourceElement">The source element to retrieve the attribute value from.</param>
        /// <param name="attributeName">The local name of the attribute to retrieve.</param>
        /// <returns>The value of the attribute, or an empty string if the attribute is not found.</returns>
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


        /// <summary>
        /// Retrieves the value of the specified attribute as a double from the given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The value of the attribute as a double. If the attribute is not found or cannot be parsed as a double, the default value for double is returned.</returns>
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

        /// <summary>
        /// Retrieves the value of the specified attribute as an integer from the given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The value of the attribute as an integer, or the default value if the attribute is not found or cannot be parsed as an integer.</returns>
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

        /// <summary>
        /// Gets the value of the specified attribute as a boolean from the given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>
        /// The boolean value of the attribute, or the default value if the attribute is not found or cannot be parsed as a boolean.
        /// </returns>
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
