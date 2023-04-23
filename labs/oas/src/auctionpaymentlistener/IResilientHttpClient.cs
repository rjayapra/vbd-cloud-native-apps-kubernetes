using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuctionPaymentListener
{
    public interface IResilientHttpClient
    {
        HttpResponseMessage Get(string uri, string authToken = null);

        HttpResponseMessage Post<T>(string uri, T item, string authToken = null);

        HttpResponseMessage Delete(string uri, string authToken = null); 

        HttpResponseMessage Put<T>(string uri, T item, string authToken = null);
    }
}
