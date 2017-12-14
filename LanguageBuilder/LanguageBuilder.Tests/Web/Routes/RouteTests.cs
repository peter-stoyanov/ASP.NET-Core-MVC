using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Routing;
using Xunit;

namespace LanguageBuilder.Tests.Web.Routes
{
    public class RouteFacts
    {
        //[Fact]
        //public void RouteWithControllerNoActionNoId()
        //{
        //    // Arrange
        //    //StubContext context = new StubContext("~/controller1");
        //    var context = new DefaultHttpContext();
        //    RouteCollection routes = new RouteCollection();
        //    //RouteConfig.RegisterRoutes(routes);

        //    // Act
        //    RouteData routeData = routes.GetRouteData(context);

        //    // Assert
        //    Assert.NotNull(routeData);
        //    Assert.Equal("controller1", routeData.Values["controller"]);
        //    Assert.Equal("Index", routeData.Values["action"]);
        //    Assert.Equal(UrlParameter.Optional, routeData.Values["id"]);
        //}

        //[Fact]
        //public void RouteWithControllerWithActionNoId()
        //{
        //    // Arrange
        //    StubContext context = new StubContext("~/controller1/action2");
        //    RouteCollection routes = new RouteCollection();
        //    RouteConfig.RegisterRoutes(routes);

        //    // Act
        //    RouteData routeData = routes.GetRouteData(context);

        //    // Assert
        //    Assert.NotNull(routeData);
        //    Assert.Equal("controller1", routeData.Values["controller"]);
        //    Assert.Equal("action2", routeData.Values["action"]);
        //    Assert.Equal(UrlParameter.Optional, routeData.Values["id"]);
        //}

        //[Fact]
        //public void RouteWithControllerWithActionWithId()
        //{
        //    // Arrange
        //    StubContext context = new StubContext("~/controller1/action2/id3");
        //    RouteCollection routes = new RouteCollection();
        //    RouteConfig.RegisterRoutes(routes);

        //    // Act
        //    RouteData routeData = routes.GetRouteData(context);

        //    // Assert
        //    Assert.NotNull(routeData);
        //    Assert.Equal("controller1", routeData.Values["controller"]);
        //    Assert.Equal("action2", routeData.Values["action"]);
        //    Assert.Equal("id3", routeData.Values["id"]);
        //}

        //[Fact]
        //public void RouteWithTooManySegments()
        //{
        //    // Arrange
        //    StubContext context = new StubContext("~/a/b/c/d");
        //    RouteCollection routes = new RouteCollection();
        //    RouteConfig.RegisterRoutes(routes);

        //    // Act
        //    RouteData routeData = routes.GetRouteData(context);

        //    // Assert
        //    Assert.Null(routeData);
        //}

        //[Fact]
        //public void RouteForEmbeddedResource()
        //{
        //    // Arrange
        //    StubContext context = new StubContext("~/foo.axd/bar/baz/biff");
        //    RouteCollection routes = new RouteCollection();
        //    RouteConfig.RegisterRoutes(routes);

        //    // Act
        //    RouteData routeData = routes.GetRouteData(context);

        //    // Assert
        //    Assert.NotNull(routeData);
        //    Assert.IsAssignableFrom<StopRoutingHandler>(routeData.RouteHandler);
        //}
    }
}
