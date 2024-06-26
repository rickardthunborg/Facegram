﻿using Microsoft.EntityFrameworkCore;
using Project_Hashtag.Data;
using Project_Hashtag.Models;
using System.Security.Claims;

namespace Project_Hashtag.Data
{
    public class AccessControl
    {
        public int LoggedInAccountID { get; set; }
        public string LoggedInAccountName { get; set; }
        public string LoggedInAvatar { get; set; }
        public AccessControl(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext.User;
            string subject = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            string issuer = user.FindFirst(ClaimTypes.NameIdentifier).Issuer;

            LoggedInAccountID = db.Users.Single(p => p.OpenIDIssuer == issuer && p.OpenIDSubject == subject).ID;
            LoggedInAccountName = user.FindFirst(ClaimTypes.Name).Value;

            if(user.FindFirst("urn:google:image") != null){
                LoggedInAvatar = user.FindFirst("urn:google:image").Value;
            }
        }
    }
}
