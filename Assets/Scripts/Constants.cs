namespace Hanabanashiku.GameJam {
    public static class Constants {
        public static class Tags {
            public const string PLAYER = "Player";
            public const string PROJECTILE = "Projectile";
        }

        public static class Scenes {
            public const string MAIN_MENU = "MainMenu";
            public const string OPEN_DIALOG = "OpenDialog";
            public const string SCENE_1 = "Scene01_Forest";
            public const string SCENE_2 = "Scene02_Compound";
            public const string SCENE_3 = "Scene03_House";
            public const string LOSE = "Lose";
            public const string WIN = "Win";
        }

        public static class Dialogues {
            public const int OPEN_DIALOG = 2;
            public const int SCENE_1_OPEN = 3;
            public const int SCENE_2_GATE = 4;
            public const int SCENE_2_END = 5;
            public const int SCENE_3_START = 6;
            public const int SCENE_3_VICTORY = 7;
        }

        public const int MAX_HOSTAGES = 100;
        public const float SHOT_VOLUME = 0.4f;
    }
}