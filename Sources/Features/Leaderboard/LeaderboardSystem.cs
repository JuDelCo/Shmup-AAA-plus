using UnityEngine;
using Entitas;

public class LeaderboardSystem : IExecuteSystem, ISetPool
{
	Pool _pool;
	Group _inputStringGroup;
	Group _leaderboardGroup;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_inputStringGroup = pool.GetGroup(Matcher.InputString);
		_leaderboardGroup = pool.GetGroup(Matcher.AllOf(Matcher.Leaderboard, Matcher.View));
		_leaderboardGroup.OnEntityAdded += (group, entity, index, component) => {
			entity.view.gameObject.transform.Find("HighScores").GetComponent<GUIText>().text = "Highscore\nusername:\n" + entity.leaderboard.userName.ToUpper().PadRight(3, '_');
		};
	}
	
	public void Execute()
	{
		if(_leaderboardGroup.Count > 0)
		{
			var e = _leaderboardGroup.GetSingleEntity();
			string userName = e.leaderboard.userName;
			
			if(_pool.isKeyReturnPressed)
			{
				e.view.gameObject.transform.Find("HighScores").GetComponent<GUIText>().text = "Refreshing\nleaderboard...";
				
				if(string.IsNullOrEmpty(userName))
				{
					RefreshLeaderboard(e.view.gameObject);
				}
				else
				{
					LeaderBoardController.PushNewScore(userName, _pool.score.value,
						() => RefreshLeaderboard(e.view.gameObject),
						ex => { e.view.gameObject.transform.Find("HighScores").GetComponent<GUIText>().text = "Leaderboard:\n\nWeb connection\nis required"; }
					);
				}
				
				e.RemoveLeaderboard();
			}
			else if(_pool.isKeyDeletePressed)
			{
				e.ReplaceLeaderboard("");
			}
			else if(_inputStringGroup.Count > 0)
			{
				var c = _inputStringGroup.GetEntities()[0].inputString.value[0];
				
				if(c == 0x08 /* Backspace */ && userName.Length > 0)
				{
					userName = userName.Substring(0, userName.Length - 1);
				}
				else if(userName.Length < 3 &&
					((c >= 97 && c <= 122) || (c >= 65 && c <= 90) || (c >= 48 && c <= 57))) // a-z, A-Z, 0-9
				{
					userName += c.ToString().ToUpper();
				}
				
				e.ReplaceLeaderboard(userName);
			}
		}
		
		foreach (var e in _inputStringGroup.GetEntities())
		{
			_pool.DestroyEntity(e);
		}
	}
	
	void RefreshLeaderboard(GameObject obj)
	{
		LeaderBoardController.GetHighScores(5, highScores =>
		{
			string highScoreValues = "";
			
			for (int i = 0; i < 5; ++i)
			{
				if (highScoreValues != "") highScoreValues += "\n";
				
				var scoreUserName = "---";
				var scoreValue = 0;
				
				if(highScores.Length >= i + 1)
				{
					if(! string.IsNullOrEmpty(highScores[i].userName))
						scoreUserName = highScores[i].userName.Substring(0, Mathf.Min(highScores[i].userName.Length, 3));
					
					scoreValue = highScores[i].score;
				}
				
				highScoreValues += string.Format("{0} {1} {2}", (i + 1), scoreUserName.PadRight(3, ' ').ToUpper(), scoreValue.ToString().PadLeft(8, '0'));
			}
			
			obj.transform.Find("HighScores").GetComponent<GUIText>().text = highScoreValues;
		},
		ex => { obj.transform.Find("HighScores").GetComponent<GUIText>().text = "Leaderboard:\n\nWeb connection\nis required"; } );
	}
}
