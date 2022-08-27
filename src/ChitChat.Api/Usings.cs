﻿global using ChitChat.Api.Configuration;
global using ChitChat.Data.Configurations;
global using ChitChat.Data.Dependencies;
global using ChitChat.Data.Services;
global using ChitChat.Identity.Configuration;
global using ChitChat.Identity.Dependencies;
global using ChitChat.Identity.Documents;
global using ChitChat.Identity.DTOs;
global using ChitChat.Identity.Request;
global using ChitChat.Identity.Response;
global using ChitChat.Identity.Services;
global using ChitChat.Infrastructure.Dependencies;
global using ChitChat.Infrastructure.RabbitMQ;
global using ChitChat.Infrastructure.SignalR;
global using FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.IdentityModel.Tokens;
global using Serilog;
global using Serilog.Events;
global using StackExchange.Redis;
global using System.Text;