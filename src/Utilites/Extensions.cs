

using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;


namespace drz.Infrastructure.Utility.Extensions
{
    /// <summary>
    /// Расширения
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Сравнение версий сборок
        /// <br>https://stackoverflow.com/a/28695949</br>
        /// </summary>
        /// <param name="version">новая версия</param>
        /// <param name="otherVersion">старая версия</param>
        /// <param name="significantParts"> Major-1 Minor-2 Build-3 Revision-4</param>
        /// <returns> новая больше =1, новая меньше =-1, равны =0 </returns>
        public static int CompareTo(this Version version, Version otherVersion, int significantParts)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            if (otherVersion == null)
            {
                return 1;
            }

            if (version.Major != otherVersion.Major && significantParts >= 1)
                if (version.Major > otherVersion.Major)
                    return 1;
                else
                    return -1;

            if (version.Minor != otherVersion.Minor && significantParts >= 2)
                if (version.Minor > otherVersion.Minor)
                    return 1;
                else
                    return -1;

            if (version.Build != otherVersion.Build && significantParts >= 3)
                if (version.Build > otherVersion.Build)
                    return 1;
                else
                    return -1;

            if (version.Revision != otherVersion.Revision && significantParts >= 4)
                if (version.Revision > otherVersion.Revision)
                    return 1;
                else
                    return -1;

            return 0;
        }


        /// <summary>
        /// конвертация XmlDocument в XDocument
        /// https://stackoverflow.com/questions/1508572/converting-xdocument-to-xmldocument-and-vice-versa
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns>XDocument</returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (XmlNodeReader nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        /// <summary>
        /// конвертация XDocument в XmlDocument
        /// https://stackoverflow.com/questions/1508572/converting-xdocument-to-xmldocument-and-vice-versa
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.PreserveWhitespace = true;
            using (XmlReader xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

    }
}
