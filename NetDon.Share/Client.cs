﻿using NetDon.Enums;
using NetDon.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace NetDon
{
    public class Client : ApiBase
    {
        private readonly string clientId;
        private readonly string clientSecret;


        public Client(string instanceUri, string clientId, string clientSecret)
            : base(new Uri(instanceUri))
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public Client(string instanceUri, string accessToken)
            : base(new Uri(instanceUri), accessToken)
        {
        }

        public Client(Uri instanceUri, string clientId, string clientSecret)
            : base(instanceUri)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public Client(Uri instanceUri, string accessToken)
            : base(instanceUri, accessToken)
        {
        }

        #region GetToken

        /// <summary>
        /// Get an Authorize URI.
        /// 認証に使用する URI を取得します。UWP の場合はこの URI を WebAuthenticationBroker などで使用することにより、アクセス トークンを取得できます。
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public Uri GetAuthorizeUri(string redirectUri, Scope scopes)
        {
            var parameter = $"oauth/authorize?client_id={this.clientId}&" +
                $"grant_type=authorization_code&" +
                $"response_type=code&" +
                $"redirect_uri={Uri.EscapeUriString(redirectUri)}";
            var scopeStr = scopes.ToScopeStrings();
            if (!string.IsNullOrWhiteSpace(scopeStr))
            {
                parameter += "&scopes=" + scopeStr;
            }

            return new Uri(instanceUri, Uri.EscapeUriString(parameter));
        }

        public async Task<string> GetAccessToken(string authCode, string redirectUri = "")
        {
            var endpoint = new Uri(this.instanceUri, "/oauth/token");
            var data = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code"},
                { "client_id", this.clientId },
                { "client_secret", this.clientSecret},
                { "code", authCode }
            };

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                data.Add("redirect_uri", redirectUri);
            }

            var requestData = new FormUrlEncodedContent(data);

            var http = CreateClient();
            var response = await http.PostAsync(endpoint, requestData);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }

            return null;
        }

        #endregion

        #region Accounts

        /// <summary>
        /// Get an Current User Information. 現在ログインしているユーザーの情報を取得します。
        /// </summary>
        /// <returns></returns>
        public async Task<AccountModel> GetCurrentUserAsync()
        {
            var endpoint = CreateUriBase("/accounts/verify_credentials");
            var result = await GetAsync<AccountModel>(endpoint);

            return result;
        }

        /// <summary>
        /// Get an User Information.
        /// 指定されたユーザーの情報を取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AccountModel> GetUserAsync(long id)
        {
            var endpoint = CreateUriBase("/accounts/" + id);
            var result = await GetAsync<AccountModel>(endpoint);

            return result;
        }

        /// <summary>
        /// Get an account's following.
        /// 指定されたユーザーのフォローしているユーザーの一覧を取得します。
        /// </summary>
        /// <param name="id">User ID. 取得したいユーザーの ID。</param>
        /// <param name="maxId">Get a list of followers with ID less than or equal this value. 取得するフォロー ユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of followers with ID greater than this value. 取得するフォロー ユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="limit">Maximum number of accounts to get (Default 40, Max 80) 取得するフォロー ユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetUserFollowing(long id, long? maxId = null, long? sinceId = null, int limit = 40)
        {
            return await GetUserFollows("following", id, maxId, sinceId, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">User ID. 取得したいユーザーの ID。</param>
        /// <param name="maxId">Get a list of followers with ID less than or equal this value. 取得するフォロワー ユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of followers with ID greater than this value. 取得するフォロワー ユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="limit">Maximum number of accounts to get (Default 40, Max 80) 取得するフォロワー ユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetUserFollowers(long id, long? maxId = null, long? sinceId = null, int limit = 40)
        {
            return await GetUserFollows("followers", id, maxId, sinceId, limit);
        }

        public async Task<RelationshipModel> GetRelationShipsAsync(long id)
        {
            var endpoint = CreateUriBase("/accounts/relationships?id=" + id);
            var result = await GetAsync<IEnumerable<RelationshipModel>>(endpoint);

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<RelationshipModel>> GetRelationShipsAsync(long[] ids)
        {
            var endpoint = CreateUriBase("/accounts/relationships?id[]=" + string.Join("&id[]=", ids));
            var result = await GetAsync<IEnumerable<RelationshipModel>>(endpoint);

            return result;
        }

        private async Task<IEnumerable<AccountModel>> GetUserFollows(string apiName, long id, long? maxId, long? sinceId, int limit)
        {
            var parameters = "/accounts/" + id + "/" + apiName;

            // limit は最大 80 まで
            if (limit > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(limit));
            }

            parameters += "?limit=" + limit;

            if (maxId.HasValue)
            {
                parameters += "&max_id=" + maxId;
            }
            if (sinceId.HasValue)
            {
                parameters += "&since_id=" + sinceId;
            }

            var endpoint = CreateUriBase(parameters);
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint);
            return result;
        }

        private async Task<string> GetRelationshipsAsync(Uri endpoint)
        {
            var http = CreateClient();

            var response = await http.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }

            return null;
        }

        #endregion

        #region Timelines

        /// <summary>
        /// Get current home timeline. 現在のホーム タイムラインを取得します。
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StatusModel>> GetHomeTimelineAsync()
        {
            var endpoint = CreateUriBase("/timelines/home");
            var result = await GetAsync<IEnumerable<StatusModel>>(endpoint);
            return result;
        }

        #endregion
    }
}
