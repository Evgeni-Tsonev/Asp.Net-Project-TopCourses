namespace TopCourses.Core.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using TopCourses.Core.Contracts;

    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine razorViewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IHttpContextAccessor contextAccessor;

        public ViewRenderService(
                                 IRazorViewEngine razorViewEngine,
                                 ITempDataProvider tempDataProvider,
                                 IHttpContextAccessor contextAccessor)
        {
            this.razorViewEngine = razorViewEngine;
            this.tempDataProvider = tempDataProvider;
            this.contextAccessor = contextAccessor;
        }

        public async Task<string> RenderToString(string viewName, object model)
        {
            var actionContext = new ActionContext(
                this.contextAccessor.HttpContext,
                this.contextAccessor.HttpContext.GetRouteData(),
                new ActionDescriptor());

            await using var sw = new StringWriter();
            var viewResult = this.FindView(actionContext, viewName);

            if (viewResult == null)
            {
                throw new ArgumentNullException($"{viewName} does not match any available view");
            }

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model,
            };

            var viewContext = new ViewContext(
                actionContext,
                viewResult,
                viewDictionary,
                new TempDataDictionary(actionContext.HttpContext, this.tempDataProvider),
                sw,
                new HtmlHelperOptions());

            await viewResult.RenderAsync(viewContext);
            return sw.ToString();
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = this.razorViewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = this.razorViewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));

            throw new InvalidOperationException(errorMessage);
        }
    }
}
