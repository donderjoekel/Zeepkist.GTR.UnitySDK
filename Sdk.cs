using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.SDK.Client;
using TNRD.Zeepkist.GTR.SDK.Implementation;
using UnityEngine.LowLevel;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public class Sdk
{
    public const string DEFAULT_API_ADDRESS = "https://api.zeepkist-gtr.com";
    public const string DEFAULT_AUTH_ADDRESS = "https://auth.zeepkist-gtr.com";
    public const string DEFAULT_GRAPHQL_ADDRESS = "https://graphql.zeepkist-gtr.com";
    public const string DEFAULT_ZWORP_ADDRESS = "https://api.zworpshop.com";

    /// <summary>
    /// Initializes the SDK with the default backend address and no logging
    /// </summary>
    public static Sdk Initialize()
    {
        return Initialize(DEFAULT_API_ADDRESS,
            DEFAULT_AUTH_ADDRESS,
            DEFAULT_GRAPHQL_ADDRESS,
            DEFAULT_ZWORP_ADDRESS,
            null,
            false,
            false);
    }

    /// <summary>
    /// Initializes the SDK with the default backend address and no logging
    /// <param name="discordApplicationId">The application id of a discord application to use to connect to Discord</param>
    /// </summary>
    public static Sdk Initialize(long discordApplicationId)
    {
        return Initialize(DEFAULT_API_ADDRESS,
            DEFAULT_AUTH_ADDRESS,
            DEFAULT_GRAPHQL_ADDRESS,
            DEFAULT_ZWORP_ADDRESS,
            discordApplicationId,
            false,
            false);
    }

    /// <summary>
    /// Initializes the SDK with the default backend address
    /// </summary>
    /// <param name="logRequestUrl">Setting this to true will log the URL of each request</param>
    /// <param name="logResponseOutput">Setting this to true will log the response text of each request</param>
    public static Sdk Initialize(bool logRequestUrl, bool logResponseOutput)
    {
        return Initialize(DEFAULT_API_ADDRESS,
            DEFAULT_AUTH_ADDRESS,
            DEFAULT_GRAPHQL_ADDRESS,
            DEFAULT_ZWORP_ADDRESS,
            null,
            logRequestUrl,
            logResponseOutput);
    }

    /// <summary>
    /// Initializes the SDK with the default backend address
    /// </summary>
    /// <param name="discordApplicationId">The application id of a discord application to use to connect to Discord</param>
    /// <param name="logRequestUrl">Setting this to true will log the URL of each request</param>
    /// <param name="logResponseOutput">Setting this to true will log the response text of each request</param>
    public static Sdk Initialize(long discordApplicationId, bool logRequestUrl, bool logResponseOutput)
    {
        return Initialize(DEFAULT_API_ADDRESS,
            DEFAULT_AUTH_ADDRESS,
            DEFAULT_GRAPHQL_ADDRESS,
            DEFAULT_ZWORP_ADDRESS,
            discordApplicationId,
            logRequestUrl,
            logResponseOutput);
    }

    /// <summary>
    /// Initializes the SDK with a custom backend address
    /// </summary>
    /// <param name="apiAddress">The base address to use for all api requests</param>
    /// <param name="authAddress">The base address to use for all auth requests</param>
    /// <param name="graphqlAddress">THe base address to use for all graphql requests</param>
    /// <param name="zworpAddress">The base address to use for all zworpshop requests</param>
    /// <param name="discordApplicationId">The application id of a discord application to use to connect to Discord</param>
    /// <param name="logRequestUrl">Setting this to true will log the URL of each request</param>
    /// <param name="logResponseOutput">Setting this to true will log the response text of each request</param>
    public static Sdk Initialize(
        string apiAddress,
        string authAddress,
        string graphqlAddress,
        string zworpAddress,
        long? discordApplicationId,
        bool logRequestUrl,
        bool logResponseOutput
    )
    {
        return new Sdk(apiAddress,
            authAddress,
            graphqlAddress,
            zworpAddress,
            discordApplicationId,
            logRequestUrl,
            logResponseOutput);
    }

    internal long? DiscordApplicationId { get; private set; }

    internal AuthClient AuthClient { get; set; }
    internal ApiClient ApiClient { get; set; }

    private GraphQLClient graphQLClient;
    private ZworpClient zworpClient;

    private readonly FavoritesApi favoritesApi;
    private readonly RecordsApi recordsApi;
    private readonly UpvotesApi upvotesApi;
    private readonly UsersApi usersApi;
    private readonly VotesApi votesApi;
    private readonly VersionApi versionApi;
    private readonly PersonalBestApi personalBestApi;
    private readonly WorldRecordApi worldRecordsApi;
    private readonly MediaApi mediaApi;
    private readonly LevelApi levelApi;

    public IFavoritesApi FavoritesApi => favoritesApi;
    public IRecordsApi RecordsApi => recordsApi;
    public IUpvotesApi UpvotesApi => upvotesApi;
    public IUsersApi UsersApi => usersApi;
    public IVotesApi VotesApi => votesApi;
    public IVersionApi VersionApi => versionApi;
    public IPersonalBestApi PersonalBestApi => personalBestApi;
    public IWorldRecordApi WorldRecordApi => worldRecordsApi;
    public IMediaApi MediaApi => mediaApi;
    public ILevelApi LevelApi => levelApi;

    public IGraphQLClient GraphQLClient => graphQLClient;

    public IZworpClient ZworpClient => zworpClient;

    private Sdk(
        string apiAddress,
        string authAddress,
        string graphqlAddress,
        string zworpAddress,
        long? discordApplicationId,
        bool logRequestUrl,
        bool logResponseOutput
    )
    {
        DiscordApplicationId = discordApplicationId;

        // Create the actual ApiClient
        ApiClient = ApiClient.Create(this, apiAddress, logRequestUrl, logResponseOutput);
        AuthClient = AuthClient.Create(this, authAddress);
        graphQLClient = Client.GraphQLClient.Create(this, graphqlAddress);
        zworpClient = Client.ZworpClient.Create(this, zworpAddress, logRequestUrl, logResponseOutput);

        // Initialize the player loop helper, this is to reduce issues with UniTask
        if (!PlayerLoopHelper.IsInjectedUniTaskPlayerLoop())
        {
            PlayerLoopSystem loop = PlayerLoop.GetCurrentPlayerLoop();
            PlayerLoopHelper.Initialize(ref loop);
        }

        favoritesApi = new FavoritesApi(this);
        recordsApi = new RecordsApi(this);
        upvotesApi = new UpvotesApi(this);
        usersApi = new UsersApi(this);
        votesApi = new VotesApi(this);
        versionApi = new VersionApi(this);
        personalBestApi = new PersonalBestApi(this);
        worldRecordsApi = new WorldRecordApi(this);
        mediaApi = new MediaApi(this);
        levelApi = new LevelApi(this);
    }
}
