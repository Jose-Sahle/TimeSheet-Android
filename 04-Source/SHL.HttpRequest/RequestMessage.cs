using System;
using System.Collections.Generic;
using System.Text;

namespace SHL.HttpRequest
{
    /// <summary>
    /// Request message.
    /// </summary>
    public class RequestMessage
    {
        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        /// <value>The object.</value>
        public object Object { get; set; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>The request URL.</value>
        public String RequestUrl { get; set; }
    }
}
