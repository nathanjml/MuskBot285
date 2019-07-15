﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuskBot.Helper
{
    public static class Quips
    {
        //Lists
        private static List<string> RegularQuips = new List<string> {Wholesome, WatchItHappen, Extraordinary, Seppuku, Reality, Mars, Failure, NegativeFeedback, SelfDrivingCars, Work, Ummm, Stealing, RussianMissiles, Pyramids, GoingHome, Vacation, RussianAssassaination, Dating, FutureOfCars };
        private static List<string> MentionQuips = new List<string> {SomethingWrong, ZuccBoi, StealData, Russian, Muskyboi, Fail, Intern, Coding };
        
        //regular strings
        public const string Wholesome = "When something is important enough, you do it even if the odds are not in your favor.";
        public const string WatchItHappen = "I could either watch it happen or be a part of it.";
        public const string Extraordinary = "I think it is possible for ordinary people to choose to be extraordinary.";
        public const string Seppuku = "My mentality is that of a samurai. I would rather commit [REDACTED] than fail.";
        public const string Reality = "There's a billion-to-one chance we're living in base reality.";
        public const string Mars = "I'd like to die on Mars, just not on impact";
        public const string Failure = "Failure is an option here.";
        public const string NegativeFeedback = "I think it is very important to actively seek out and listen very carefully to negative feedback.";
        public const string SelfDrivingCars = "Self-driving cars are the natural extension of active safety and obviously something we should do.";
        public const string Work = "Work is like hell.";
        public const string Ummm = "Ummm...";
        public const string Stealing = "Like why did you go steal Tesla’s E? Like you’re some sort of fascist army marching across the alphabet, some sort of Sesame Street robber?";
        public const string RussianMissiles = "So next I went to Russia three times, in late 2001 and 2002, to see if I could negotiate the purchase of two ICBMs [missiles]. Without the nukes, obviously.";
        public const string Pyramids = " Stacking stone blocks is not evidence of an advanced civilization.";
        public const string GoingHome = "The rumor that I'm building a spaceship to get back to my home planet Mars is totally untrue.";
        public const string Vacation = "Vacation will kill you";
        public const string RussianAssassaination = "My family fears that the Russians will assassinate me.";
        public const string Dating = "I would like to allocate more time to dating, though. I need to find a girlfriend.";
        public const string FutureOfCars = "You can’t have a person driving a two-ton death machine.";
        
        
        //special mention strings ('=' is the character that is replaced)
        public const string SomethingWrong = "Is there a problem here? Do I offend you =?";
        public const string ZuccBoi = "= have you met my friend, **zuckerbot**";
        public const string StealData = "= seems like he would sell people's data, dont you agree **zuckerbot**?";
        public const string Russian = "if = isn't a Russian, then I am not Elon Musk";
        public const string Muskyboi = "= you are messing with one Musky boi";
        public const string Fail = "No Tesla integrations on your project =? Expect failure.";
        public const string Intern = "You remind me of one of my interns =.";
        public const string Coding = "SpaceX needs you =.";

        public static string GetRandomQuip(string mentionUser)
        {
            var randomNumber = new Random().Next(0, (MentionQuips.Count - 1));
            var quip = MentionQuips[randomNumber];
            var result = quip.Replace("=", mentionUser);
            return result;
        }

        public static string GetRandomQuip()
        {
            var randomNumber = new Random().Next(0, (RegularQuips.Count - 1));
            return RegularQuips[randomNumber];
        }
    }
}
