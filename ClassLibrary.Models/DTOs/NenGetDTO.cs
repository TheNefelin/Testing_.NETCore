﻿namespace ClassLibrary.Models.DTOs
{
    public class NenGetDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<HunterDTO> Hunter { get; set; }
    }
}
