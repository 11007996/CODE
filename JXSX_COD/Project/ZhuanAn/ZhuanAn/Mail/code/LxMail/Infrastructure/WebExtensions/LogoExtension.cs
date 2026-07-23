using Infrastructure.Helper;
using Infrastructure.Model;
using JinianNet.JNTemplate;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
{
    public static class LogoExtension
    {
        public static void AddLogo(this IServiceCollection services)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            var contentTpl = JnHelper.ReadTemplate("", "logo.txt");
            var content = contentTpl?.Render();
            var url = AppSettings.GetConfig("urls");
            SystemInfo info =    AppSettings.Get<SystemInfo>("SystemInfo");
            Console.WriteLine(content);
            Console.WriteLine(info.Name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Swagger地址：{url}/swagger/index.html");
            // Console.WriteLine($"初始化种子数据地址：{url}/common/InitSeedData");
        }
    }
}
