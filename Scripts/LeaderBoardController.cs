using System;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

public static class LeaderBoardController
{
	// https://apphq.shephertz.com/service/index#/leaderBoard
	const string apiKey = "XXXXXXXXXXXXXXXX_SECRET_API_KEY_XXXXXXXXXXXXXXXX";
	const string secretKey = "XXXXXXXXXXXXXXXX_SECRET_KEY_XXXXXXXXXXXXXXXX";
	const string gameName = "ShmupAAAp";
	
	static ScoreBoardService _scoreBoardService;
	
	static ScoreBoardService scoreBoardService
	{
        get
        {
            if (_scoreBoardService == null)
            {
            	App42API.Initialize(apiKey, secretKey);
            	_scoreBoardService = App42API.BuildScoreBoardService();
            }
            
            return _scoreBoardService;
        }
    }
	
	public static void GetHighScores(int numOfValues, Action<ScoreInfo[]> callback, Action<Exception> errorCallback = null)
	{
		scoreBoardService.GetTopNRankers(gameName, numOfValues, (App42CallBack)(new ScoreBoardServiceListCallBack(callback, errorCallback)));
	}
	
	public static void PushNewScore(string userName, int score, Action callback, Action<Exception> errorCallback = null)
	{
		scoreBoardService.SaveUserScore(gameName, userName, score, (App42CallBack)(new ScoreBoardServiceEmptyCallBack(callback, errorCallback)));
	}
}

public struct ScoreInfo
{
	private readonly string _userName;
	private readonly int _score;
	
	public string userName { get { return _userName; } }
	public int score { get { return _score; } }
	
	public ScoreInfo(string userName, int score)
	{
		_userName = userName;
		_score = score;
	}
}

class ScoreBoardServiceEmptyCallBack : App42CallBack
{
	private readonly Action _callback;
	private readonly Action<Exception> _errorCallback;
	
	public ScoreBoardServiceEmptyCallBack(Action callback, Action<Exception> errorCallback)
	{
		_callback = callback;
		_errorCallback = errorCallback;
	}
	
	public void OnSuccess(object response)
	{
		if (_callback != null)
			_callback();
	}
	
	public void OnException(Exception e)
	{
		if (_errorCallback != null)
			_errorCallback(e);
		else
			Debug.Log(e.Message);
	}
}

class ScoreBoardServiceListCallBack : App42CallBack
{
	private readonly Action<ScoreInfo[]> _callback;
	private readonly Action<Exception> _errorCallback;
	
	public ScoreBoardServiceListCallBack(Action<ScoreInfo[]> callback, Action<Exception> errorCallback)
	{
		_callback = callback;
		_errorCallback = errorCallback;
	}
	
	public void OnSuccess(object response)
	{
		var game = (Game)response;
		var scores = new ScoreInfo[game.GetScoreList().Count];
		
		for(int i = 0; i < game.GetScoreList().Count; ++i)
		{
			scores[i] = new ScoreInfo(game.GetScoreList()[i].GetUserName(), (int)game.GetScoreList()[i].GetValue());
		}
		
		_callback(scores);
	}
	
	public void OnException(Exception e)
	{
		try
		{
			if(((App42Exception)e).GetAppErrorCode() == 3013) // Not found | Leaderboard empty
			{
				_callback(new ScoreInfo[0]);
				return;
			}
		}
		catch (Exception) {}
		
		if(_errorCallback != null)
			_errorCallback(e);
		else
			Debug.Log(e.Message);
	}
}
