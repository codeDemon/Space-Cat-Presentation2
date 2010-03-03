using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Text;

namespace Space_Cats_V1._2
{
    class AudioManager : GameComponent
    {
        //Instance Variables
        private Dictionary<string, Song> z_ListSongs;
        private Dictionary<string, SoundEffect> z_ListSounds;
        public bool z_songIsPlaying;


        //Constructor
        public AudioManager(Game game)
            : base(game)
        {
            this.z_ListSongs = new Dictionary<string, Song>();
            this.z_ListSounds = new Dictionary<string, SoundEffect>();
            this.z_songIsPlaying = false;
        }

        //Accessors


        //Mutators


        //Load Method


        //Update Method


        //Play A Sound Method

















        /*Some Sound stuff
         * 
         * //Instance Variables
         * //Variables For Music
        private Song z_beautifulDarkness;
        private bool z_songStart = false;
        private SoundEffect z_achievementSound;
         * 
         * 
         * //Initialization
         * //Load the Music
            this.z_beautifulDarkness = Content.Load<Song>("Audio\\Music\\ATreeFalls");
            MediaPlayer.IsRepeating = true;
         * this.z_achievementSound = Content.Load<SoundEffect>("Audio\\SoundFX\\AchievementSound");
         * 
         * 
         * 
         * //Play a sound
         * this.z_achievementSound.Play();
         * 
         * //Play Background Music
            if (!this.z_songStart)
            {
                MediaPlayer.Play(this.z_beautifulDarkness);
                MediaPlayer.Volume = 0.7f;
                this.z_songStart = true;
            }
         * */

    }
}
