using NetDon.Enums;
using NetDon.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

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
                parameter += "&scope=" + scopeStr;
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
        /// Get an account's followers.
        /// 指定されたユーザーをフォローしているユーザーの一覧を取得します。
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

        /// <summary>
        /// Get an account's relationships.
        /// 指定されたユーザーと、現在ログインしているユーザーとの関係を取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RelationshipModel> GetRelationShipsAsync(long id)
        {
            var endpoint = CreateUriBase("/accounts/relationships?id=" + id);
            var result = await GetAsync<IEnumerable<RelationshipModel>>(endpoint);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Get an account's relationships.
        /// 指定されたユーザーと、現在ログインしているユーザーとの関係を取得します。
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RelationshipModel>> GetRelationShipsAsync(long[] ids)
        {
            var endpoint = CreateUriBase("/accounts/relationships?id[]=" + string.Join("&id[]=", ids));
            var result = await GetAsync<IEnumerable<RelationshipModel>>(endpoint);

            return result;
        }

        /// <summary>
        /// Get result of search for accounts.
        /// アカウントの検索結果を取得します。
        /// </summary>
        /// <param name="query"></param>
        /// <param name="lim"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> SearchAccountAsync(string query, int lim = 40)
        {
            var endpoint = CreateUriBase("/accounts/search?" + CreateGetParameters(q => query, limit => lim));
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint);

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

        #region Blocks

        /// <summary>
        /// Get own blocks list.
        /// 現在ログインしているユーザーのブロック リストを取得します。
        /// </summary>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 40, Max 80) 取得するユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetBlocksAsync(long? maxId = null, long? sinceId = null, int lim = 40)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 80 まで
            if (lim > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/blocks?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint);

            return result;
        }

        #endregion

        #region Favorites

        /// <summary>
        /// Get own favorites list.
        /// 現在ログインしているユーザーのお気に入りリストを取得します。
        /// </summary>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 40, Max 80) 取得するユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<StatusModel>> GetFavoritesAsync(long? maxId = null, long? sinceId = null, int lim = 40)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 80 まで
            if (lim > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/favourites?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<StatusModel>>(endpoint);

            return result;
        }

        #endregion

        #region Follow Requests

        /// <summary>
        /// Get follow requests list.
        /// フォロー リクエストを出しているユーザーの一覧を取得します。
        /// </summary>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 40, Max 80) 取得するユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetFollowRequestsAsync(long? maxId = null, long? sinceId = null, int lim = 40)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 80 まで
            if (lim > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/follow_requests?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint);

            return result;
        }

        #endregion

        #region Mutes

        /// <summary>
        /// Get muted users list.
        /// ミュートしているユーザーの一覧を取得します。
        /// </summary>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 40, Max 80) 取得するユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetMutesAsync(long? maxId = null, long? sinceId = null, int lim = 40)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 80 まで
            if (lim > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/mutes?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint);

            return result;
        }

        #endregion

        #region Notifications

        /// <summary>
        /// Get notification by <paramref name="id"/>.
        /// 指定された通知 ID の情報を取得します。
        /// </summary>
        /// <param name="id">Notification ID. 通知 ID。</param>
        /// <returns></returns>
        public async Task<NotificationModel> GetNotificationsAsync(long id)
        {
            var endpoint = CreateUriBase("/notifications/" + id);
            var result = await GetAsync<NotificationModel>(endpoint);

            return result;
        }

        /// <summary>
        /// Get notifications list.
        /// 通知一覧を取得します。
        /// </summary>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 15, Max 30) 取得するユーザーの数を指定します。既定値は 15 で、最大値は 30 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<NotificationModel>> GetNotificationsAsync(long? maxId = null, long? sinceId = null, int lim = 15)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 30 まで
            if (lim > 30)
            {
                throw new ArgumentException("Limit exeeded. Max 30.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/notifications?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<NotificationModel>>(endpoint);

            return result;
        }

        #endregion

        #region Reports

        /// <summary>
        /// Get reported list.
        /// 報告した一覧を取得します。
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ReportModel>> GetReportsAsync()
        {
            var endpoint = CreateUriBase("/reports");
            var result = await GetAsync<IEnumerable<ReportModel>>(endpoint);

            return result;
        }

        #endregion

        #region Search

        /// <summary>
        /// Get result search matched content.
        /// 検索に一致した情報を取得します。
        /// </summary>
        /// <param name="query">Search query. 検索に使用するクエリ。</param>
        /// <param name="resolveRemote">Wether to resolve remote-instances accounts. リモート インスタンスも検索に含めるか。</param>
        /// <returns></returns>
        public async Task<ResultModel> SearchAsync(string query, bool resolveRemote)
        {
            var endpoint = CreateUriBase("/search?" + CreateGetParameters(q => query, resolve => resolveRemote));
            var result = await GetAsync<ResultModel>(endpoint);

            return result;
        }

        #endregion

        #region Statuses

        public async Task<StatusModel> GetStatusAsync(long id)
        {
            var endpoint = CreateUriBase("/statuses/" + id);
            var result = await GetAsync<StatusModel>(endpoint, false);

            return result;
        }

        public async Task<ContextModel> GetStatusContextAsync(long id)
        {
            var endpoint = CreateUriBase("/statuses/" + id + "/context");
            var result = await GetAsync<ContextModel>(endpoint, false);

            return result;
        }

        public async Task<CardModel> GetStatusCardAsync(long id)
        {
            var endpoint = CreateUriBase("/statuses/" + id + "/card");
            var result = await GetAsync<CardModel>(endpoint, false);

            return result;
        }

        /// <summary>
        /// Get who boosted a status list.
        /// 指定されたトゥートをブーストしたアカウントの一覧を取得します。
        /// </summary>
        /// <param name="id">Toot ID. トゥート ID</param>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 40, Max 80) 取得するユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetRebloggedByAsync(long id, long? maxId = null, long? sinceId = null, int lim = 40)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 80 まで
            if (lim > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/statuses/"+id+"/reblogged_by?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint, false);

            return result;
        }

        /// <summary>
        /// Get who favorited a status list.
        /// 指定されたトゥートをお気に入りしたアカウントの一覧を取得します。
        /// </summary>
        /// <param name="id">Toot ID. トゥート ID</param>
        /// <param name="maxId">Get a list of accounts with ID less than or equal this value. 取得するユーザーのうち、ID が指定以下のユーザーにフィルターします。</param>
        /// <param name="sinceId">Get a list of accounts with ID greater than this value. 取得するユーザーのうち、ID が指定以上のユーザーにフィルターします。</param>
        /// <param name="lim">Maximum number of accounts to get (Default 40, Max 80) 取得するユーザーの数を指定します。既定値は 40 で、最大値は 80 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<AccountModel>> GetFavoritedByAsync(long id, long? maxId = null, long? sinceId = null, int lim = 40)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 80 まで
            if (lim > 80)
            {
                throw new ArgumentException("Limit exeeded. Max 80.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/statuses/" + id + "/favourited_by?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<AccountModel>>(endpoint, false);

            return result;
        }

        #endregion

        #region Timelines

        /// <summary>
        /// Get current home timeline. 現在のホーム タイムラインを取得します。
        /// </summary>
        /// <param name="maxId">Get a list of status with ID less than or equal this value. 取得するトゥートのうち、ID が指定以下のトゥートにフィルターします。</param>
        /// <param name="sinceId">Get a list of status with ID greater than this value. 取得するトゥートのうち、ID が指定以上のトゥートにフィルターします。</param>
        /// <param name="lim">Maximum number of status to get (Default 20, Max 40) 取得するトゥートの数を指定します。既定値は 20 で、最大値は 40 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<StatusModel>> GetHomeTimelineAsync(bool localOnly = false, long? maxId = null, long? sinceId = null, int lim = 20)
        {
            return await GetTimelineAsync("/timelines/home", localOnly, maxId, sinceId, lim);
        }

        /// <summary>
        /// Get current federate timeline. 現在の連合タイムラインを取得します。
        /// </summary>
        /// <param name="maxId">Get a list of status with ID less than or equal this value. 取得するトゥートのうち、ID が指定以下のトゥートにフィルターします。</param>
        /// <param name="sinceId">Get a list of status with ID greater than this value. 取得するトゥートのうち、ID が指定以上のトゥートにフィルターします。</param>
        /// <param name="lim">Maximum number of status to get (Default 20, Max 40) 取得するトゥートの数を指定します。既定値は 20 で、最大値は 40 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<StatusModel>> GetPublicTimelineAsync(bool localOnly = false, long? maxId = null, long? sinceId = null, int lim = 20)
        {
            return await GetTimelineAsync("/timelines/public", localOnly, maxId, sinceId, lim);
        }

        /// <summary>
        /// Get current hashtag timeline. 指定したハッシュタグによる現在のタイムラインを取得します。
        /// </summary>
        /// <param name="maxId">Get a list of status with ID less than or equal this value. 取得するトゥートのうち、ID が指定以下のトゥートにフィルターします。</param>
        /// <param name="sinceId">Get a list of status with ID greater than this value. 取得するトゥートのうち、ID が指定以上のトゥートにフィルターします。</param>
        /// <param name="lim">Maximum number of status to get (Default 20, Max 40) 取得するトゥートの数を指定します。既定値は 20 で、最大値は 40 です。</param>
        /// <returns></returns>
        public async Task<IEnumerable<StatusModel>> GetHashtagTimelineAsync(string hashtag, long? maxId = null, long? sinceId = null, int lim = 20)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 40 まで
            if (lim > 40)
            {
                throw new ArgumentException("Limit exeeded. Max 40.", nameof(lim));
            }

            parameters.Add(limit => lim);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase("/timelines/tag/" + hashtag + "?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<StatusModel>>(endpoint);
            return result;
        }

        private async Task<IEnumerable<StatusModel>> GetTimelineAsync(string endpointName, bool localOnly, long? maxId, long? sinceId, int lim)
        {
            var parameters = new List<Expression<Func<object, object>>>();

            // limit は最大 40 まで
            if (lim > 40)
            {
                throw new ArgumentException("Limit exeeded. Max 40.", nameof(lim));
            }

            parameters.Add(limit => lim);

            parameters.Add(local => localOnly);

            if (maxId.HasValue)
            {
                parameters.Add(max_id => maxId.Value);
            }
            if (sinceId.HasValue)
            {
                parameters.Add(since_id => sinceId.Value);
            }

            var endpoint = CreateUriBase(endpointName + "?" + CreateGetParameters(parameters.ToArray()));
            var result = await GetAsync<IEnumerable<StatusModel>>(endpoint);
            return result;
        }

        #endregion
    }
}
