using ChilliSoft_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChilliSoft_Assignment.Helpers
{
    public class Helper
    {
         private readonly DataContext _context = new DataContext();

        //Get last added ID
        public int getLastAddedMeetingID(string meeting)
        {

            int id = _context.Meetings.
                OrderByDescending(x=> x.MeetingId).
                Where(r => r.MeetingCode.StartsWith(meeting)).
                Select(c => c.MeetingId).FirstOrDefault();

            return id + 1;
        }

        //return previous meeting ID
        public int getPreviousMeetingId(string code)
        {
            int id = _context.Meetings.
                OrderByDescending(x => x.MeetingId).
                Where(r => r.MeetingCode.StartsWith(code)).
                Select(c => c.MeetingId).FirstOrDefault();

            return id;
        }

        //Check if Meeting Type code is not null
        public int checkMeetingCode(string code)
        {
            int id = _context.Meetings.
                Where(r => r.MeetingCode.StartsWith(code)).
                Select(c => c.MeetingId).Count();

            return id-1;
        }

        // Function to find string which has first character of each word.
        public string firstLetterWord(string str)
        {
            string result = "";

            bool v = true;
            for (int i = 0; i < str.Length; i++)
            {
                // If it is space, set v as true.
                if (str[i] == ' ')
                    v = true;
                else if (str[i] != ' ' && v == true)
                {
                    result += str[i];
                    v = false;
                }
            }

            return result.ToUpper();
        }
    }
}