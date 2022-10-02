using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Hanabanashiku.GameJam.Database.Entities;
using UnityEngine;

namespace Hanabanashiku.GameJam.Database {
    public class DialogDatabase {
        private readonly IDbConnection _db;
        
        private static string _uri => $"URI=file:${Application.streamingAssetsPath}/dlg";
        
        private const string GetConversationQuery = @"
            SELECT
                ch.Name AS CharacterName
                ,ch.Photo AS CharacterPhoto
                ,ch.BorderColor AS CharacterColor
                ,l.Line AS Line
            FROM Line l
            JOIN Character ch
                ON ch.ID = l.CharacterID
            JOIN Conversation c
                ON c.ID = l.ConversationID
            WHERE c.ID = @p_ID
            ORDER BY
                l.SortOrder ASC
        ";
        
        public DialogDatabase() {
            _db = new SQLiteConnection(_uri);
        }
        
        public IEnumerable<VoiceLine> GetConversation(int id) {
            var result = _db.Query<VoiceLine>(GetConversationQuery, new { p_ID = id}).ToArray();
            if(!result.Any()) {
                throw new ArgumentException("Conversation not found", nameof(id));
            }
        
            return result;
        }
    }
}