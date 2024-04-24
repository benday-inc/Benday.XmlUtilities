using System;
using System.Linq;
using System.Xml.Linq;

namespace Benday.XmlUtilities
{
    /// <summary>
    /// Provides utility methods for working with XML elements and documents.
    /// </summary>
    public static class XmlUtility
    {
        /// <summary>
        /// Sets the value of an attribute in the specified XElement to the specified double value.
        /// </summary>
        /// <param name="toValue">The XElement to set the attribute value on.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The double value to set.</param>
        public static void SetAttributeValue(XElement toValue, string name, double value)
        {
            SetAttributeValue(toValue, name, value.ToString());
        }

        /// <summary>
        /// Sets the value of the specified attribute in the given XML element.
        /// </summary>
        /// <param name="toValue">The XML element to set the attribute value for.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value to set for the attribute.</param>
        public static void SetAttributeValue(XElement toValue, string name, string value)
        {
            toValue.SetAttributeValue(name, value);
        }

        /// <summary>
        /// Sets the value of a child element with the specified name in the given parent element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="childElementValue">The value to set for the child element.</param>
        public static void SetChildElement(XElement parentElement,
            string childElementName,
            double childElementValue)
        {
            SetChildElement(parentElement, childElementName,
                childElementValue.ToString());
        }

        /// <summary>
        /// Sets the value of a child element with the specified name in the given parent element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="childElementValue">The value to set for the child element.</param>
        public static void SetChildElement(XElement parentElement,
            string childElementName,
            DateTime childElementValue)
        {
            SetChildElement(parentElement, childElementName,
                childElementValue.ToString());
        }

        /// <summary>
        /// Sets the value of a child element with the specified name in the given parent element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="childElementValue">The value to set for the child element.</param>
        public static void SetChildElement(XElement parentElement, string childElementName, string childElementValue)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(childElementName))
                throw new ArgumentException("childElementName is null or empty.", "childElementName");
            if (String.IsNullOrEmpty(childElementValue))
            {
                childElementValue = String.Empty;
            }

            parentElement.SetElementValue(childElementName, childElementValue);
        }

        /// <summary>
        /// Gets the child element with the specified name from the parent element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <returns>The child element with the specified name, or null if not found.</returns>
        public static XElement GetChildElement(XElement parentElement, string childElementName)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(childElementName))
                throw new ArgumentException("childElementName is null or empty.", "childElementName");

            var childElement = parentElement.Descendants(childElementName).FirstOrDefault();
            return childElement;
        }

        /// <summary>
        /// Gets the value of a child element from the specified parent element.
        /// If the child element is not found, returns the specified default value.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="defaultReturnValue">The default value to return if the child element is not found.</param>
        /// <returns>The value of the child element, or the default value if the child element is not found.</returns>
        public static string GetChildElementValue(XElement parentElement,
            string childElementName,
            string defaultReturnValue)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(childElementName))
                throw new ArgumentException("childElementName is null or empty.", "childElementName");

            XElement childElement = GetChildElement(parentElement, childElementName);

            if (childElement == null)
                return defaultReturnValue;
            else
                return childElement.Value;
        }

        /// <summary>
        /// Gets the value of a child element as a <see cref="DateTime"/> from the specified parent element.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="defaultReturnValue">The default value to return if the child element is not found or cannot be parsed as a <see cref="DateTime"/>.</param>
        /// <returns>The value of the child element as a <see cref="DateTime"/> if found and successfully parsed; otherwise, the default value.</returns>
        public static DateTime GetChildElementValue(XElement parentElement,
            string childElementName,
            DateTime defaultReturnValue)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(childElementName))
                throw new ArgumentException("childElementName is null or empty.", "childElementName");

            XElement childElement = GetChildElement(parentElement, childElementName);

            if (childElement == null)
                return defaultReturnValue;
            else
            {
                DateTime returnValue;

                if (DateTime.TryParse(childElement.Value, out returnValue) == true)
                {
                    return returnValue;
                }
                else
                {
                    return defaultReturnValue;
                }
            }
        }

        /// <summary>
        /// Gets the value of a child element from the specified parent element.
        /// If the child element is not found or its value cannot be parsed as an integer,
        /// the default return value is returned.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="defaultReturnValue">The default return value.</param>
        /// <returns>The value of the child element if found and parsed successfully; otherwise, the default return value.</returns>
        public static int GetChildElementValue(XElement parentElement,
            string childElementName,
            int defaultReturnValue)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(childElementName))
                throw new ArgumentException("childElementName is null or empty.", "childElementName");

            XElement childElement = GetChildElement(parentElement, childElementName);

            if (childElement == null)
                return defaultReturnValue;
            else
            {
                int returnValue;

                if (Int32.TryParse(childElement.Value, out returnValue) == true)
                {
                    return returnValue;
                }
                else
                {
                    return defaultReturnValue;
                }
            }
        }

        /// <summary>
        /// Retrieves the value of a child element in an XML document based on the specified criteria.
        /// </summary>
        /// <param name="parentElement">The parent element in which to search for the child element.</param>
        /// <param name="childElementName">The name of the child element to retrieve the value from.</param>
        /// <param name="childAttributeName">The name of the attribute of the child element to match.</param>
        /// <param name="childAttributeValue">The value of the attribute of the child element to match.</param>
        /// <param name="defaultReturnValue">The default value to return if the child element is not found.</param>
        /// <returns>The value of the child element if found; otherwise, the default value.</returns>
        public static string GetChildElementValue(XElement parentElement,
            string childElementName,
            string childAttributeName,
            string childAttributeValue,
            string defaultReturnValue)
        {
            if (parentElement == null)
                throw new ArgumentNullException("parentElement", "parentElement is null.");
            if (String.IsNullOrEmpty(childElementName))
                throw new ArgumentException("childElementName is null or empty.", "childElementName");

            var childElement = (
                from temp in parentElement.Descendants(childElementName)
                where temp.HasAttributes == true &&
                temp.Attribute(childAttributeName) != null &&
                temp.Attribute(childAttributeName).Value == childAttributeValue
                select temp).FirstOrDefault();

            if (childElement == null)
                return defaultReturnValue;
            else
                return childElement.Value;
        }

        /// <summary>
        /// Converts a string representation of XML to an XDocument object.
        /// </summary>
        /// <param name="fromValue">The string representation of XML.</param>
        /// <returns>An XDocument object representing the XML.</returns>
        public static XDocument StringToXDocument(string fromValue)
        {
            return XDocument.Parse(fromValue);
        }
    }
}
