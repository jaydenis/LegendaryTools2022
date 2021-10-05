using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Utilities
{
    public class MCUErrors
    {


        public static string GetRandomErrorMessage()
        {
            var random = new Random();
            List<string> errorMessages = new List<string>
            {
                "We're Fighting An Army Of Robots And I Have A Bow And Arrow. None Of This Makes Sense.",
                "Whatever It Takes...",
                "If You're Nothing Without The Suit, Then You Shouldn't Have It.",
                "I Have Nothing To Prove To You.",
                "She's Not Alone.",
                "I Can Do This All Day.",
                "That's My Secret Cap; I'm Always Angry.",
                "Wakanda Forever!",
                "I Am Iron Man.",
                "We Have A Hulk.",
                "There Was An Idea...",
                "We Are Groot!",
                "If We Can't Protect The Earth, You Can Be Damn Well Sure We'll Avenge It!",
                "Dormammu, I've Come To Bargain.",
                 "Dread it. Run from it. Destiny arrives all the same. And now, it's here. Or should I say, I am.",
                 "Fine, I'll do it myself."
            };
            int index = random.Next(errorMessages.Count);
            return errorMessages[index];
        }
    }
}
