using AutoMapper;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Helpers
{
    public class AutoMapper : IAutoMapper
    {
        private bool _isInitialized;

        public void Initialize()
        {
            if (!_isInitialized)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<LoggedInUserModel, UserModel>().ReverseMap();
                });
                _isInitialized = true;
            }
        }
    }
}
