using System.Linq;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using System.Web.Http;
using System;
using System.Collections.Generic;

namespace Cyrus.Bootstrapper.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwashbuckleExtensions
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            try
            {
                var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
                var isAuthorized = filterPipeline
                                                 .Select(filterInfo => filterInfo.Instance)
                                                 .Any(filter => filter is IAuthorizationFilter);

                var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
                //var myTokenAttribute = apiDescription.GetControllerAndActionAttributes<MyToken>().Any();
                if (isAuthorized && !allowAnonymous)
                {
                    if (operation.parameters == null)
                    {
                        operation.parameters = new List<Parameter>();
                    }
                    operation.parameters.Add(new Parameter()
                    {
                        name = "Authorization",
                        @in = "header",
                        description = "JWT Token Authorization",
                        required = true,
                        type = "string"
                    });
                }
            }catch(Exception e)
            {
                var message = e.Message;
                var inner = e.InnerException;
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TokenEndpointDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/token", new PathItem
            {
                post = new Operation
                {
                    tags = new string[] { "Authentication" },
                    summary = "Authenticates provided credentials and returns an access token",
                    operationId = "OAuth2TokenPost",
                    consumes = new string[] { "application/x-www-form-url-encoded" },
                    produces = new string[] { "application/json" },
                    parameters = new List<Parameter>
                {
                    new Parameter
                    {
                        name = "username",
                        @in = "formData",
                        type = "string"
                    },
                    new Parameter
                    {
                        name = "password",
                        @in = "formData",
                        type = "string"
                    },
                    new Parameter
                    {
                        name = "grant_type",
                        @in = "formData",
                        type = "string"
                    },
                    new Parameter
                    {
                        name = "client_id",
                        @in = "formData",
                        type = "string"
                    }
                },
                    responses = new Dictionary<string, Response>
                {
                    {
                        "200",
                        new Response
                        {
                            description = "Success",
                            schema = new Schema
                            {
                                type = "object",
                                properties = new Dictionary<string, Schema>
                                {
                                    {
                                        "access_token",
                                        new Schema
                                        {
                                            type = "string"
                                        }
                                    },
                                    //... etc. ...
                                }
                            }
                        }
                    }
                }
                }
            });
        }
    }

}


