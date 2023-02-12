using GameRemoteServer.Services;

namespace GameRemoteServer.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;

        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IJwtService jwtService,
            IUserService userService)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();
            if(!string.IsNullOrEmpty(token))
            {
                var userId = jwtService.ValidateJwtToken(token);
                if(userId.HasValue)
                {
                    context.Items["User"] = userService.GetUserById(userId.Value);
                }
            }

            await next.Invoke(context);
        }
    }
}