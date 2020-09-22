using softdesign_test_domain.Models.Entity;
using System;

namespace softdesign_test_domain.Models.DTOs
{
    public class ApplicationDTO
    {
        public int Application { get; set; }
        public bool DebuggingMode { get; set; }
        public string Url { get; set; }
        public string PathLocal { get; set; }

        public void Map(ApplicationEntity applicationEntity)
        {
            Url = applicationEntity.Url;
            PathLocal = applicationEntity.PathLocal;
            Application = applicationEntity.Application;
            DebuggingMode = applicationEntity.DebuggingMode;
        }
    }
}
