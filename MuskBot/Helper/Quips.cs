using System;
using System.Collections.Generic;
using System.Text;

namespace MuskBot.Helper
{
    public static class Quips
    {
        //Lists
        private static List<string> RegularQuips = new List<string> {Wholesome, WatchItHappen, Extraordinary, Seppuku, Reality };
        private static List<string> MentionQuips = new List<string> {SomethingWrong, ZuccBoi, StealData, Russian, Muskyboi, Fail, Intern, Coding };
        
        //regular strings
        public const string Wholesome = "When something is important enough, you do it even if the odds are not in your favor.";
        public const string WatchItHappen = "I could either watch it happen or be a part of it.";
        public const string Extraordinary = "I think it is possible for ordinary people to choose to be extraordinary.";
        public const string Seppuku = "My mentality is that of a samurai. I would rather commit seppuku than fail.";
        public const string Reality = "There's a billion-to-one chance we're living in base reality.";

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
