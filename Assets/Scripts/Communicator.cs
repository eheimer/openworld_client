using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEngine;

namespace Openworld
{
  public class Communicator : MonoBehaviour
  {
    private const string prodUrl = "https://www.openworld-game.com";
    private const string devUrl = "http://localhost:3001";
    [SerializeField] public string baseUrl = prodUrl;
    [SerializeField] public string apiPath = "/api/v1";
    protected GameManager gameManager;

    protected void Start()
    {
      gameManager = FindObjectOfType<GameManager>();
    }

    public void SetIsDevUrl(bool isDevUrl)
    {
      baseUrl = isDevUrl ? devUrl : prodUrl;
    }

    private RequestHelper getBasicRequest(string path, bool root = false)
    {
      string url = root ? baseUrl + path : baseUrl + apiPath + path;
      return new RequestHelper
      {
        Uri = url,
        EnableDebug = gameManager.debugApi
      };
    }

    private RequestHelper getAuthorizedRequest(string path, bool root = false)
    {
      RequestHelper req = getBasicRequest(path, root);
      var headers = new Dictionary<string, string>();
      headers.Add("Authorization", "Bearer " + gameManager.GetAuthToken());
      req.Headers = headers;
      return req;
    }

    private void HandleError(Exception err, Action<RequestException> handler)
    {
      handler((RequestException)err);
    }

    public void Login(string username, string password, Action<LoginResponse> success, Action<RequestException> error)
    {
      RestClient.Post<LoginResponse>(LoginRequest(username, password))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void Register(string email, string name, string password, Action<PlayerResponse> success, Action<RequestException> error)
    {
      RestClient.Post<PlayerResponse>(RegisterRequest(email, name, password))
      .Then(res => success(res))
      .Catch(err => HandleError(err, error));
    }

    public void GetPlayer(string id, Action<PlayerResponse> success, Action<RequestException> error)
    {
      RestClient.Get<PlayerResponse>(PlayerRequest(id))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void GetPlayerDetail(string id, Action<PlayerDetailResponse> success, Action<RequestException> error)
    {
      RestClient.Get<PlayerDetailResponse>(PlayerDetailRequest(id))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void GetGames(string playerId, Action<GamesResponse[]> success, Action<RequestException> error)
    {
      RestClient.GetArray<GamesResponse>(GamesRequest(playerId))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void CreateGame(string name, int maxPlayers, Action<ResponseHelper> success, Action<RequestException> error)
    {
      RestClient.Post<ResponseHelper>(CreateGameRequest(name, maxPlayers))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void InvitePlayer(string gameId, string email, Action<ResponseHelper> success, Action<RequestException> error)
    {
      RestClient.Post<ResponseHelper>(InvitePlayerRequest(gameId, email))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void GetBattles(string gameId, Action<BattleResponse[]> success, Action<RequestException> error)
    {
      RestClient.GetArray<BattleResponse>(BattlesRequest(gameId))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void CreateBattle(string gameId, Action<ResponseHelper> success, Action<RequestException> error)
    {
      RestClient.Post<ResponseHelper>(CreateBattleRequest(gameId))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    public void CreateCharacter(string name, int maxHp, int inventory, int baseResist, Action<ResponseHelper> success, Action<RequestException> error)
    {
      RestClient.Post<ResponseHelper>(CreateCharacterRequest(name, maxHp, inventory, baseResist))
      .Then(res => { Debug.Log(res); success(res); })
      .Catch(err => HandleError(err, error));
    }

    private RequestHelper LoginRequest(string email, string password)
    {
      var req = getBasicRequest("/login");
      req.Body = new LoginRequest { email = email, password = password };
      return req;
    }

    private RequestHelper PlayerRequest(string id)
    {
      return getAuthorizedRequest("/players/" + id);
    }

    private RequestHelper PlayerDetailRequest(string id)
    {
      return getAuthorizedRequest("/players/" + id + "/detail");
    }

    private RequestHelper RegisterRequest(string email, string name, string password)
    {
      RequestHelper req = getAuthorizedRequest("/players");
      req.Body = new RegisterRequest { email = email, name = name, password = password };
      return req;
    }

    private RequestHelper GamesRequest(string id)
    {
      return getAuthorizedRequest("/players/" + id + "/games");
    }

    private RequestHelper CreateGameRequest(string name, int maxPlayers)
    {
      RequestHelper req = getAuthorizedRequest("/games");
      req.Body = new CreateGameRequest { name = name, maxPlayers = maxPlayers };
      return req;
    }

    private RequestHelper InvitePlayerRequest(string gameId, string email)
    {
      RequestHelper req = getAuthorizedRequest("/games/" + gameId + "/players");
      req.Body = new InvitePlayerRequest { email = email };
      return req;
    }

    private RequestHelper BattlesRequest(string id)
    {
      return getAuthorizedRequest("/games/" + id + "/battles");
    }

    private RequestHelper CreateBattleRequest(string id)
    {
      return getAuthorizedRequest("/games/" + id + "/battles");
    }

    private RequestHelper CreateCharacterRequest(string name, int maxHp, int inventory, int baseResist)
    {
      RequestHelper req = getAuthorizedRequest("/games/" + gameManager.currentGame + "/characters");
      req.Body = new CreateCharacterRequest { name = name, maxHp = maxHp, inventorySize = inventory, baseResist = baseResist };
      return req;
    }

  }

}