using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using SHL.IRetorno.Model;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace SHL.HttpRequest
{
    /// <summary>
    /// SHLERPH ttp request.
    /// </summary>
    public class SHLHttpRequest
    {
        public static ResponseMessage Post(RequestMessage request, Boolean serialize = true, List<HttpHeaders> headers = null, String mediaType = "application/json")
        {
            ResponseMessage response = new ResponseMessage();
            HttpContent content = null;
            String resultContent = String.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    String jsonData = null;

                    if (mediaType == "application/json")
                    {
                        if (serialize)
                            jsonData = JsonConvert.SerializeObject(request.Object);
                        else
                            jsonData = request.Object.ToString();

                        content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                    }
                    else if (mediaType == "application/x-www-form-urlencoded")
                    {
                        var dict = new Dictionary<string, string>();

                        PropertyInfo[] properties = request.Object.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            String value = propertyInfo.GetValue(request.Object).ToString();
                            dict.Add(propertyInfo.Name, value);
                        }

                        content = new FormUrlEncodedContent(dict);
                    }

                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    var responseMessage = client.PostAsync(request.RequestUrl, content).GetAwaiter().GetResult(); ;
                    response.HttpResponseMessage = responseMessage;
                    resultContent = responseMessage.Content.ReadAsStringAsync().Result;
                    if (resultContent != null)
                        response.Return = JsonConvert.DeserializeObject<List<RETORNO>>(resultContent);
                }
            }
            catch (Exception oException)
            {
                RETORNO oRETORNO = new RETORNO();
                oRETORNO.transacao = "";
                oRETORNO.status = "9";
                oRETORNO.mensagem = oException.Message;
                oRETORNO.dt_retorno = DateTime.Now;
                response.Return.Add(oRETORNO);
            }
            return response;
        }

        public static ResponseMessageBase<T> PostT<T>(RequestMessage request, Boolean serialize = true, List<HttpHeaders> headers = null, String mediaType = "application/json")
        {
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            HttpContent content = null;
            String resultContent = String.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    String jsonData = null;

                    if (mediaType == "application/json")
                    {
                        if (serialize)
                            jsonData = JsonConvert.SerializeObject(request.Object);
                        else
                            jsonData = request.Object.ToString();

                        content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                    }
                    else if (mediaType == "application/x-www-form-urlencoded")
                    {
                        var dict = new Dictionary<string, string>();

                        PropertyInfo[] properties = request.Object.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            String value = propertyInfo.GetValue(request.Object).ToString();
                            dict.Add(propertyInfo.Name, value);
                        }

                        content = new FormUrlEncodedContent(dict);
                    }

                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    var responseMessage = client.PostAsync(request.RequestUrl, content).GetAwaiter().GetResult(); ;
                    response.HttpResponseMessage = responseMessage;
                    resultContent = responseMessage.Content.ReadAsStringAsync().Result;
                    if (resultContent != null)
                        response.Return = JsonConvert.DeserializeObject<T>(resultContent);
                }
            }
            catch (Exception oException)
            {
                throw new Exception(resultContent + "\n\n" + oException.Message);
            }

            return response;
        }

        public static ResponseMessage Get(RequestMessage request, List<HttpHeaders> headers = null)
        {
            ResponseMessage response = new ResponseMessage();

            try
            {
                using (var client = new HttpClient())
                {
                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    var responseMessage = client.GetAsync(request.RequestUrl).GetAwaiter().GetResult();
                    response.HttpResponseMessage = responseMessage;
                }
            }
            catch (Exception oException)
            {
                RETORNO oRETORNO = new RETORNO();
                oRETORNO.transacao = "";
                oRETORNO.status = "9";
                oRETORNO.mensagem = oException.Message;
                oRETORNO.dt_retorno = DateTime.Now;
            }

            return response;
        }

        public static ResponseMessage Put(RequestMessage request, Boolean serialize = true, List<HttpHeaders> headers = null, String mediaType = "application/json")
        {
            ResponseMessage response = new ResponseMessage();
            HttpContent content = null;
            String resultContent = String.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    String jsonData = null;

                    if (mediaType == "application/json")
                    {
                        if (serialize)
                            jsonData = JsonConvert.SerializeObject(request.Object);
                        else
                            jsonData = request.Object.ToString();

                        content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                    }
                    else if (mediaType == "application/x-www-form-urlencoded")
                    {
                        var dict = new Dictionary<string, string>();

                        PropertyInfo[] properties = request.Object.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            String value = propertyInfo.GetValue(request.Object).ToString();
                            dict.Add(propertyInfo.Name, value);
                        }

                        content = new FormUrlEncodedContent(dict);
                    }

                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    var responseMessage = client.PutAsync(request.RequestUrl, content).GetAwaiter().GetResult(); ;
                    response.HttpResponseMessage = responseMessage;
                    resultContent = responseMessage.Content.ReadAsStringAsync().Result;
                    if (resultContent != null)
                        response.Return = JsonConvert.DeserializeObject<List<RETORNO>>(resultContent);
                }
            }
            catch (Exception oException)
            {
                RETORNO oRETORNO = new RETORNO();
                oRETORNO.transacao = "";
                oRETORNO.status = "9";
                oRETORNO.mensagem = oException.Message;
                oRETORNO.dt_retorno = DateTime.Now;
                response.Return.Add(oRETORNO);
            }

            return response;
        }

        public static ResponseMessageBase<T> PutT<T>(RequestMessage request, Boolean serialize = true, List<HttpHeaders> headers = null, String mediaType = "application/json")
        {
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            HttpContent content = null;
            String resultContent = String.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    String jsonData = null;

                    if (mediaType == "application/json")
                    {
                        if (serialize)
                            jsonData = JsonConvert.SerializeObject(request.Object);
                        else
                            jsonData = request.Object.ToString();

                        content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                    }
                    else if (mediaType == "application/x-www-form-urlencoded")
                    {
                        var dict = new Dictionary<string, string>();

                        PropertyInfo[] properties = request.Object.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            String value = propertyInfo.GetValue(request.Object).ToString();
                            dict.Add(propertyInfo.Name, value);
                        }

                        content = new FormUrlEncodedContent(dict);
                    }

                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    var responseMessage = client.PutAsync(request.RequestUrl, content).GetAwaiter().GetResult(); ;
                    response.HttpResponseMessage = responseMessage;
                    resultContent = responseMessage.Content.ReadAsStringAsync().Result;
                    if (resultContent != null)
                        response.Return = JsonConvert.DeserializeObject<T>(resultContent);
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }

            return response;
        }
        
        public static ResponseMessage Delete(RequestMessage request, Boolean serialize = true, List<HttpHeaders> headers = null, String mediaType = "application/json")
        {
            ResponseMessage response = new ResponseMessage();
            HttpContent content = null;
            String resultContent = String.Empty;
            HttpRequestMessage sendrequest = new HttpRequestMessage();
            
            try
            {
                using (var client = new HttpClient())
                {
                    String jsonData = null;

                    if (mediaType == "application/json")
                    {
                        if (serialize)
                            jsonData = JsonConvert.SerializeObject(request.Object);
                        else
                            jsonData = request.Object.ToString();

                        content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                    }
                    else if (mediaType == "application/x-www-form-urlencoded")
                    {
                        var dict = new Dictionary<string, string>();

                        PropertyInfo[] properties = request.Object.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            String value = propertyInfo.GetValue(request.Object).ToString();
                            dict.Add(propertyInfo.Name, value);
                        }

                        content = new FormUrlEncodedContent(dict);
                    }

                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    sendrequest.Method = HttpMethod.Delete;
                    sendrequest.RequestUri = new Uri(request.RequestUrl);
                    sendrequest.Content = content;
                    
                    var responseMessage = client.SendAsync(sendrequest).GetAwaiter().GetResult(); ;
                    response.HttpResponseMessage = responseMessage;
                    resultContent = responseMessage.Content.ReadAsStringAsync().Result;
                    if (resultContent != null)
                        response.Return = JsonConvert.DeserializeObject<List<RETORNO>>(resultContent);
                }
            }
            catch (Exception oException)
            {
                RETORNO oRETORNO = new RETORNO();
                oRETORNO.transacao = "";
                oRETORNO.status = "9";
                oRETORNO.mensagem = oException.Message;
                oRETORNO.dt_retorno = DateTime.Now;
                response.Return.Add(oRETORNO);
            }

            return response;
        }

        public static ResponseMessageBase<T> DeleteT<T>(RequestMessage request, Boolean serialize = true, List<HttpHeaders> headers = null, String mediaType = "application/json")
        {
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            HttpContent content = null;
            String resultContent = String.Empty;
            HttpRequestMessage sendrequest = new HttpRequestMessage();
            
            try
            {
                using (var client = new HttpClient())
                {
                    String jsonData = null;

                    if (mediaType == "application/json")
                    {
                        if (serialize)
                            jsonData = JsonConvert.SerializeObject(request.Object);
                        else
                            jsonData = request.Object.ToString();

                        content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                    }
                    else if (mediaType == "application/x-www-form-urlencoded")
                    {
                        var dict = new Dictionary<string, string>();

                        PropertyInfo[] properties = request.Object.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            String value = propertyInfo.GetValue(request.Object).ToString();
                            dict.Add(propertyInfo.Name, value);
                        }

                        content = new FormUrlEncodedContent(dict);
                    }

                    if (headers == null)
                    {
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        foreach (HttpHeaders header in headers)
                        {
                            if (header.key.ToUpper() == "AUTHORIZATION")
                            {
                                String[] authenticationproperty = header.value.Split(':');
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationproperty[0].Trim(), authenticationproperty[1].Trim());
                            }
                            else
                                client.DefaultRequestHeaders.Add(header.key, header.value);
                        }
                    }

                    sendrequest.Method = HttpMethod.Delete;
                    sendrequest.RequestUri = new Uri(request.RequestUrl);
                    sendrequest.Content = content;
                    
                    var responseMessage = client.SendAsync(sendrequest).GetAwaiter().GetResult();
                    
                    response.HttpResponseMessage = responseMessage;
                    resultContent = responseMessage.Content.ReadAsStringAsync().Result;
                    if (resultContent != null)
                        response.Return = JsonConvert.DeserializeObject<T>(resultContent);
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }

            return response;
        }
    }
}
