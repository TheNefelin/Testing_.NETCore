﻿namespace ClassLibrary_TicTacToe
{
    public class Player(string id, string name)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}