
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Netsparker.Core.RequestManager
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        #region Private Members

        private IHttpRawRequestParser m_RequestParser;

        #endregion

        #region Public Members

        public event RequestExecutedHandler OnRequestExecuted;

        #endregion
        
        #region Private Methods

        private void onRequestExecuted(IAsyncResult asyncResult)
        {
            HttpWebRequest request = (HttpWebRequest)asyncResult.AsyncState;
            string responseData = String.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult))
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    responseData = streamReader.ReadToEnd();

                    if (OnRequestExecuted != null)
                        OnRequestExecuted(this, new RequestExecutedEventArgs { StatusCode = (int)response.StatusCode, Status = response.StatusDescription, Host = request.Host, URL = response.ResponseUri.ToString(), Headers = response.Headers.ToString(), Body = responseData });
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    if (OnRequestExecuted != null)
                        OnRequestExecuted(this, new RequestExecutedEventArgs { StatusCode = (int)response.StatusCode, Status = response.StatusDescription, Host = request.Host, URL = response.ResponseUri.ToString(), Headers = response.Headers.ToString(), Body = responseData });
                }
            }
        }


        #endregion
        
        #region Public Methods

        public HttpRequestBuilder(IHttpRawRequestParser requestParser)
        {
            m_RequestParser = requestParser;
        }

        public void ExecuteRequest(string rawRequest)
        {
            IWebRequestProxy requestProxy = m_RequestParser.Parse(rawRequest);

            WebRequest request = (requestProxy as WebRequestProxy).Request;

            request.BeginGetResponse(new AsyncCallback(onRequestExecuted), request);
        } 
        
        #endregion
    }
}
