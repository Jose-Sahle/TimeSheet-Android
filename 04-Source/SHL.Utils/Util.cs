using SHL.HttpRequest;
using SHL.IRetorno.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using SHL.TokenSecurity;
using System.Threading.Tasks;

namespace SHL.Utils
{
    public static class Util
    {
        public static Object SendPostT<T>(string url, Object oObject, List<HttpHeaders> headears, String mediaType, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;
            String httpoperator = "?";

            try
            {
                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += httpoperator + p + "=";
                        httpoperator = "&";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.PostT<T>(request, true, headears, mediaType);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object SendPutT<T>(string url, Object oObject, List<HttpHeaders> headers, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;
            String httpoperator = "?";

            try
            {
                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += httpoperator + p + "=";
                        httpoperator = "&";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.PutT<T>(request, true, headers);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerSelect<T>(string group, string url, string controller, string metodo, params string[] listparam)
        {
            Object ret = null;
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo="+group;
                
                foreach(String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.RequestUrl = url;

                response = SHLHttpRequest.Get(request);

                if (response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var result = response.HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    ret = JsonConvert.DeserializeObject<T>(result);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }

        public static Object ControllerPost<T>(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            Object ret = null;
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();           
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.RequestUrl = url;
                request.Object = oObject;

                response = SHLHttpRequest.PostT<T>(request, true, null, "application/json");

                if (response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var result = response.HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    ret = JsonConvert.DeserializeObject<T>(result);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }
        
         public static Object ControllerPut<T>(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            Object ret = null;
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.RequestUrl = url;
                request.Object = oObject;

                response = SHLHttpRequest.PutT<T>(request, true, null, "application/json");

                if (response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var result = response.HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    ret = JsonConvert.DeserializeObject<T>(result);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }

        public static Object ControllerSelectEx<T>(string url, string controller, string metodo, params string[] listparam)
        {
            Object ret = null;
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.RequestUrl = url;

                response = SHLHttpRequest.Get(request);

                if (response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var result = response.HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    ret = JsonConvert.DeserializeObject<T>(result);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }

        public static Object ControllerPost(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.Post(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPostEx(Boolean serialize, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.Post(request, serialize);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPostT<T>(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.PostT<T>(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPostExT<T>(Boolean serialize, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.PostT<T>(request, serialize);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPut(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.Put(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPutEx(string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.Put(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPutT<T>(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.PutT<T>(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerPutExT<T>(string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.PutT<T>(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerDelete(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.Delete(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerDeleteEx(string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessage response = new ResponseMessage();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.Delete(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerDeleteT<T>(string group, string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;

            try
            {
                url += controller + "/" + metodo + "?grupo=" + group;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += "&" + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.DeleteT<T>(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }

        public static Object ControllerDeleteExT<T>(string url, string controller, string metodo, Object oObject, params string[] listparam)
        {
            RequestMessage request = new RequestMessage();
            ResponseMessageBase<T> response = new ResponseMessageBase<T>();
            Boolean isimpar = true;
            String operador = "?";

            try
            {
                url += controller + "/" + metodo;

                foreach (String p in listparam)
                {
                    if (isimpar)
                    {
                        isimpar = false;
                        url += operador + p + "=";
                    }
                    else
                    {
                        isimpar = true;
                        url += System.Web.HttpUtility.UrlEncode(p);
                    }

                    operador = "&";
                }

                request = new RequestMessage();
                request.Object = oObject;
                request.RequestUrl = url;

                response = SHLHttpRequest.DeleteT<T>(request);

            }
            catch (Exception)
            {

                throw;
            }

            return response.Return;
        }
        
    }
}
