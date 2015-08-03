using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammock;
using Hammock.Authentication.OAuth;
using System.Configuration;
using Hammock.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using ASP.NET_MVC5_Bootstrap3_3_1_LESS.Models;

namespace ASP.NET_MVC5_Bootstrap_3._3._1_LESS1
{
    public class LinkedInService
    {
        private const string URL_BASE = "http://api.linkedin.com/v1";
        public static string ConsumerKey { get { return ConfigurationManager.AppSettings["ConsumerKey"]; } }
        public static string ConsumerKeySecret { get { return ConfigurationManager.AppSettings["ConsumerSecret"]; } }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public LinkedInService(string accessToken, string accessTokenSecret)
        {
            this.AccessToken = accessToken;
            this.AccessTokenSecret = accessTokenSecret;
        }

        private OAuthCredentials AccessCredentials
        {
            get
            {
                return new OAuthCredentials
                {
                    Type = OAuthType.AccessToken,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerKeySecret,
                    Token = AccessToken,
                    TokenSecret = AccessTokenSecret
                };
            }
        }

        #region Helper

        private RestResponse GetResponse(string path)
        {
            var client = new RestClient()
            {
                Authority = URL_BASE,
                Credentials = AccessCredentials,
                Method = WebMethod.Get
            };

            var request = new RestRequest { Path = path };

            return client.Request(request);
        }

        private T Deserialize<T>(string xmlContent)
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xmlContent));
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            return (T)deserializer.Deserialize(new StringReader(xmlContent));
        }

        #endregion

        #region People Information

        public Person GetCurrentUser()
        {
            var response = GetResponse("people/~");
            return Deserialize<Person>(response.Content);
        }

        public Person GetPersonById(string id)
        {
            var response = GetResponse("people/id=" + id.ToString());
            return Deserialize<Person>(response.Content);
        }

        public Person GetPersonByPublicProfileUrl(string url)
        {
            var response = GetResponse("people::(url=" + url + ")");
            return Deserialize<Person>(response.Content);
        }

        public PeopleSearchresult SearchPeopleByKeyWord(string keyword)
        {
            var response = GetResponse("people-search?keywords=" + keyword);
            return Deserialize<PeopleSearchresult>(response.Content);
        }

        public PeopleSearchresult GetPeopleByFirstName(string firstName)
        {
            var response = GetResponse("people-search?first-name=" + firstName);
            return Deserialize<PeopleSearchresult>(response.Content);
        }

        public PeopleSearchresult GetPeopleByLastName(string lastname)
        {
            var response = GetResponse("people-search?last-name=" + lastname);
            return Deserialize<PeopleSearchresult>(response.Content);
        }

        public PeopleSearchresult GetPeopleBySchoolName(string schoolName)
        {
            var response = GetResponse("people-search?school-name=" + schoolName);
            return Deserialize<PeopleSearchresult>(response.Content);
        }

        #endregion

        #region Company information

        public Company GetCompany(int id)
        {
            var response = GetResponse("companies/" + id.ToString());
            return Deserialize<Company>(response.Content);
        }

        public Company GetCompanyByUniversalName(string universalName)
        {
            var response = GetResponse("companies/universal-name=" + universalName);
            return Deserialize<Company>(response.Content);
        }

        public CompanyCollection GetCompaniesByEmailDomain(string emailDomain)
        {
            var response = GetResponse("companies?email-domain=" + emailDomain);
            return Deserialize<CompanyCollection>(response.Content);
        }

        public CompanyCollection GetCompaniesByIdAnduniversalName(string id, string universalName)
        {
            var response = GetResponse("companies::(" + id + ",universal-name=" + universalName + ")");
            return Deserialize<CompanyCollection>(response.Content);
        }

        #endregion


    }
}