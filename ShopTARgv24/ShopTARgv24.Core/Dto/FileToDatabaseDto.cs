﻿namespace ShopTARgv24.Core.Dto
{
    public class FileToDatabaseDto
    {
        public Guid Id { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}