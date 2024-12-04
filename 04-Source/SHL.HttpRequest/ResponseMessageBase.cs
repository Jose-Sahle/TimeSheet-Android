using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SHL.HttpRequest
{
    public class ResponseMessageBase<T>
    {
        public ResponseMessageBase()
        {
            Return = (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// Lista de retorno da chamada ao serviço de persitência
        /// </summary>
        public T Return { get; set; }


        /// <summary>
        /// Objeto de retorno da execução da requisição receberá sempre System.Net.Http.HttpResponseMessage
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
