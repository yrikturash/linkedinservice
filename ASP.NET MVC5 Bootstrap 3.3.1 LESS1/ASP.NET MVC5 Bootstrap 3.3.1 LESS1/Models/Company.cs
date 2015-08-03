using System;
using System.Xml.Serialization;

namespace ASP.NET_MVC5_Bootstrap3_3_1_LESS.Models
{
    [Serializable, XmlRoot("company")]
    public class Company
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }

    [Serializable, XmlRoot("companies")]
    public class CompanyCollection
    {
        [System.Xml.Serialization.XmlElementAttribute("company")]
        public Company[] Companies { get; set; }
    }
}