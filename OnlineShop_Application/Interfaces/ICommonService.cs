using System.Collections.Generic;
using OnlineShop_Application.ViewModels;

namespace OnlineShop_Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        List<SlideViewModel> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}