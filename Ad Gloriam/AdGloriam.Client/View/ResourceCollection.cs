using System.Drawing;

namespace Ad_Gloriam.View
{
    public static class ResourceCollection
    {
        private static string[] names = new string[100];
        private static Image[] images = new Image[100];
        private static int _resourceCounter = 0;

        public static int ResourceCounter { get => _resourceCounter; set => _resourceCounter = value; }

        public static void addResources(string name, Image image, int i)
        {
            names[i] = name;
            images[i] = image;
            ResourceCounter++;
        }

        public static void Clear()
        {
            names = new string[100];
            images = new Image[100];
            ResourceCounter = 0;
        }

        public static Image GetResourceByName(string name)
        {
            for(int i = 0; i < names.Length; i++)
            {
                if (name.Equals(names[i])) return images[i];
            }

            return null;
        }

        public static void SetDefaultCollection()
        {
            Clear();
            names[0] = "ad_gloriam_game_background.png";
            images[0] = Properties.Resources.ad_gloriam_game_background;
            names[1] = "button_gray.png";
            images[1] = Properties.Resources.button_gray;
            names[2] = "button_ok.png";
            images[2] = Properties.Resources.button_ok;
            names[3] = "dialog_window.png";
            images[3] = Properties.Resources.dialog_window;
            names[4] = "logo.png";
            images[4] = Properties.Resources.logo;
            names[5] = "main_menu.png";
            images[5] = Properties.Resources.main_menu;
            names[6] = "menu_button_back.png";
            images[6] = Properties.Resources.menu_button_back;
            names[7] = "menu_button_create_game.png";
            images[7] = Properties.Resources.menu_button_create_game;
            names[8] = "menu_button_exit.png";
            images[8] = Properties.Resources.menu_button_exit;
            names[9] = "menu_button_hot_seat.png";
            images[9] = Properties.Resources.menu_button_hot_seat;
            names[10] = "menu_button_internet.png";
            images[10] = Properties.Resources.menu_button_internet;
            names[11] = "menu_button_join_game.png";
            images[11] = Properties.Resources.menu_button_join_game;
            names[12] = "menu_button_lan.png";
            images[12] = Properties.Resources.menu_button_lan;
            names[13] = "menu_button_multyplayer.png";
            images[13] = Properties.Resources.menu_button_multyplayer;
            names[14] = "menu_button_options.png";
            images[14] = Properties.Resources.menu_button_options;
            names[15] = "menu_button_random_game.png";
            images[15] = Properties.Resources.menu_button_random_name;
            names[16] = "menu_button_refresh.png";
            images[16] = Properties.Resources.menu_button_refresh;
            names[17] = "menu_button_singleplayer.png";
            images[17] = Properties.Resources.menu_button_singleplayer;
            names[18] = "menu_change_theme.png";
            images[18] = Properties.Resources.menu_change_theme;
            names[19] = "menu_create_game.png";
            images[19] = Properties.Resources.menu_create_game;
            names[20] = "menu_game_lobby.png";
            images[20] = Properties.Resources.menu_game_lobby;
            names[21] = "token_neutral.png";
            images[21] = Properties.Resources.token_neutral;
            names[22] = "token_neutral_avaiable.png";
            images[22] = Properties.Resources.token_neutral_avaiable;
            names[23] = "token_neutral_jump.png";
            images[23] = Properties.Resources.token_neutral_jump;
            names[24] = "token_player1.png";
            images[24] = Properties.Resources.token_player1;
            names[25] = "token_player1_selected.png";
            images[25] = Properties.Resources.token_player1_selected;
            names[26] = "token_player2.png";
            images[26] = Properties.Resources.token_player2;
            names[27] = "token_player2_selected.png";
            images[27] = Properties.Resources.token_player2_selected;
            names[28] = "menu_button_change_theme.png";
            images[28] = Properties.Resources.menu_button_change_theme;
            names[29] = "soundDisable.png";
            images[29] = Properties.Resources.soundDisable;
            names[30] = "soundEnabled.png";
            images[30] = Properties.Resources.soundEnabled;
            ResourceCounter = 31;
        }
    }
}
