﻿namespace Client.Models.Dtos.Category
{
    public class GetCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public string PictureBase64 { get; set; }

    }
}
