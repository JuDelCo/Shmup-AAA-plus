public static class ComponentIds {
    public const int Bullet = 0;
    public const int CollisionDamage = 1;
    public const int Coroutine = 2;
    public const int CoroutineWait = 3;
    public const int Damage = 4;
    public const int Destroy = 5;
    public const int Enemy = 6;
    public const int Energy = 7;
    public const int Explosion = 8;
    public const int Flash = 9;
    public const int Friendly = 10;
    public const int GameOver = 11;
    public const int Health = 12;
    public const int Immortal = 13;
    public const int Input = 14;
    public const int InputString = 15;
    public const int KeyDeletePressed = 16;
    public const int KeyDownPressed = 17;
    public const int KeyEscapePressed = 18;
    public const int KeyFirePressed = 19;
    public const int KeyLeftPressed = 20;
    public const int KeyReturnPressed = 21;
    public const int KeyRightPressed = 22;
    public const int KeyUpPressed = 23;
    public const int Killed = 24;
    public const int Leaderboard = 25;
    public const int Life = 26;
    public const int LifeNumScreen = 27;
    public const int LowestScoreLeaderboard = 28;
    public const int Map = 29;
    public const int MapSpeed = 30;
    public const int NotFriendly = 31;
    public const int Player = 32;
    public const int Points = 33;
    public const int Position = 34;
    public const int RegenerateView = 35;
    public const int Resource = 36;
    public const int RotateView = 37;
    public const int Score = 38;
    public const int ScoreNumScreen = 39;
    public const int Screen = 40;
    public const int ScreenLimitedDestroy = 41;
    public const int ScreenLimitedPosition = 42;
    public const int Shield = 43;
    public const int Speed = 44;
    public const int SpeedMovement = 45;
    public const int View = 46;

    public const int TotalComponents = 47;

    static readonly string[] components = {
        "Bullet",
        "CollisionDamage",
        "Coroutine",
        "CoroutineWait",
        "Damage",
        "Destroy",
        "Enemy",
        "Energy",
        "Explosion",
        "Flash",
        "Friendly",
        "GameOver",
        "Health",
        "Immortal",
        "Input",
        "InputString",
        "KeyDeletePressed",
        "KeyDownPressed",
        "KeyEscapePressed",
        "KeyFirePressed",
        "KeyLeftPressed",
        "KeyReturnPressed",
        "KeyRightPressed",
        "KeyUpPressed",
        "Killed",
        "Leaderboard",
        "Life",
        "LifeNumScreen",
        "LowestScoreLeaderboard",
        "Map",
        "MapSpeed",
        "NotFriendly",
        "Player",
        "Points",
        "Position",
        "RegenerateView",
        "Resource",
        "RotateView",
        "Score",
        "ScoreNumScreen",
        "Screen",
        "ScreenLimitedDestroy",
        "ScreenLimitedPosition",
        "Shield",
        "Speed",
        "SpeedMovement",
        "View"
    };

    public static string IdToString(int componentId) {
        return components[componentId];
    }
}

namespace Entitas {
    public partial class Matcher : AllOfMatcher {
        public Matcher(int index) : base(new [] { index }) {
        }

        public override string ToString() {
            return ComponentIds.IdToString(indices[0]);
        }
    }
}