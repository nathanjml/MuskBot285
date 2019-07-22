using System;
using System.Collections.Generic;
using System.Text;

namespace MuskBot.Helper
{
    public static class Quips
    {
        //Lists
        private static List<string> RegularQuips = new List<string> {Wholesome, WatchItHappen, Extraordinary, Seppuku, RealityIsNotReal, Mars, Failure, NegativeFeedback,
            SelfDrivingCars, Work, Ummm, Stealing, RussianMissiles, Pyramids, GoingHome, Vacation, RussianAssassaination, Dating, FutureOfCars, Catgirls, NoCats,
            WarrenBuffett, KanyeWest, Money, ScientificallyCorrect, WhereAreTheAliens, IAmAnAlien, Scale, MarsBars, Reality, HaHa, BusinessPlan, PlanesInSpace,
            Considerations, Emperor, BadDate, Tunnels, IWasJoking, Irony, Me, WatchThis, Awkward, FlamethrowerSales, TunnelCreator, AlienSubterfuge, GetOffLawn,
            ThatWouldSuck, TerribleIdea, Stab, Rockets, AprilFools, ThatActuallyHappened, Magnet, BeCareful };

        private static List<string> MentionQuips = new List<string> {SomethingWrong, ZuccBoi, StealData, Russian, Muskyboi, Fail, Intern, Coding, Spaceballs,
            NotAFlamethrower, MovingQuickly, Underappreciated, Haggling, DontGoogleIt, Beware };
        
        //regular strings
        public const string Wholesome = "When something is important enough, you do it even if the odds are not in your favor.";
        public const string WatchItHappen = "I could either watch it happen or be a part of it.";
        public const string Extraordinary = "I think it is possible for ordinary people to choose to be extraordinary.";
        public const string Seppuku = "My mentality is that of a samurai. I would rather commit [REDACTED] than fail.";
        public const string RealityIsNotReal = "There's a billion-to-one chance we're living in base reality.";
        public const string Mars = "I'd like to die on Mars, just not on impact";
        public const string Failure = "Failure is an option here.";
        public const string NegativeFeedback = "I think it is very important to actively seek out and listen very carefully to negative feedback.";
        public const string SelfDrivingCars = "Self-driving cars are the natural extension of active safety and obviously something we should do.";
        public const string Work = "Work is like hell.";
        public const string Ummm = "Ummm...";
        public const string Stealing = "Like why did you go steal Tesla’s E? Like you’re some sort of fascist army marching across the alphabet, some sort of Sesame Street robber?";
        public const string RussianMissiles = "So next I went to Russia three times, in late 2001 and 2002, to see if I could negotiate the purchase of two ICBMs. Without the nukes, obviously.";
        public const string Pyramids = " Stacking stone blocks is not evidence of an advanced civilization.";
        public const string GoingHome = "The rumor that I'm building a spaceship to get back to my home planet Mars is totally untrue.";
        public const string Vacation = "Vacation will kill you";
        public const string RussianAssassaination = "My family fears that the Russians will assassinate me.";
        public const string Dating = "I would like to allocate more time to dating, though. I need to find a girlfriend.";
        public const string FutureOfCars = "You can’t have a person driving a two-ton death machine.";
        public const string Catgirls = "Scientists say it’s a crucial step towards catgirls";
        public const string NoCats = "Cats, probably not tbh";
        public const string WarrenBuffett = "That’s a direct quote from Warren Buffett";
        public const string KanyeWest = "Well Kanye West... Obviously";
        public const string Money = "Imagine a pallet of cash, burning up, falling through the atmosphere.";
        public const string ScientificallyCorrect = "This is like,  80% scientifically correct.";
        public const string WhereAreTheAliens = "Where are the aliens?";
        public const string IAmAnAlien = "Some people think that I am an alien";
        public const string Scale = "It's on scale for scale's sake.";
        public const string MarsBars = "I think Mars should have really great bars. 'The Mars Bar'.";
        public const string Reality = "Reality is pretty messed up.";
        public const string HaHa = "Hahahahaha.";
        public const string BusinessPlan = "I don't really have a business plan.";
        public const string PlanesInSpace = "In space, wings are not very usefull... Because there is no air.";
        public const string Considerations = "These are important considerations.";
        public const string Emperor = "I want my title to be Emperor or God Emperor... I don't know yet.";
        public const string BadDate = "I went on a date and talked about nothing but electric cars.";
        public const string Tunnels = "I like tunnels.";
        public const string IWasJoking = "Please don't take that seriously. I was joking.";
        public const string Irony = "I must remember that not all people 'get' irony.";
        public const string Me = "I'm not sure I want to be me. Why do others want to?";
        public const string WatchThis = "Watch this...";
        public const string Awkward = "Yeah, that could be awkward with a rocket launch.";
        public const string FlamethrowerSales = "I thank you, for any who has bought our flamethrower. You will not be sorry... or maybe you will. Hehehehe, it won't be boring.";
        public const string TunnelCreator = "I invented tunnels.";
        public const string AlienSubterfuge = "We try to confuse the ailiens as much as possible.";
        public const string GetOffLawn = "Get off my lawn!";
        public const string ThatWouldSuck = "That would suck.";
        public const string TerribleIdea = "This is a terrible idea.";
        public const string Stab = "Did I just stab you?";
        public const string Rockets = "I'd like to buy two of your biggest rockets.";
        public const string AprilFools = "I'll just make an April Fools joke that we did go bankrupt.";
        public const string ThatActuallyHappened = "Wait. You're kidding. That actually happened? Like, just now... ";
        public const string Magnet = "Can someone go in and change my Facebook page to 'Magnet'? I want to be a magnet.";
        public const string BeCareful = "For the prototype, I would recommend not dropping anything while you are near it.";

        //special mention strings ('=' is the character that is replaced)
        public const string SomethingWrong = "Is there a problem here? Do I offend you =?";
        public const string ZuccBoi = "= have you met my friend, **zuckerbot**";
        public const string StealData = "= seems like he would sell people's data, dont you agree **zuckerbot**?";
        public const string Russian = "if = isn't a Russian, then I am not Elon Musk";
        public const string Muskyboi = "= you are messing with one Musky boi";
        public const string Fail = "No Tesla integrations on your project =? Expect failure.";
        public const string Intern = "You remind me of one of my interns =.";
        public const string Coding = "SpaceX needs you =.";
        public const string Spaceballs = "Also =, I stole the idea from Spaceballs";
        public const string NotAFlamethrower = "It’s Not a Flamethrower, Mr. =";
        public const string MovingQuickly = "= is moving at ~25 times the speed of sound & orbits the Earth every 90 minutes";
        public const string Underappreciated = "Later in life = is underappreciated";
        public const string Haggling = "But, if you’re going to buy a =, be sure to haggle properly!";
        public const string DontGoogleIt = "If you don’t already know what a = is, don’t google it. You have been warned.";
        public const string Competition = "= will have the best project this semester, I am sure of it.";
        public const string Beware = "Be careful, = can move in any direction";

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
