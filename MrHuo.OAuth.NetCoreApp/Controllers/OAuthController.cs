﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MrHuo.OAuth.Github;
using MrHuo.OAuth.Huawei;
using MrHuo.OAuth.QQ;
using MrHuo.OAuth.Wechat;

namespace MrHuo.OAuth.NetCoreApp.Controllers
{
    public class OAuthController : Controller
    {
        private readonly GithubOAuth githubOauth = null;
        private readonly WechatOAuth wechatOAuth = null;
        private readonly QQOAuth qqOAuth = null;
        private readonly HuaweiOAuth huaweiOAuth = null;

        public OAuthController(
            GithubOAuth githubOauth, 
            WechatOAuth wechatOAuth, 
            QQOAuth qqOAuth,
            HuaweiOAuth huaweiOAuth
        )
        {
            this.githubOauth = githubOauth;
            this.wechatOAuth = wechatOAuth;
            this.qqOAuth = qqOAuth;
            this.huaweiOAuth = huaweiOAuth;
        }

        [HttpGet("oauth/{type}")]
        public IActionResult Index(string type)
        {
            switch (type.ToLower())
            {
                case "github":
                    {
                        githubOauth.Authorize();
                        break;
                    }
                case "wechat":
                    {
                        wechatOAuth.Authorize();
                        break;
                    }
                case "qq":
                    {
                        qqOAuth.Authorize();
                        break;
                    }
                case "huawei":
                    {
                        huaweiOAuth.Authorize();
                        break;
                    }
                default:
                    return Content($"没有实现【{type}】登录！");
            }
            return Content("");
        }

        [HttpGet("oauth/{type}callback")]
        public IActionResult LoginCallback(string type)
        {
            switch (type.ToLower())
            {
                case "github":
                    {
                        var accessToken = githubOauth.AuthorizeCallback();
                        var userInfo = githubOauth.GetUserInfo(accessToken);
                        return Content(JsonSerializer.Serialize(new
                        {
                            accessToken,
                            userInfo
                        }));
                    }
                case "wechat":
                    {
                        var accessToken = wechatOAuth.AuthorizeCallback();
                        var userInfo = wechatOAuth.GetUserInfo(accessToken);
                        return Content(JsonSerializer.Serialize(new
                        {
                            accessToken,
                            userInfo
                        }));
                    }
                case "qq":
                    {
                        var accessToken = qqOAuth.AuthorizeCallback();
                        var userInfo = qqOAuth.GetUserInfo(accessToken);
                        return Content(JsonSerializer.Serialize(new
                        {
                            accessToken,
                            userInfo
                        }));
                    }
                case "huawei":
                    {
                        var accessToken = huaweiOAuth.AuthorizeCallback();
                        var userInfo = huaweiOAuth.GetUserInfo(accessToken);
                        return Content(JsonSerializer.Serialize(new
                        {
                            accessToken,
                            userInfo
                        }));
                    }
            }
            return Content($"没有实现【{type}】登录！");
        }
    }
}