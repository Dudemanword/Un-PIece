using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using DatabaseInteractions;

namespace OnePieceAbridged.Controllers
{
    public class YoutubeController : Controller
    {
        // GET: Youtube
        public ActionResult Index()
        {
            var databaseInteraction = new MongoDbInteraction(new MongoDbConnection { CollectionName = "googleAuthorization", DatabaseName = "StrawHatEntertainment" },
                                                             new MongoClient("mongodb://localhost"));
            var results = databaseInteraction.FindAll<BsonDocument>().Result;
            if (results.Count() == 0)
            {
                var authEndpoint = "https://accounts.google.com/o/oauth2/auth?";
                NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                queryString["response_type"] = "code";
                queryString["client_id"] = "585224365875-s7phb7mlduaa5e93r2qojjldcgi4o2vj.apps.googleusercontent.com";
                queryString["redirect_uri"] = "http://localhost:55102/youtube/HandleClientCode";
                queryString["scope"] = "https://www.googleapis.com/auth/youtube";
                queryString["access_type"] = "offline";
                queryString["approval_prompt"] = "force";

                var authorizationUrl = authEndpoint + queryString.ToString();
                return Redirect(authorizationUrl);
            }

            var googleAuthentication = results.Select(x => BsonSerializer.Deserialize<TokenInfo>(x)).ToList()[0];
            
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult HandleClientCode([FromUri]string code)
        {
            var httpClient = new HttpClient();
            var exchangeUri = "https://accounts.google.com/o/oauth2/token";
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString["code"] = code;
            queryString["client_id"] = "585224365875-s7phb7mlduaa5e93r2qojjldcgi4o2vj.apps.googleusercontent.com";
            queryString["client_secret"] = "Naxgfm0K31CxxlFiX8Ch723m";
            queryString["redirect_uri"] = "http://localhost:55102/youtube/HandleClientCode";
            queryString["grant_type"] = "authorization_code";
            
            var content = new StringContent(queryString.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            var exchangePostResponse = httpClient.PostAsync(exchangeUri, content).Result;
            var resp = exchangePostResponse.Content.ReadAsStringAsync().Result;

            var googleResponse = new JavaScriptSerializer().Deserialize<TokenInfo>(resp);

            var databaseInteraction = new MongoDbInteraction(new MongoDbConnection { CollectionName = "googleAuthorization", DatabaseName = "StrawHatEntertainment" }, 
                                                             new MongoClient("mongodb://localhost"));

            var result = databaseInteraction.Log(googleResponse);

            return View("~/Views/Home/Index.cshtml");
        }
        
    }
}