global using System;
global using System.Threading.Tasks;
global using System.IO;
global using System.IO.Compression;
global using System.Collections.Generic;
global using System.Linq;

global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;

global using Grpc.Core;
global using Grpc.Core.Interceptors;
global using Grpc.Net.Compression;
global using Google.Protobuf.WellKnownTypes;

global using ReaAccountingSys.Server.Compression;
global using ReaAccountingSys.Server.Interceptors.Helpers;