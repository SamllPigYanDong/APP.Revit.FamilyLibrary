using System;

namespace Revit.Entity.Entity.Dtos.Family
{
    public class FamilyDto: DtoBase
    {
        private string _mainPhotoPath;
        public string MainPhotoPath
        {
            get { return _mainPhotoPath; }
            set { _mainPhotoPath = value; }
        }

        public byte[] MainPhotoBytes{ get; set; }
        public string Name { get; set; }

        public string FileExtension { get; set; } = "";

        public string FileSize { get; set; } = "";

        public long Key { get; set; }

        public int Version { get; set; } = 1;
    }
}