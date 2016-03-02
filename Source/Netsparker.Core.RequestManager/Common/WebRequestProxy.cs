using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Netsparker.Core.RequestManager
{
    // Wrapper class for built-in WebRequest class
    public class WebRequestProxy : IWebRequestProxy
    {
        #region Private Members

        private HttpWebRequest m_Request;
        private const string HEADER_SEPERATOR = "-";
        private const string VERSION_PREFIX = "HTTP/";
        private const string KEEP_ALIVE = "keep-alive";
        private const string CLOSE = "close";


        #endregion

        #region Constructors

        public WebRequestProxy(string uri)
        {
            m_Request = WebRequest.CreateHttp(uri);
            m_Request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
        }

        #endregion

        #region Public Methods

        public void AddHeader(string key, string value)
        {
            HttpRequestHeader header;

            // Replace - chars in header key to get HttpRequestHeader enum string
            string headerEnum = key.Replace(HEADER_SEPERATOR, String.Empty);

            // Try header key to parse into HttpRequestHeader
            bool properlyParsed = Enum.TryParse<HttpRequestHeader>(headerEnum, true, out header);

            // If it's properly parsed then it's a standard header
            if (properlyParsed)
            {
                addStandardHeader(header, value);
            }
            else
            {
                // Add non-standard header as string (key, value) pair
                m_Request.Headers.Add(key, value);
            }
        }

        public void SetMethod(string method)
        {
            m_Request.Method = method;
        }

        public void SetVersion(string version)
        {
            // Remove version prefix to create Version instance
            m_Request.ProtocolVersion = new Version(version.Replace(VERSION_PREFIX, String.Empty));
        }

        public void SetBody(string body)
        {
            byte[] data = Encoding.ASCII.GetBytes(body);

            using (Stream requestStream = m_Request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }
        }

        #endregion
        
        #region Private Methods

        private void addStandardHeader(HttpRequestHeader header, string value)
        {
            try
            {
                // Some headers could only be set by proper methods or properties
                // Check if the header key is restricted that way if so add restricted header
                if (WebHeaderCollection.IsRestricted(header.ToString()))
                {
                    addRestrictedHeader(header, value);
                }
                else
                {
                    // If the header key is not restricted then add it to header collection directly
                    m_Request.Headers.Add(header, value);
                }
            }
            catch (ArgumentException) // Somehow some of the restricted headers aren't marked as Restricted so if any exception occurs add them as restricted header
            {
                addRestrictedHeader(header, value);
            }
        }

        private void addRestrictedHeader(HttpRequestHeader header, string value)
        {
            switch (header)
            {
                case HttpRequestHeader.Accept:
                    m_Request.Accept = value;
                    break;
                case HttpRequestHeader.Connection:
                    // CAUTION: Ignores multiple values in the Connection header (e.g. Connection: Keep-Alive, Persist)
                    // TODO: Fix the issue
                    if (Regex.Match(value, KEEP_ALIVE, RegexOptions.IgnoreCase) != null)
                        m_Request.KeepAlive = true;
                    else if(Regex.Match(value, CLOSE, RegexOptions.IgnoreCase) != null)
                        m_Request.KeepAlive = false;
                    else
                        m_Request.Connection = value;
                    break;
                case HttpRequestHeader.ContentLength:
                    m_Request.ContentLength = Convert.ToInt64(value);
                    break;
                case HttpRequestHeader.ContentType:
                    m_Request.ContentType = value;
                    break;
                case HttpRequestHeader.Date:
                    m_Request.Date = DateTime.Parse(value);
                    break;
                case HttpRequestHeader.Expect:
                    m_Request.Expect = value;
                    break;
                case HttpRequestHeader.Host:
                    m_Request.Host = value;
                    break;
                case HttpRequestHeader.IfModifiedSince:
                    m_Request.IfModifiedSince = DateTime.Parse(value);
                    break;
                case HttpRequestHeader.Referer:
                    m_Request.Referer = value;
                    break;
                case HttpRequestHeader.TransferEncoding:
                    m_Request.TransferEncoding = value;
                    break;
                case HttpRequestHeader.UserAgent:
                    m_Request.UserAgent = value;
                    break;
                default:
                    // e.g. "Range" header is not supported
                    throw new NotSupportedException(String.Format("{0} header is not supported.", header.ToString()));

            }
        }

        #endregion

        #region Internal Members

        internal WebRequest Request
        {
            get { return m_Request; }
        }

        #endregion

    }
}
